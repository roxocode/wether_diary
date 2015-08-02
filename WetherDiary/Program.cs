using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WetherDiary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string errorMsg = string.Format("Unhandled exception:\n\n {0} \n\n {1} \n\n Stack Trace:\n {2}",
                e.Exception.Message,
                e.Exception.GetType(),
                e.Exception.StackTrace);
            if (MessageBox.Show(errorMsg, "Application Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.Cancel)
                Application.Exit();
        }
    }
}
