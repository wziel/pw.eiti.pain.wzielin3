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
        public ICollection<Form> childForms { get; set; }

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

        private void AddNewForm(Form f)
        {
            f.MdiParent = this;
            f.Show();
            Model.FormsCount++;
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
