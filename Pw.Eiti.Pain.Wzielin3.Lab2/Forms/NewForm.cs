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
    public partial class NewForm : Form
    {
        private PointModel model;

        public NewForm()
        {
            InitializeComponent();
        }

        public NewForm(PointModel model)
        {
            InitializeComponent();
            this.model = model;
            txtLabel.Text = model.Label;
            txtX.Text = model.X.ToString();
            txtY.Text = model.Y.ToString();
            colorControl.ColorType = model.Color;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(ValidateChildren())
            {
                model.Label = txtLabel.Text;
                model.X = int.Parse(txtX.Text);
                model.Y = int.Parse(txtY.Text);
                model.Color = colorControl.ColorType;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void txtInteger_Validating(object sender, CancelEventArgs e)
        {
            var txt = (TextBox)sender;
            int value;
            if(txt.Text.Length == 0 || !int.TryParse(txt.Text, out value))
            {
                e.Cancel = true;
                errorProvider.SetError(txt, "Wprowadź liczbę.");
            }
        }

        private void txtLabel_Validating(object sender, CancelEventArgs e)
        {
            var txt = (TextBox)sender;
            if(txt.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(txt, "Pole nie może być puste.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
