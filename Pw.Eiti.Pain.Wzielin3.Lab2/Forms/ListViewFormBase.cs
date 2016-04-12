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
    public abstract partial class ListViewFormBase : Form
    {
        public event EventHandler PointsCountChanged;
        public abstract int PointsCount{ get; }

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

        private void OnFilterChanged(object sender, EventArgs e)
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
                if(Hide(point))
                {
                    PointsCountChanged?.Invoke(this, null);
                }
            }
        }

        private void PointChanged(object sender, EventArgs e)
        {
            var point = (PointModel)sender;
            if (GetCurrentFilterMethod()(point))
            {
                if(!Change(point))
                {
                    Display(point);
                    PointsCountChanged?.Invoke(this, null);
                }
            }
            else
            {
                Hide(point);
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

        protected abstract bool Change(PointModel point);

        protected abstract bool Hide(PointModel point);

        protected abstract void Display(PointModel point);

        protected abstract void ClearDisplay(IEnumerable<PointModel> points);

        protected abstract IReadOnlyCollection<PointModel> GetSelectedModels();
    }
}
