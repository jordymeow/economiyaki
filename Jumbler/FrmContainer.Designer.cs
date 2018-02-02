namespace Finance.Jumbler
{
    partial class FrmContainer
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
            this.pnlControlContainer = new System.Windows.Forms.Panel();
            this.lblTitle = new randz.CustomControls.VerticalLabel();
            this.picGrow = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picGrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControlContainer
            // 
            this.pnlControlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlControlContainer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlControlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControlContainer.Location = new System.Drawing.Point(17, 2);
            this.pnlControlContainer.Name = "pnlControlContainer";
            this.pnlControlContainer.Size = new System.Drawing.Size(274, 87);
            this.pnlControlContainer.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTitle.Location = new System.Drawing.Point(2, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(15, 72);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Container";
            this.lblTitle.TextDrawMode = randz.CustomControls.DrawMode.BottomUp;
            // 
            // picGrow
            // 
            this.picGrow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picGrow.Image = global::Finance.Jumbler.Properties.Resources.magnifier_zoom_in;
            this.picGrow.Location = new System.Drawing.Point(1, 16);
            this.picGrow.Name = "picGrow";
            this.picGrow.Size = new System.Drawing.Size(15, 15);
            this.picGrow.TabIndex = 2;
            this.picGrow.TabStop = false;
            this.picGrow.MouseLeave += new System.EventHandler(this.picGrow_MouseLeave);
            this.picGrow.Click += new System.EventHandler(this.picGrow_Click);
            this.picGrow.MouseEnter += new System.EventHandler(this.picGrow_MouseEnter);
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picClose.Image = global::Finance.Jumbler.Properties.Resources.cross;
            this.picClose.Location = new System.Drawing.Point(1, 1);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(16, 16);
            this.picClose.TabIndex = 1;
            this.picClose.TabStop = false;
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseEnter += new System.EventHandler(this.picClose_MouseEnter);
            // 
            // FrmContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(293, 91);
            this.Controls.Add(this.picGrow);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlControlContainer);
            this.Controls.Add(this.picClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmContainer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.picGrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private randz.CustomControls.VerticalLabel lblTitle;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Panel pnlControlContainer;
        private System.Windows.Forms.PictureBox picGrow;
    }
}