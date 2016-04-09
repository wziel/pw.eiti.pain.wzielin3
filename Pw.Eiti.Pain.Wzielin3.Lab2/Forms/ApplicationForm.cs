using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    public partial class ApplicationForm : Form
    {
        protected ApplicationModel ApplicationModel { get; private set; }

        public ApplicationForm(ApplicationModel model)
        {
            InitializeComponent();

            ApplicationModel = model;
            model.PointAdded += PointAdded;
            model.PointRemoved += PointRemoved;
        }

        protected virtual void PointAdded(object sender, EventArgs e)
        {
            var model = (PointModel)sender;
            model.Changed += PointChanged;
        }

        protected virtual void PointRemoved(object sender, EventArgs e)
        {
            var model = (PointModel)sender;
            model.Changed -= PointRemoved;
        }

        protected virtual void PointChanged(object sender, EventArgs e)
        {
        }
    }
}
