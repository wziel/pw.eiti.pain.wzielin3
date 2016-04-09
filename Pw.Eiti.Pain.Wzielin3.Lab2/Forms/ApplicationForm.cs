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

        public ApplicationForm()
        {
            InitializeComponent();
        }

        public ApplicationForm(ApplicationModel model)
        {
            InitializeComponent();

            ApplicationModel = model;
            model.PointAdded += PointAdded;
            model.PointRemoved += PointRemoved;
            model.PointChanged += PointChanged;
        }

        protected virtual void PointAdded(object sender, EventArgs e)
        {
        }

        protected virtual void PointRemoved(object sender, EventArgs e)
        {
        }

        protected virtual void PointChanged(object sender, EventArgs e)
        {
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedModels();
            if(selected.Count != 0)
            {
                var form = new NewForm(selected.First());
                form.ShowDialog();
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedModels();
            foreach(var model in selected)
            {
                ApplicationModel.Remove(model);
            }
        }

        protected virtual IReadOnlyCollection<PointModel> GetSelectedModels()
        {
            return new List<PointModel>();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(ApplicationModel.FormsCount <= 1)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
                ApplicationModel.FormsCount--;
            }
        }
    }
}
