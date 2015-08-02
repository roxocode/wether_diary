using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace WetherDiary
{
    static public class Icons
    {
        /// <summary>
        /// Width and height of icon - constant
        /// </summary>
        static public readonly byte IconSize = Properties.Settings.Default.IconSize;

        /// <summary>
        /// Icon's directory
        /// </summary>
        static public readonly string IconsDirectory = Properties.Settings.Default.IconsDir;

        /// <summary>
        /// Absolute default icon path
        /// </summary>
        static public readonly string DefaultIconPath = Path.Combine(
            Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
                Properties.Settings.Default.IconsDir),
            Properties.Settings.Default.DefaultIconName);

        /// <summary>
        /// Get icon image from relative path (relative path convert to absolute)
        /// </summary>
        /// <param name="relativeIconPath">relative icon path</param>
        /// <returns></returns>
        static public Image GetIconImage(string relativeIconPath)
        {
            Image res = null;
            string absoluteIconPath = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
                relativeIconPath);
            try
            {
                // без этого выдает ошибку ArgumentException: The path is not of a legal form.
                if (absoluteIconPath == string.Empty)
                    res = Image.FromFile(DefaultIconPath);
                else
                    res = Image.FromFile(absoluteIconPath);
            }
            catch (System.IO.FileNotFoundException)
            {
                res = Image.FromFile(DefaultIconPath);
            }
            return res;
        }
    }
}
