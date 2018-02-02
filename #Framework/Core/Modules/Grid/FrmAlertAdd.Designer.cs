namespace Finance.Framework.Core
{
    partial class FrmAlertAdd
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
            this.cbo = new System.Windows.Forms.ComboBox();
            this.cboComparison = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.rdoFixValue = new System.Windows.Forms.RadioButton();
            this.rdoCell = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbo
            // 
            this.cbo.FormattingEnabled = true;
            this.cbo.Location = new System.Drawing.Point(12, 12);
            this.cbo.Name = "cbo";
            this.cbo.Size = new System.Drawing.Size(135, 21);
            this.cbo.TabIndex = 0;
            // 
            // cboComparison
            // 
            this.cboComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComparison.FormattingEnabled = true;
            this.cboComparison.Items.AddRange(new object[] {
            ">",
            "<",
            "=="});
            this.cboComparison.Location = new System.Drawing.Point(153, 12);
            this.cboComparison.Name = "cboComparison";
            this.cboComparison.Size = new System.Drawing.Size(49, 21);
            this.cboComparison.TabIndex = 1;
            // 
            // comboBox3
            // 
            this.comboBox3.Enabled = false;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(89, 63);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(113, 21);
            this.comboBox3.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Image = global::Finance.Framework.Core.Properties.Resources.clock_add;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(8, 120);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(196, 24);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // rdoFixValue
            // 
            this.rdoFixValue.AutoSize = true;
            this.rdoFixValue.Checked = true;
            this.rdoFixValue.Location = new System.Drawing.Point(12, 39);
            this.rdoFixValue.Name = "rdoFixValue";
            this.rdoFixValue.Size = new System.Drawing.Size(52, 17);
            this.rdoFixValue.TabIndex = 4;
            this.rdoFixValue.TabStop = true;
            this.rdoFixValue.Text = "Value";
            this.rdoFixValue.UseVisualStyleBackColor = true;
            // 
            // rdoCell
            // 
            this.rdoCell.AutoSize = true;
            this.rdoCell.Location = new System.Drawing.Point(12, 64);
            this.rdoCell.Name = "rdoCell";
            this.rdoCell.Size = new System.Drawing.Size(42, 17);
            this.rdoCell.TabIndex = 5;
            this.rdoCell.Text = "Cell";
            this.rdoCell.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(113, 20);
            this.textBox1.TabIndex = 6;
            // 
            // comboBox4
            // 
            this.comboBox4.Enabled = false;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(89, 90);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(113, 21);
            this.comboBox4.TabIndex = 7;
            // 
            // FrmAlertAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 152);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rdoCell);
            this.Controls.Add(this.rdoFixValue);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.cboComparison);
            this.Controls.Add(this.cbo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAlertAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Add an alert";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo;
        private System.Windows.Forms.ComboBox cboComparison;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rdoFixValue;
        private System.Windows.Forms.RadioButton rdoCell;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox4;
    }
}