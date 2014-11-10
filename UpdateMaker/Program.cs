using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;

/*
 * Application for making update content (version.xml): files name, version, crc etc.
 */

namespace UpdateMaker
{
    class Program
    {
        static readonly string[] assemblies = new string[] { "WetherDiary.exe", "DBEngine.dll" };

        static void Main(string[] args)
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XComment(DateTime.Now.ToString()),
                new XElement("update",
                    new XElement("description", "What's new"),
                    new XElement("files")));

            foreach (string file in assemblies)
            {
                Version ver = AssemblyName.GetAssemblyName(file).Version;
                doc.Root.Element("files").Add(
                    new XElement("file",
                        new XElement("name", file),
                        new XElement("version",
                            new XElement("major", ver.Major),
                            new XElement("minor", ver.Minor),
                            new XElement("build", ver.Build),
                            new XElement("revision", ver.Revision)),
                        new XElement("crc", GetMD5(file)),
                        new XElement("path", "")));
            }
            
            doc.Save("version.xml");
        }

        static string GetMD5(string file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(file))
                {
                    byte[] b = md5.ComputeHash(stream);
                    return BitConverter.ToString(b).Replace("-", "").ToLower();
                }
            }
        }

        /*
        // Input form (for new file name
        private DialogResult GetWhatNew(ref string input)
        {
            Form inputForm = new Form();
            inputForm.Text = "";
            inputForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.Height = 70;
            inputForm.Width = 200;

            TextBox tbNewFileName = new TextBox();
            tbNewFileName.Height = 23;
            tbNewFileName.Width = 130;
            tbNewFileName.Location = new Point(5, 5);
            inputForm.Controls.Add(tbNewFileName);

            Button btnOK = new Button();
            btnOK.Size = new Size(60, 30);
            btnOK.Location = new Point(140, 5);
            btnOK.Text = "OK";
            btnOK.DialogResult = DialogResult.OK;
            inputForm.Controls.Add(btnOK);

            inputForm.AcceptButton = btnOK;

            DialogResult result = inputForm.ShowDialog();
            input = tbNewFileName.Text;
            return result;
        }
        */
    }
}
