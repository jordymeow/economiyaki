namespace Finance.Framework.Core.Excel
{
    partial class MarketSpreadsheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarketSpreadsheet));
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.toolOut = new System.Windows.Forms.ToolStripLabel();
            this.toolInputMessage = new System.Windows.Forms.ToolStripLabel();
            this.mnuUnmanagedInput = new System.Windows.Forms.ToolStripButton();
            this.mnuLinker = new System.Windows.Forms.ToolStripButton();
            this.axSpreadsheet = new AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet();
            this.toolMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSpreadsheet)).BeginInit();
            this.SuspendLayout();
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOut,
            this.toolInputMessage,
            this.mnuUnmanagedInput,
            this.mnuLinker});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(450, 25);
            this.toolMenu.TabIndex = 9;
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
            // mnuUnmanagedInput
            // 
            this.mnuUnmanagedInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuUnmanagedInput.Image = global::Finance.Framework.Core.Properties.Resources.error;
            this.mnuUnmanagedInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuUnmanagedInput.Name = "mnuUnmanagedInput";
            this.mnuUnmanagedInput.Size = new System.Drawing.Size(23, 22);
            this.mnuUnmanagedInput.Text = "Check unmanaged input";
            this.mnuUnmanagedInput.Visible = false;
            this.mnuUnmanagedInput.Click += new System.EventHandler(this.mnuUnmanagedInput_Click);
            // 
            // mnuLinker
            // 
            this.mnuLinker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuLinker.Image = global::Finance.Framework.Core.Properties.Resources.table_row_insert;
            this.mnuLinker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuLinker.Name = "mnuLinker";
            this.mnuLinker.Size = new System.Drawing.Size(23, 22);
            this.mnuLinker.Text = "toolStripButton1";
            this.mnuLinker.Click += new System.EventHandler(this.mnuLinker_Click);
            // 
            // axSpreadsheet
            // 
            this.axSpreadsheet.DataSource = null;
            this.axSpreadsheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSpreadsheet.Enabled = true;
            this.axSpreadsheet.Location = new System.Drawing.Point(0, 25);
            this.axSpreadsheet.Name = "axSpreadsheet";
            this.axSpreadsheet.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSpreadsheet.OcxState")));
            this.axSpreadsheet.Size = new System.Drawing.Size(450, 225);
            this.axSpreadsheet.TabIndex = 10;
            // 
            // MarketSpreadsheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axSpreadsheet);
            this.Controls.Add(this.toolMenu);
            this.Name = "MarketSpreadsheet";
            this.Size = new System.Drawing.Size(450, 250);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSpreadsheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripLabel toolOut;
        private System.Windows.Forms.ToolStripLabel toolInputMessage;
        private AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet axSpreadsheet;
        private System.Windows.Forms.ToolStripButton mnuLinker;
        private System.Windows.Forms.ToolStripButton mnuUnmanagedInput;
    }
}
