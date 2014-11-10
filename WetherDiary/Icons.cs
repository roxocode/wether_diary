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

        // Получаем иконку осадка
        static public Image GetIconImage(string iconPath)
        {
            // TODO: Путь к иконке по умолнчанию в константу или в настройки
            Image res = null;
            try
            {
                // без этого выдает ошибку ArgumentException: The path is not of a legal form.
                if (iconPath == string.Empty)
                    res = Image.FromFile("icons\\default.png");
                else
                    res = Image.FromFile(iconPath);
            }
            catch (System.IO.FileNotFoundException e)
            {
                res = Image.FromFile("icons\\default.png");
            }
            return res;
        }
    }
}
