using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;

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
        
        public Main()
        {
            InitializeComponent();
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
            versionDoc.Load(Properties.Settings.Default.VersionXmlPath);
            // TODO: Show what's new section on the form
            //MessageBox.Show(versionDoc.SelectSingleNode("/update/description").InnerText);
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
            System.Diagnostics.ProcessStartInfo mainApp = new System.Diagnostics.ProcessStartInfo("WetherDiary.exe");
            System.Diagnostics.Process.Start(mainApp);
            Application.Exit();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Credentials = new NetworkCredential(Properties.Settings.Default.UserFTP, Properties.Settings.Default.PasswordFTP);
                    tbLog.AppendLine("Downloading file 'version.xml'...");
                    wc.DownloadFile(String.Format("{0}version.xml", Properties.Settings.Default.UpdateURI), "backup\\version.xml");
                }
            }
            catch (WebException ex)
            {
                tbLog.AppendLine("Can't download file 'version.xml'");
                RunWeatherDiaryAndExit();
                return;
            }

            tbLog.AppendLine("File 'version.xml' was downloaded");
            // TODO: disable selection on tbVersions
            tbVersions.Text = GetCurrentTextVersions();
            tbUpdateVersions.Text = GetUpdateTextVersions();

            // Structure of update packege:
            // version.xml (XML file with versions data, crc sums)
            // WeatherDiary.exe or/and DBEngine.dll
            Properties.Settings consts = Properties.Settings.Default;

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
                            wc.Credentials = new NetworkCredential(consts.UserFTP, consts.PasswordFTP);
                            tbLog.AppendLine(string.Format("Downloading file '{0}' (version: {1})...", uf.name, uf.ver));
                            wc.DownloadFile(
                                string.Format("{0}{1}", consts.UpdateURI, uf.name),
                                string.Format("backup\\{0}", uf.name));
                        }
                    }
                    catch (WebException ex)
                    {
                        tbLog.AppendLine(string.Format("Can't download file '{0}'", uf.name));
                        continue;
                    }

                    tbLog.AppendLine(string.Format("File '{0}' was downloaded", uf.name));
                    // check verison one more time
                    Version downloadedVer = AssemblyName.GetAssemblyName(string.Format("backup\\{0}", uf.name)).Version;
                    if (!uf.ver.Equals(downloadedVer))
                    {
                        // TODO: write down to log file or/and send by e-mail
                        tbLog.AppendLine(string.Format("Version of '{0}' in version.xml and downloaded file is not appropriates", uf.name));
                        break;
                    }
                    else
                        tbLog.AppendLine(string.Format("Checking for file '{0}' versions appropriates was completed", uf.name));

                    // Make backup (copy file to backup directory with .bak extension)
                    System.IO.File.Copy(uf.name, string.Format("backup\\{0}.bak", uf.name), true);
                    // copy updated file
                    System.IO.File.Copy(string.Format("backup\\{0}", uf.name), uf.name, true);
                    // delete update file
                    System.IO.File.Delete(string.Format("backup\\{0}", uf.name));
                    tbLog.AppendLine(string.Format("File '{0}' was updated", uf.name));
                }
                else
                    tbLog.AppendLine(string.Format("File '{0}' is already updated", uf.name));
            }

            #if DEBUG
            MessageBox.Show("Done");
            #endif
            
            RunWeatherDiaryAndExit();
        }
    }
}
