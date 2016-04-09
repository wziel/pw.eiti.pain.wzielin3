using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    internal class ColorEditor : UITypeEditor
    {
        private Dictionary<ColorType, Color> colorTypeToColor = new Dictionary<ColorType, Color>
        {
            { ColorType.Red, System.Drawing.Color.FromKnownColor(KnownColor.Red) },
            { ColorType.Green, System.Drawing.Color.FromKnownColor(KnownColor.Green) },
            { ColorType.Blue, System.Drawing.Color.FromKnownColor(KnownColor.Blue) },
        };

        public override void PaintValue(PaintValueEventArgs e)
        {
            var colorType = (ColorType)e.Value;
            var color = colorTypeToColor[colorType];
            var brush = new SolidBrush(color);
            var pen = new Pen(color, 0);
            var rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            e.Graphics.DrawRectangle(pen, rect);
            e.Graphics.FillRectangle(brush, rect);
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if(editorService != null)
            {
                var colorControl = new ColorControl();
                colorControl.ColorType = (ColorType)value;
                editorService.DropDownControl(colorControl);
                return colorControl.ColorType;
            }
            return value;
        }
    }
}