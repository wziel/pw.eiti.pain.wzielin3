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
    public partial class TreeViewForm : ListViewFormBase
    {
        public override int PointsCount
        {
            get
            {
                return treeView.Nodes.Count;
            }
        }

        public TreeViewForm()
            : base()
        {

        }

        public TreeViewForm(ApplicationModel model)
            : base(model)
        {
            InitializeComponent();
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (treeView.SelectedNode.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        protected override void Hide(PointModel point)
        {
            var i = 0;
            foreach (var obj in treeView.Nodes)
            {
                var node = (TreeNode)obj;
                if (node.Tag == point)
                {
                    treeView.Nodes.RemoveAt(i);
                    return;
                }
                i++;
            }
            throw new InvalidOperationException();
        }

        protected override void Change(PointModel point)
        {
            var i = 0;
            foreach (var obj in treeView.Nodes)
            {
                var node = (TreeNode)obj;
                if (node.Tag == point)
                {
                    node.Text = point.Label;
                    node.Nodes.Clear();
                    node.Nodes.Add(point.X.ToString());
                    node.Nodes.Add(point.Y.ToString());
                    node.Nodes.Add(point.Color.ToString());
                    return;
                }
                i++;
            }
            throw new InvalidOperationException();
        }

        protected override void Display(PointModel point)
        {
            var mainNode = new TreeNode(point.Label);
            mainNode.Nodes.Add(point.X.ToString());
            mainNode.Nodes.Add(point.Y.ToString());
            mainNode.Nodes.Add(point.Color.ToString());
            mainNode.Tag = point;
            treeView.Nodes.Add(mainNode);
        }

        protected override void ClearDisplay(IEnumerable<PointModel> points)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            foreach (var point in points)
            {
                Display(point);
            }
            treeView.EndUpdate();
        }

        protected override IReadOnlyCollection<PointModel> GetSelectedModels()
        {
            if (treeView.SelectedNode == null)
            {
                return new List<PointModel>();
            }
            return new List<PointModel>
            {
                (PointModel)treeView.SelectedNode.Tag
            };
        }
    }
}
