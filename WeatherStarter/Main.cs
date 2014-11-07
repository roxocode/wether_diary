using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using WeatherStarter.Properties;

/*
 * WeatherStarter used to checking for updates, updating and starting main application
*/

namespace WeatherStarter
{
    public partial class Main : Form
    {
        readonly string[] assemblies = new string[] { "WetherDiary.exe", "DBEngine.dll" };
        struct UpdateFile
        {
            public string name;
            public Version ver;
            public string crc;
        }
        List<UpdateFile> updateVersions;
        string updateDescr = string.Empty;
        
        public Main()
        {
            InitializeComponent();
            this.Icon = WeatherStarter.Properties.Resources.WeatherDiary;
        }

        private string GetUpdateTextVersions()
        {
            if (updateVersions == null)
                updateVersions = GetUpdateVersions();

            string res = string.Empty;
            foreach (UpdateFile file in updateVersions)
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
            versionDoc.Load(Settings.Default.VersionXmlPath);
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
            this.Hide();
            System.Diagnostics.ProcessStartInfo mainApp = new System.Diagnostics.ProcessStartInfo("WetherDiary.exe");
            System.Diagnostics.Process.Start(mainApp);
            Application.Exit();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(Settings.Default.BackupDir);
                using (WebClient wc = new WebClient())
                {
                    wc.Credentials = new NetworkCredential(Settings.Default.UserFTP, Settings.Default.PasswordFTP);
                    tbLog.AppendLine("Downloading file 'version.xml'...");
                    wc.DownloadFile(String.Format("{0}version.xml", Settings.Default.UpdateURI), Settings.Default.VersionXmlPath);
                }
            }
            catch (WebException ex)
            {
                // TODO: save log to file 'start.log'
                tbLog.AppendLine("Can't download file 'version.xml'");
                RunWeatherDiaryAndExit();
                return;
            }

            tbLog.AppendLine("File 'version.xml' was downloaded");
            // TODO: disable selection on tbVersions
            tbCurrentVersions.Text = GetCurrentTextVersions();
            tbUpdateVersions.Text = GetUpdateTextVersions();

            // Structure of update packege:
            // version.xml (XML file with versions data, crc sums)
            // WeatherDiary.exe or/and DBEngine.dll

            List<UpdateFile> updateFileList = GetUpdateVersions();
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
                            tbLog.AppendLine(string.Format("Downloading file '{0}' (version: {1})...", uf.name, uf.ver));
                            wc.DownloadFile(
                                string.Format("{0}{1}", Settings.Default.UpdateURI, uf.name),
                                string.Format("{0}\\{1}", Settings.Default.BackupDir, uf.name));
                        }
                    }
                    catch (WebException ex)
                    {
                        tbLog.AppendLine(string.Format("Can't download file '{0}'", uf.name));
                        continue;
                    }

                    tbLog.AppendLine(string.Format("File '{0}' was downloaded", uf.name));
                    // check verison one more time
                    Version downloadedVer = AssemblyName.GetAssemblyName(string.Format("{0}\\{1}", Settings.Default.BackupDir, uf.name)).Version;
                    if (!uf.ver.Equals(downloadedVer))
                    {
                        // TODO: write down to log file or/and send by e-mail
                        tbLog.AppendLine(string.Format("Version of '{0}' in version.xml and downloaded file is not appropriates", uf.name));
                        break;
                    }
                    else
                        tbLog.AppendLine(string.Format("Checking for file '{0}' versions appropriates was completed", uf.name));

                    // Make backup (copy file to backup directory with .bak extension)
                    System.IO.File.Copy(uf.name, string.Format("{0}\\{1}.bak", Settings.Default.BackupDir, uf.name), true);
                    // copy updated file
                    System.IO.File.Copy(string.Format("{0}\\{1}", Settings.Default.BackupDir, uf.name), uf.name, true);
                    // delete update file
                    System.IO.File.Delete(string.Format("{0}\\{1}", Settings.Default.BackupDir, uf.name));
                    tbLog.AppendLine(string.Format("File '{0}' was updated", uf.name));
                    if (this.updateDescr == string.Empty)
                        MessageBox.Show("This application was updated.");
                    else
                        MessageBox.Show(string.Format("This application was updated.{0}What's new:{0}{1}", Environment.NewLine, this.updateDescr));
                }
                else
                    tbLog.AppendLine(string.Format("File '{0}' is already updated", uf.name));
            }

            #if DEBUG
            MessageBox.Show("This application was updated");
            #endif
            
            RunWeatherDiaryAndExit();
        }
    }
}
