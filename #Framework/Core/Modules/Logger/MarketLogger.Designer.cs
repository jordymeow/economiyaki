namespace Finance.Framework.Core
{
    partial class MarketLogger
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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.toolOut = new System.Windows.Forms.ToolStripLabel();
            this.toolInputMessage = new System.Windows.Forms.ToolStripLabel();
            this.toolClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.AutoWordSelection = true;
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.DetectUrls = false;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(0, 25);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(300, 115);
            this.txtLog.TabIndex = 7;
            this.txtLog.Text = "";
            this.txtLog.WordWrap = false;
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOut,
            this.toolInputMessage,
            this.toolClear,
            this.toolStripSeparator1});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(300, 25);
            this.toolMenu.TabIndex = 8;
            this.toolMenu.Text = "toolStrip1";
            this.toolMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip_MouseDown);
            // 
            // toolOut
            // 
            this.toolOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolOut.Image = global::Finance.Framework.Core.Properties.Resources.RelationshipsHS;
            this.toolOut.Name = "toolOut";
            this.toolOut.Size = new System.Drawing.Size(16, 22);
            // 
            // toolInputMessage
            // 
            this.toolInputMessage.Name = "toolInputMessage";
            this.toolInputMessage.Size = new System.Drawing.Size(0, 22);
            // 
            // toolClear
            // 
            this.toolClear.Image = global::Finance.Framework.Core.Properties.Resources.application_xp_terminal;
            this.toolClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClear.Name = "toolClear";
            this.toolClear.Size = new System.Drawing.Size(52, 22);
            this.toolClear.Text = "Clear";
            this.toolClear.Click += new System.EventHandler(this.toolClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // MarketLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.toolMenu);
            this.Name = "MarketLogger";
            this.Size = new System.Drawing.Size(300, 140);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripLabel toolOut;
        private System.Windows.Forms.ToolStripLabel toolInputMessage;
        private System.Windows.Forms.ToolStripButton toolClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
