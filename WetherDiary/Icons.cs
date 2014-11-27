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

        // Получаем иконку осадка
        static public Image GetIconImage(string iconPath)
        {
            Image res = null;
            try
            {
                // без этого выдает ошибку ArgumentException: The path is not of a legal form.
                if (iconPath == string.Empty)
                    res = Image.FromFile(DefaultIconPath);
                else
                    res = Image.FromFile(iconPath);
            }
            catch (System.IO.FileNotFoundException)
            {
                res = Image.FromFile(DefaultIconPath);
            }
            return res;
        }
    }
}
