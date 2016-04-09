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
    public partial class ListViewForm : ApplicationForm
    {
        public ListViewForm() :base()
        {

        }

        public ListViewForm(ApplicationModel model)
            : base(model)
        {
            InitializeComponent();// Create a new ListView control.

            listView.BeginUpdate();
            foreach (var point in model.Points)
            {
                var item = new ListViewItem(point.Label);
                item.SubItems.Add(point.X.ToString());
                item.SubItems.Add(point.Y.ToString());
                item.SubItems.Add(point.Color.ToString());
                item.Tag = point;
                listView.Items.Add(item);
            }
            listView.EndUpdate();
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }
        
        protected override IReadOnlyCollection<PointModel> GetSelectedModels()
        {
            return listView.SelectedItems.Cast<ListViewItem>().Select(i => (PointModel)i.Tag).ToList();
        }
        
        protected override void PointAdded(object sender, EventArgs e)
        {
            base.PointAdded(sender, e);
            var point = (PointModel)sender;
            var item = new ListViewItem(point.Label);
            item.SubItems.Add(point.X.ToString());
            item.SubItems.Add(point.Y.ToString());
            item.SubItems.Add(point.Color.ToString());
            item.Tag = point;
            listView.Items.Add(item);
        }

        protected override void PointRemoved(object sender, EventArgs e)
        {
            base.PointRemoved(sender, e);
            var point = (PointModel)sender;
            var i = 0;
            foreach(var obj in listView.Items)
            {
                var item = (ListViewItem)obj;
                if(item.Tag == point)
                {
                    listView.Items.RemoveAt(i);
                    return;
                }
                ++i;
            }
        }

        protected override void PointChanged(object sender, EventArgs e)
        {
            base.PointRemoved(sender, e);
            var point = (PointModel)sender;
            var i = 0;
            foreach (var obj in listView.Items)
            {
                var item = (ListViewItem)obj;
                if (item.Tag == point)
                {
                    item.SubItems.Clear();
                    item.Text = point.Label;
                    item.SubItems.Add(point.X.ToString());
                    item.SubItems.Add(point.Y.ToString());
                    item.SubItems.Add(point.Color.ToString());
                    return;
                }
                ++i;
            }
        }
    }
}
