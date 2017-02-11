namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    partial class NewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lblX = new System.Windows.Forms.Label();
            this.lblLabel = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.colorControl = new Pw.Eiti.Pain.Wzielin3.Lab2.ColorControl();
            this.lblColorValue = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CausesValidation = false;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.txtY, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtX, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblX, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblY, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblColor, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(499, 287);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtY
            // 
            this.txtY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtY.Location = new System.Drawing.Point(53, 55);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(403, 20);
            this.txtY.TabIndex = 6;
            this.txtY.Validating += new System.ComponentModel.CancelEventHandler(this.txtInteger_Validating);
            // 
            // txtX
            // 
            this.txtX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtX.Location = new System.Drawing.Point(53, 29);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(403, 20);
            this.txtX.TabIndex = 5;
            this.txtX.Validating += new System.ComponentModel.CancelEventHandler(this.txtInteger_Validating);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblX.Location = new System.Drawing.Point(3, 39);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(44, 13);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X";
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLabel.Location = new System.Drawing.Point(3, 13);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(44, 13);
            this.lblLabel.TabIndex = 1;
            this.lblLabel.Text = "Label";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblY.Location = new System.Drawing.Point(3, 65);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(44, 13);
            this.lblY.TabIndex = 2;
            this.lblY.Text = "Y";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblColor.Location = new System.Drawing.Point(3, 233);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(44, 13);
            this.lblColor.TabIndex = 3;
            this.lblColor.Text = "Color";
            // 
            // txtLabel
            // 
            this.txtLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLabel.Location = new System.Drawing.Point(53, 3);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(403, 20);
            this.txtLabel.TabIndex = 4;
            this.txtLabel.Validating += new System.ComponentModel.CancelEventHandler(this.txtLabel_Validating);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CausesValidation = false;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(53, 249);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(403, 35);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 24);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(83, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Controls.Add(this.colorControl, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblColorValue, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(50, 78);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(409, 168);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // colorControl
            // 
            this.colorControl.AutoSize = true;
            this.colorControl.ColorType = ColorType.Red;
            this.colorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorControl.Location = new System.Drawing.Point(3, 3);
            this.colorControl.MinimumSize = new System.Drawing.Size(100, 20);
            this.colorControl.Name = "colorControl";
            this.colorControl.Size = new System.Drawing.Size(323, 162);
            this.colorControl.TabIndex = 8;
            // 
            // lblColorValue
            // 
            this.lblColorValue.AutoSize = true;
            this.lblColorValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblColorValue.Location = new System.Drawing.Point(332, 155);
            this.lblColorValue.Name = "lblColorValue";
            this.lblColorValue.Size = new System.Drawing.Size(74, 13);
            this.lblColorValue.TabIndex = 9;
            this.lblColorValue.Text = "lblColorValue";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // NewForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(499, 287);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(360, 160);
            this.Name = "NewForm";
            this.Text = "NewForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private ColorControl colorControl;
        private System.Windows.Forms.Label lblColorValue;
    }
}