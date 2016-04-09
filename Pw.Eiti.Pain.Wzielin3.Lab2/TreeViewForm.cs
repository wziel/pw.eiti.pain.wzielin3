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
    public partial class TreeViewForm : Form
    {
        internal TreeViewForm(ApplicationModel model)
        {
            InitializeComponent();
            treeView1.BeginUpdate();
            foreach (var point in model.Models)
            {
                var mainNode = new TreeNode(point.Label);
                mainNode.Nodes.Add(point.X.ToString());
                mainNode.Nodes.Add(point.Y.ToString());
                mainNode.Nodes.Add(point.Color.ToString());
                treeView1.Nodes.Add(mainNode);
            }
            treeView1.EndUpdate();
        }
    }
}
