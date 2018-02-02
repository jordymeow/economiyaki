namespace Finance.Framework.Core
{
    partial class FrmDataItemSettings
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
            this.pnlWizard = new System.Windows.Forms.Panel();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdAddHistoryField = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbHistoryField = new System.Windows.Forms.ComboBox();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.lstFields = new System.Windows.Forms.ListView();
            this.colField = new System.Windows.Forms.ColumnHeader();
            this.btnAddField = new System.Windows.Forms.Button();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.txtEquity = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbMarketDataLib = new System.Windows.Forms.ComboBox();
            this.txtParam = new System.Windows.Forms.TextBox();
            this.lblMarketAccess = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlWizard.SuspendLayout();
            this.grpHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWizard
            // 
            this.pnlWizard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWizard.Controls.Add(this.grpHistory);
            this.pnlWizard.Controls.Add(this.lstFields);
            this.pnlWizard.Controls.Add(this.btnAddField);
            this.pnlWizard.Controls.Add(this.cmbField);
            this.pnlWizard.Controls.Add(this.txtEquity);
            this.pnlWizard.Location = new System.Drawing.Point(0, 29);
            this.pnlWizard.Name = "pnlWizard";
            this.pnlWizard.Size = new System.Drawing.Size(296, 171);
            this.pnlWizard.TabIndex = 4;
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.label5);
            this.grpHistory.Controls.Add(this.label6);
            this.grpHistory.Controls.Add(this.cmdAddHistoryField);
            this.grpHistory.Controls.Add(this.label4);
            this.grpHistory.Controls.Add(this.cmbHistoryField);
            this.grpHistory.Controls.Add(this.dateTimeTo);
            this.grpHistory.Controls.Add(this.dateTimeFrom);
            this.grpHistory.Enabled = false;
            this.grpHistory.Location = new System.Drawing.Point(6, 28);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Size = new System.Drawing.Size(135, 137);
            this.grpHistory.TabIndex = 36;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "History";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "From...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "To...";
            // 
            // cmdAddHistoryField
            // 
            this.cmdAddHistoryField.Image = global::Finance.Framework.Core.Properties.Resources.add;
            this.cmdAddHistoryField.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdAddHistoryField.Location = new System.Drawing.Point(102, 101);
            this.cmdAddHistoryField.Name = "cmdAddHistoryField";
            this.cmdAddHistoryField.Size = new System.Drawing.Size(26, 27);
            this.cmdAddHistoryField.TabIndex = 30;
            this.cmdAddHistoryField.UseVisualStyleBackColor = true;
            this.cmdAddHistoryField.Click += new System.EventHandler(this.cmdAddHistoryField_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Additional fields:";
            // 
            // cmbHistoryField
            // 
            this.cmbHistoryField.BackColor = System.Drawing.Color.LightSteelBlue;
            this.cmbHistoryField.FormattingEnabled = true;
            this.cmbHistoryField.Location = new System.Drawing.Point(9, 107);
            this.cmbHistoryField.Name = "cmbHistoryField";
            this.cmbHistoryField.Size = new System.Drawing.Size(87, 21);
            this.cmbHistoryField.TabIndex = 29;
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeTo.Location = new System.Drawing.Point(9, 69);
            this.dateTimeTo.MaxDate = new System.DateTime(2008, 9, 9, 0, 0, 0, 0);
            this.dateTimeTo.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(119, 20);
            this.dateTimeTo.TabIndex = 33;
            this.dateTimeTo.Value = new System.DateTime(2008, 9, 2, 0, 0, 0, 0);
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeFrom.Location = new System.Drawing.Point(9, 33);
            this.dateTimeFrom.MaxDate = new System.DateTime(2008, 9, 9, 0, 0, 0, 0);
            this.dateTimeFrom.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(119, 20);
            this.dateTimeFrom.TabIndex = 32;
            this.dateTimeFrom.Value = new System.DateTime(2008, 9, 2, 0, 0, 0, 0);
            // 
            // lstFields
            // 
            this.lstFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colField});
            this.lstFields.FullRowSelect = true;
            this.lstFields.GridLines = true;
            this.lstFields.Location = new System.Drawing.Point(147, 31);
            this.lstFields.MultiSelect = false;
            this.lstFields.Name = "lstFields";
            this.lstFields.ShowGroups = false;
            this.lstFields.Size = new System.Drawing.Size(144, 134);
            this.lstFields.TabIndex = 31;
            this.lstFields.UseCompatibleStateImageBehavior = false;
            this.lstFields.View = System.Windows.Forms.View.Details;
            this.lstFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFields_MouseDoubleClick);
            // 
            // colField
            // 
            this.colField.Text = "Field";
            this.colField.Width = 103;
            // 
            // btnAddField
            // 
            this.btnAddField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddField.Image = global::Finance.Framework.Core.Properties.Resources.add;
            this.btnAddField.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddField.Location = new System.Drawing.Point(265, -2);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(26, 27);
            this.btnAddField.TabIndex = 26;
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // cmbField
            // 
            this.cmbField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbField.BackColor = System.Drawing.Color.Khaki;
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(147, 1);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(112, 21);
            this.cmbField.TabIndex = 25;
            // 
            // txtEquity
            // 
            this.txtEquity.Location = new System.Drawing.Point(6, 2);
            this.txtEquity.Name = "txtEquity";
            this.txtEquity.Size = new System.Drawing.Size(135, 20);
            this.txtEquity.TabIndex = 24;
            this.txtEquity.Text = "EURJPY=";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Image = global::Finance.Framework.Core.Properties.Resources.accept;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(6, 206);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(285, 26);
            this.btnOk.TabIndex = 41;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbMarketDataLib
            // 
            this.cmbMarketDataLib.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMarketDataLib.DisplayMember = "Reuters";
            this.cmbMarketDataLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarketDataLib.FormattingEnabled = true;
            this.cmbMarketDataLib.Location = new System.Drawing.Point(33, 2);
            this.cmbMarketDataLib.Name = "cmbMarketDataLib";
            this.cmbMarketDataLib.Size = new System.Drawing.Size(110, 21);
            this.cmbMarketDataLib.TabIndex = 43;
            // 
            // txtParam
            // 
            this.txtParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParam.Location = new System.Drawing.Point(195, 2);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(96, 20);
            this.txtParam.TabIndex = 44;
            // 
            // lblMarketAccess
            // 
            this.lblMarketAccess.AutoSize = true;
            this.lblMarketAccess.Location = new System.Drawing.Point(3, 6);
            this.lblMarketAccess.Name = "lblMarketAccess";
            this.lblMarketAccess.Size = new System.Drawing.Size(24, 13);
            this.lblMarketAccess.TabIndex = 42;
            this.lblMarketAccess.Text = "Lib:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Param:";
            // 
            // FrmDataItemSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(296, 239);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.cmbMarketDataLib);
            this.Controls.Add(this.lblMarketAccess);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pnlWizard);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDataItemSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MultiData Flux";
            this.pnlWizard.ResumeLayout(false);
            this.pnlWizard.PerformLayout();
            this.grpHistory.ResumeLayout(false);
            this.grpHistory.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlWizard;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdAddHistoryField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbHistoryField;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.ListView lstFields;
        private System.Windows.Forms.ColumnHeader colField;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.TextBox txtEquity;
        private System.Windows.Forms.ComboBox cmbMarketDataLib;
        private System.Windows.Forms.TextBox txtParam;
        private System.Windows.Forms.Label lblMarketAccess;
        private System.Windows.Forms.Label label1;
    }
}