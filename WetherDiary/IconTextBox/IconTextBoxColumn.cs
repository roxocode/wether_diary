using System;
using System.Drawing;
using System.Windows.Forms;

namespace WetherDiary.IconTextBox
{
    /// <summary>
    /// Description of IconTextColumn.
    /// </summary>
    public class IconTextColumn : DataGridViewTextBoxColumn
    {
        private Image icon;
        private Size iconSize;
        
        public IconTextColumn()
        {
            this.CellTemplate = new IconTextCell();
        }
        
        public override object Clone()
        {
            IconTextColumn c = base.Clone() as IconTextColumn;
            c.icon = this.icon;
            c.iconSize = this.iconSize;
            return c;
        }
        
        public Image Image
        {
            get { return this.icon; }
            set
            {
                if (this.icon != value)
                {
                    this.icon = value;
                    this.iconSize = value.Size;
                    
                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.DefaultCellStyle.Padding = new Padding(iconSize.Width, 
                                                                    inheritedPadding.Top, 
                                                                    inheritedPadding.Right, 
                                                                    inheritedPadding.Bottom);
                    }
                }
            }
        }
        
        private IconTextCell IconTextCellTemplate
        {
            get { return this.CellTemplate as IconTextCell; }
        }
        
        internal Size IconSize
        {
            get { return iconSize; }
        }
    }
}
