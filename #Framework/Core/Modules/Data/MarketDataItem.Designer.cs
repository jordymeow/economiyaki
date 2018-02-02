namespace Finance.Framework.Core
{
    partial class MarketDataItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSecurity = new System.Windows.Forms.TextBox();
            this.picConfig = new System.Windows.Forms.PictureBox();
            this.picStatic = new System.Windows.Forms.PictureBox();
            this.picMisc = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMisc)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSecurity
            // 
            this.txtSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecurity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSecurity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtSecurity.BackColor = System.Drawing.Color.Black;
            this.txtSecurity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSecurity.ForeColor = System.Drawing.Color.White;
            this.txtSecurity.Location = new System.Drawing.Point(9, 0);
            this.txtSecurity.MaxLength = 60;
            this.txtSecurity.Multiline = true;
            this.txtSecurity.Name = "txtSecurity";
            this.txtSecurity.ShortcutsEnabled = false;
            this.txtSecurity.Size = new System.Drawing.Size(167, 17);
            this.txtSecurity.TabIndex = 9;
            this.txtSecurity.Text = "EURJPY=";
            this.txtSecurity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSecurity.TextChanged += new System.EventHandler(this.txtSecurity_TextChanged);
            this.txtSecurity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecurity_KeyPress);
            // 
            // picConfig
            // 
            this.picConfig.BackColor = System.Drawing.Color.Black;
            this.picConfig.Image = global::Finance.Framework.Core.Properties.Resources.bullet_wrench;
            this.picConfig.Location = new System.Drawing.Point(9, 0);
            this.picConfig.Name = "picConfig";
            this.picConfig.Size = new System.Drawing.Size(20, 17);
            this.picConfig.TabIndex = 11;
            this.picConfig.TabStop = false;
            this.picConfig.MouseLeave += new System.EventHandler(this.picConfig_MouseLeave);
            this.picConfig.Click += new System.EventHandler(this.picConfig_Click);
            this.picConfig.MouseEnter += new System.EventHandler(this.picConfig_MouseEnter);
            // 
            // picStatic
            // 
            this.picStatic.BackColor = System.Drawing.Color.Black;
            this.picStatic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picStatic.Location = new System.Drawing.Point(0, 0);
            this.picStatic.Name = "picStatic";
            this.picStatic.Size = new System.Drawing.Size(8, 8);
            this.picStatic.TabIndex = 6;
            this.picStatic.TabStop = false;
            // 
            // picMisc
            // 
            this.picMisc.BackColor = System.Drawing.Color.Black;
            this.picMisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMisc.Location = new System.Drawing.Point(0, 9);
            this.picMisc.Name = "picMisc";
            this.picMisc.Size = new System.Drawing.Size(8, 8);
            this.picMisc.TabIndex = 8;
            this.picMisc.TabStop = false;
            // 
            // MarketDataItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picConfig);
            this.Controls.Add(this.picStatic);
            this.Controls.Add(this.picMisc);
            this.Controls.Add(this.txtSecurity);
            this.DoubleBuffered = true;
            this.Name = "MarketDataItem";
            this.Size = new System.Drawing.Size(176, 16);
            ((System.ComponentModel.ISupportInitialize)(this.picConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMisc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picStatic;
        private System.Windows.Forms.PictureBox picMisc;
        public System.Windows.Forms.TextBox txtSecurity;
        private System.Windows.Forms.PictureBox picConfig;

    }
}
