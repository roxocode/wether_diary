using System;
using System.Drawing;
using System.Windows.Forms;

namespace WetherDiary.IconTextBox
{
    /// <summary>
    /// Description of IconTextCell.
    /// </summary>
    public class IconTextCell : DataGridViewTextBoxCell
    {
        // test for icon 16 x 16
        private int iconLeftPad = 2;
        private int iconTopPad = 2;
        
        private Image iconValue;
        private Size iconSize;
        
        public override object Clone()
        {
            IconTextCell c = base.Clone() as IconTextCell;
            c.iconValue = this.iconValue;
            c.iconSize = this.iconSize;
            return c;
        }
        
        public Image Icon
        {
            get
            {
                if (this.OwningColumn == null || this.OwningIconTextColumn == null)
                {
                    return iconValue;
                }
                else if (this.iconValue != null)
                {
                    return this.iconValue;
                }
                else
                {
                    return this.OwningIconTextColumn.Image;
                }
            }
            set
            {
                if (this.iconValue != value)
                {
                    this.iconValue = value;
                    this.iconSize = value.Size;
                    
                    Padding inheritedPadding = this.InheritedStyle.Padding;
                    this.Style.Padding = new Padding(iconSize.Width + iconLeftPad,
                                                     inheritedPadding.Top,
                                                     inheritedPadding.Right,
                                                     inheritedPadding.Bottom);
                }
            }
        }
        
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, 
        DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, 
            paintParts);
            
            if (this.Icon != null)
            {
                System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
                graphics.SetClip(cellBounds);
                // ?????? ??? ????????? ??????
                Point p = new Point();
                p = cellBounds.Location;
                p.Offset(iconLeftPad, iconTopPad);
                graphics.DrawImageUnscaled(this.Icon, p);
                graphics.EndContainer(container);
            }
        }
        
        private IconTextColumn OwningIconTextColumn
        {
            get { return this.OwningColumn as IconTextColumn; }
        }
    }
}
