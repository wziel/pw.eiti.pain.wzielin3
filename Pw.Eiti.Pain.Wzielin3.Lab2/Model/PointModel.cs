
using System;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    public class PointModel
    {
        public ApplicationModel appModel { get; set; }

        public PointModel(ApplicationModel parentModel)
        {
            appModel = parentModel;
        }

        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                if (label != value)
                {
                    label = value;
                    appModel.Change(this);
                }
            }
        }

        private int x;
        public int X
        {
            get { return x; }
            set
            {
                if(x != value)
                {
                    x = value;
                    appModel.Change(this);
                }
            }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set
            {
                if(y != value)
                {
                    y = value;
                    appModel.Change(this);
                }
            }
        }

        private ColorType color;
        public ColorType Color
        {
            get { return color; }
            set
            {
                if(color != value)
                {
                    color = value;
                    appModel.Change(this);
                }
            }
        }
    }
}