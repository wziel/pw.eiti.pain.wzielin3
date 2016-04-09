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
        public ListViewForm(ApplicationModel model)
            : base(model)
        {
            InitializeComponent();// Create a new ListView control.

            listView1.BeginUpdate();
            foreach (var point in model.Points)
            {
                var item = new ListViewItem(point.Label);
                item.SubItems.Add(point.X.ToString());
                item.SubItems.Add(point.Y.ToString());
                item.SubItems.Add(point.Color.ToString());
                item.Tag = point;
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }
    }
}
