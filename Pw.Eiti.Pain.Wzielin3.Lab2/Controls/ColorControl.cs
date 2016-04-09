using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    public partial class ColorControl : UserControl
    {
        public event EventHandler ColorChanged;

        private ColorType colorType;
        private Color Color
        {
            get
            {
                return colorTypeToColor[colorType];
            }
        }

        private Dictionary<ColorType, Color> colorTypeToColor = new Dictionary<ColorType, Color>
        {
            { ColorType.Red, System.Drawing.Color.FromKnownColor(KnownColor.Red) },
            { ColorType.Green, System.Drawing.Color.FromKnownColor(KnownColor.Green) },
            { ColorType.Blue, System.Drawing.Color.FromKnownColor(KnownColor.Blue) },
        };
        
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        [Category("Point")]
        [Browsable(true)]
        public ColorType ColorType
        {
            get { return colorType; }
            set
            {
                if(colorType != value)
                {
                    colorType = value;
                    Invalidate();
                    if(ColorChanged != null)
                    {
                        ColorChanged.Invoke(this, null);
                    }
                }
            }
        }

        public ColorControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var brush = new SolidBrush(Color);
            var pen = new Pen(Color, 0);
            base.OnPaint(e);
            var controlWidth = Size.Width;
            var controlHeight = Size.Height;
            var rect = new Rectangle(0, 0, controlWidth, controlHeight);
            e.Graphics.DrawRectangle(pen, rect);
            e.Graphics.FillRectangle(brush, rect);
        }

        private void ColorControl_Click(object sender, EventArgs e)
        {
            ColorType = (ColorType)(((int)ColorType + 1) % 3);
        }

    }
}
