using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using WeatherStarter.Properties;
using Logger;
using System.ComponentModel;

/*
 * WeatherStarter used to checking for updates, updating and starting main application
*/

namespace WeatherStarter
{
    public partial class Main : Form
    {
        // TODO: Exclude to resource file
        readonly string weatherFileName = "WetherDiary.exe";
        readonly string[] assemblies = new string[] { "WetherDiary.exe", "DBEngine.dll" };
        struct UpdateFile
        {
            public string name;
            public Version ver;
            public string crc;
        }
        string updateDescr = string.Empty;
        BackgroundWorker bWorker = new BackgroundWorker();
        Logger.Logger log = new Logger.Logger("logs/start.log");

        public Main()
        {
            InitializeComponent();
            this.Icon = WeatherStarter.Properties.Resources.WeatherDiary;

            bWorker.WorkerReportsProgress = true;
            bWorker.DoWork += bWorker_DoWork;
            bWorker.ProgressChanged += (s, e) => { tbLog.AppendLine(e.UserState.ToString()); };
            bWorker.RunWorkerAsync();
        }

        private void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(Settings.Default.BackupDir);
                using (WebClient wc = new WebClient())
                {
                    wc.Credentials = new NetworkCredential(Settings.Default.UserFTP, Settings.Default.PasswordFTP);
                    bWorker.ReportProgress(0, string.Format("Downloading file '{0}'...", Settings.Default.VersionXmlFile));
                    wc.DownloadFile(
                        string.Concat(Settings.Default.UpdateURI, Settings.Default.VersionXmlFile),
                        Path.Combine(Settings.Default.BackupDir, Settings.Default.VersionXmlFile));
                }
            }
            catch (WebException ex)
            {
                log.AddToLog(ex.Message);
                bWorker.ReportProgress(0, string.Format("Can't download file '{0}'", Settings.Default.VersionXmlFile));
                RunWeatherDiaryAndExit();
                return;
            }

            bWorker.ReportProgress(0, string.Format("File '{0}' was downloaded", Settings.Default.VersionXmlFile));
            // TODO: disable selection on tbVersions
            tbCurrentVersions.Invoke((MethodInvoker)(() => tbCurrentVersions.Text = GetCurrentTextVersions()));
            List<UpdateFile> updateFileList = GetUpdateVersions();
            tbUpdateVersions.Invoke((MethodInvoker)(() => tbUpdateVersions.Text = GetUpdateTextVersions(updateFileList)));

            // Structure of update packege:
            // version.xml (XML file with versions data, crc sums)
            // WeatherDiary.exe or/and DBEngine.dll
            foreach (UpdateFile uf in updateFileList)
            {
                // Compare assembly versions
                System.Version curFile = System.Reflection.AssemblyName.GetAssemblyName(uf.name).Version;
                if (uf.ver > curFile)
                {
                    try
                    {
                        using (WebClient wc = new WebClient())
                        {
                            wc.Credentials = new NetworkCredential(Settings.Default.UserFTP, Settings.Default.PasswordFTP);
                            bWorker.ReportProgress(0, string.Format("Downloading file '{0}' (version: {1})...", uf.name, uf.ver));
                            wc.DownloadFile(
                                string.Concat(Settings.Default.UpdateURI, uf.name),
                                Path.Combine(Settings.Default.BackupDir, uf.name));
                        }
                    }
                    catch (WebException)
                    {
                        bWorker.ReportProgress(0, string.Format("Can't download file '{0}'", uf.name));
                        continue;
                    }

                    bWorker.ReportProgress(0, string.Format("File '{0}' was downloaded", uf.name));
                    // check verison one more time
                    Version downloadedVer = AssemblyName.GetAssemblyName(string.Format("{0}\\{1}", Settings.Default.BackupDir, uf.name)).Version;
                    if (!uf.ver.Equals(downloadedVer))
                    {
                        bWorker.ReportProgress(0, string.Format("Version of '{0}' in {1} and downloaded file is not appropriates", uf.name, Settings.Default.VersionXmlFile));
                        break;
                    }
                    else
                        bWorker.ReportProgress(0, string.Format("Checking for file '{0}' versions appropriates was completed", uf.name));

                    // Make backup (copy file to backup directory with .bak extension)
                    System.IO.File.Copy(uf.name, string.Format("{0}\\{1}.bak", Settings.Default.BackupDir, uf.name), true);
                    // copy updated file
                    System.IO.File.Copy(string.Concat(Settings.Default.BackupDir, uf.name), uf.name, true);
                    // delete update file
                    System.IO.File.Delete(string.Concat(Settings.Default.BackupDir, uf.name));
                    bWorker.ReportProgress(0, string.Format("File '{0}' was updated", uf.name));
                    if (this.updateDescr == string.Empty)
                        MessageBox.Show("This application was updated.");
                    else
                        MessageBox.Show(string.Format("This application was updated.{0}What's new:{0}{1}", Environment.NewLine, this.updateDescr));
                }
                else
                    bWorker.ReportProgress(0, string.Format("File '{0}' is already updated", uf.name));
            }

            #if DEBUG
                MessageBox.Show("This application was updated");
            #endif

            RunWeatherDiaryAndExit();
        }

        private string GetUpdateTextVersions(List<UpdateFile> updates)
        {
            string res = string.Empty;
            foreach (UpdateFile file in updates)
            {
                res += string.Format("{0}\t{1}{2}", file.name, file.ver.ToString(), Environment.NewLine);
            }
            return res;
        }

        private List<UpdateFile> GetUpdateVersions()
        {
            List<UpdateFile> res = new List<UpdateFile>();
            // Parse XML file (using XPath)
            XmlDocument versionDoc = new XmlDocument();
            versionDoc.Load(Path.Combine(Settings.Default.BackupDir, Settings.Default.VersionXmlFile));
            // TODO: Show what's new section on the form
            this.updateDescr = versionDoc.SelectSingleNode("/update/description").InnerText;
            // Get files section
            foreach (XmlNode node in versionDoc.SelectNodes("/update/files//file"))
            {
                UpdateFile f;
                f.name = node.SelectSingleNode("name").InnerText;
                f.ver = new Version(int.Parse(node.SelectSingleNode("version/major").InnerText),
                                    int.Parse(node.SelectSingleNode("version/minor").InnerText),
                                    int.Parse(node.SelectSingleNode("version/build").InnerText),
                                    int.Parse(node.SelectSingleNode("version/revision").InnerText));
                f.crc = node.SelectSingleNode("crc").InnerText;
                res.Add(f);
            }
            return res;
        }

        private string GetCurrentTextVersions()
        {
            string res = string.Empty;
            foreach (string asmbl in this.assemblies)
            {
                System.Version asmblVer = AssemblyName.GetAssemblyName(asmbl).Version;
                res += string.Format("{0}\t{1}{2}", asmbl, asmblVer.ToString(), Environment.NewLine);
            }
            return res;
        }

        private void RunWeatherDiaryAndExit()
        {
            string msg = string.Empty;
            this.Invoke((MethodInvoker)(() => msg = tbLog.Text));
            log.AddDateToLog();
            log.AddToLog(msg, false);

            this.Invoke((MethodInvoker)(() => this.Hide()));
            System.Diagnostics.ProcessStartInfo mainApp = new System.Diagnostics.ProcessStartInfo(this.weatherFileName);
            System.Diagnostics.Process.Start(mainApp);
            Application.Exit();
        }

        private void Main_Shown(object sender, EventArgs e)
        {

        }
    }
}
