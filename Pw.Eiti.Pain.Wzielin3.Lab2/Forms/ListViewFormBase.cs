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
    public partial class ListViewFormBase : Form
    {
        public virtual event EventHandler PointsCountChanged;
        public virtual int PointsCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected ApplicationModel ApplicationModel { get; private set; }
        private enum FilterType
        {
            AllItems,
            NonNegativeXItems,
            NegativeXitems,
        }
        private Dictionary<int, FilterType> indexToFilterType = new Dictionary<int, FilterType>
        {
            {0, FilterType.AllItems },
            {1, FilterType.NonNegativeXItems },
            {2, FilterType.NegativeXitems }
        };
        private FilterType Filter
        {
            get
            {
                return indexToFilterType[toolStripComboBox.SelectedIndex];
            }
        }
        private Dictionary<FilterType, Func<PointModel, bool>> FilterToCheckMethod
            = new Dictionary<FilterType, Func<PointModel, bool>>
            {
                {FilterType.AllItems, (p) => true },
                {FilterType.NonNegativeXItems, (p) => p.X >= 0 },
                {FilterType.NegativeXitems, (p) => p.X < 0 },
            };
        private Func<PointModel, bool> GetCurrentFilterMethod()
        {
            return FilterToCheckMethod[Filter];
        }

        public ListViewFormBase()
        {
            InitializeComponent();
        }

        public ListViewFormBase(ApplicationModel model)
        {
            InitializeComponent();
            toolStripComboBox.SelectedIndex = 0;
            toolStripComboBox.SelectedIndexChanged += OnFilterChanged;

            ApplicationModel = model;
            model.PointAdded += PointAdded;
            model.PointRemoved += PointRemoved;
            model.PointChanged += PointChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var filterMethod = GetCurrentFilterMethod();
            var points = ApplicationModel.Points.Where(p => filterMethod(p));
            ClearDisplay(points);
            PointsCountChanged?.Invoke(this, null);
        }

        protected virtual void OnFilterChanged(object sender, EventArgs e)
        {
            var filterMethod = GetCurrentFilterMethod();
            var points = ApplicationModel.Points.Where(p => filterMethod(p));
            ClearDisplay(points);
            PointsCountChanged?.Invoke(this, null);
        }

        private void PointAdded(object sender, EventArgs e)
        {
            var point = (PointModel)sender;
            if (GetCurrentFilterMethod()(point))
            {
                Display(point);
                PointsCountChanged?.Invoke(this, null);
            }
        }

        private void PointRemoved(object sender, EventArgs e)
        {
            var point = (PointModel)sender;
            if (GetCurrentFilterMethod()(point))
            {
                Hide(point);
                PointsCountChanged?.Invoke(this, null);
            }
        }

        private void PointChanged(object sender, EventArgs e)
        {
            var point = (PointModel)sender;
            if (GetCurrentFilterMethod()(point))
            {
                Change(point);
            }
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedModels();
            if (selected.Count != 0)
            {
                var form = new NewForm(selected.First());
                form.ShowDialog();
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedModels();
            foreach (var model in selected)
            {
                ApplicationModel.Remove(model);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var parent = (MDIContainer)Parent.Parent;
            if (parent.childFormsCount <= 1)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
            }
        }

        protected virtual void Change(PointModel point)
        {
            throw new NotImplementedException();
        }

        protected virtual void Hide(PointModel point)
        {
            throw new NotImplementedException();
        }

        protected virtual void Display(PointModel point)
        {
            throw new NotImplementedException();
        }

        protected virtual void ClearDisplay(IEnumerable<PointModel> points)
        {
            throw new NotImplementedException();
        }

        protected virtual IReadOnlyCollection<PointModel> GetSelectedModels()
        {
            throw new NotImplementedException();
        }
    }
}
