
using System;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    public class PointModel
    {
        public event EventHandler Changed;

        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                if (label != value && Changed != null)
                {
                    Changed.Invoke(this, null);
                }
                label = value;
            }
        }

        private int x;
        public int X
        {
            get { return x; }
            set
            {
                if(x != value && Changed != null)
                {
                    Changed.Invoke(this, null);
                }
                x = value;
            }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set
            {
                if(y != value && Changed != null)
                {
                    Changed.Invoke(this, null);
                }
                y = value;
            }
        }

        private ColorType color;
        public ColorType Color
        {
            get { return color; }
            set
            {
                if(color != value && Changed != null)
                {
                    Changed.Invoke(this, null);
                }
                color = value;
            }
        }
    }
}