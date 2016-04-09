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
    public partial class MDIContainer : Form
    {
        public ApplicationModel Model { get; set; }
        private ICollection<Form> childForms { get; set; } = new List<Form>();
        public int childFormsCount { get { return childForms.Count; } }

        public MDIContainer(ApplicationModel model)
        {
            InitializeComponent();
            IsMdiContainer = true;
            Model = model;
            MainMenuStrip = menuStrip;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddNewForm(new ListViewForm(Model));
            AddNewForm(new TreeViewForm(Model));
        }

        private void layoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void listViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewForm(new ListViewForm(Model));
        }

        private void treeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewForm(new TreeViewForm(Model));
        }

        private void AddNewForm(ApplicationForm f)
        {
            f.FormClosed += RemoveFormHandler;
            f.Activated += UpdateStatusStripHandler;
            f.PointsCountChanged += UpdateStatusStripHandler;
            childForms.Add(f);
            f.MdiParent = this;
            f.Show();
        }

        private void UpdateStatusStripHandler(object sender, EventArgs e)
        {
            var count = ((ApplicationForm)sender).PointsCount;
            toolStripStatusLabel1.Text = $"{count} points displayed.";
        }

        private void RemoveFormHandler(object sender, FormClosedEventArgs e)
        {
            var form = (Form)sender;
            childForms.Remove(form);
        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var point = new PointModel(Model);
            var form = new NewForm(point);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Model.Add(point);
            }
        }
    }
}
