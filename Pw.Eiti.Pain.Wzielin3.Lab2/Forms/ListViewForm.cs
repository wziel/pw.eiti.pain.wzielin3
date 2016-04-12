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
    public partial class ListViewForm : ListViewFormBase
    {
        public override int PointsCount
        {
            get
            {
                return listView.Items.Count;
            }
        }

        public ListViewForm() :base()
        {

        }

        public ListViewForm(ApplicationModel model)
            : base(model)
        {
            InitializeComponent();// Create a new ListView control.
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
        
        protected override void Hide(PointModel point)
        {
            var i = 0;
            foreach (var obj in listView.Items)
            {
                var item = (ListViewItem)obj;
                if (item.Tag == point)
                {
                    listView.Items.RemoveAt(i);
                    return;
                }
                ++i;
            }
            throw new InvalidOperationException();
        }

        protected override void Display(PointModel point)
        {
            var item = new ListViewItem(point.Label);
            item.SubItems.Add(point.X.ToString());
            item.SubItems.Add(point.Y.ToString());
            item.SubItems.Add(point.Color.ToString());
            item.Tag = point;
            listView.Items.Add(item);
        }

        protected override void Change(PointModel point)
        {
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
            throw new InvalidOperationException();
        }

        protected override void ClearDisplay(IEnumerable<PointModel> points)
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach(var point in points)
            {
                Display(point);
            }
            listView.EndUpdate();
        }

        protected override IReadOnlyCollection<PointModel> GetSelectedModels()
        {
            return listView.SelectedItems.Cast<ListViewItem>().Select(i => (PointModel)i.Tag).ToList();
        }
    }
}
