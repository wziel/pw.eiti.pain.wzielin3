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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var form = new ListViewForm(Model);
            form.MdiParent = this;
            form.Show();

            var form2 = new TreeViewForm(Model);
            form2.MdiParent = this;
            form2.Show();
        }

        private void layoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
