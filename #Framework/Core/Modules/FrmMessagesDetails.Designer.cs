namespace Finance.Framework.Core.Graphics
{
    partial class FrmMessagesDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessagesDetails));
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.lblMessages = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRemove = new System.Windows.Forms.ToolStripButton();
            this.mnuRemoveAll = new System.Windows.Forms.ToolStripButton();
            this.mnuClose = new System.Windows.Forms.ToolStripButton();
            this.lblPath = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.toolMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.ImageKey = "Note";
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(1, 28);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(397, 174);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Note");
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessages,
            this.toolStripSeparator1,
            this.mnuRemove,
            this.mnuRemoveAll,
            this.mnuClose});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(400, 25);
            this.toolMenu.TabIndex = 1;
            this.toolMenu.Text = "toolStrip1";
            // 
            // lblMessages
            // 
            this.lblMessages.Image = global::Finance.Framework.Core.Properties.Resources.email;
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(29, 22);
            this.lblMessages.Text = "0";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Image = global::Finance.Framework.Core.Properties.Resources.cross;
            this.mnuRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Size = new System.Drawing.Size(89, 22);
            this.mnuRemove.Text = "Remove item";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // mnuRemoveAll
            // 
            this.mnuRemoveAll.Image = global::Finance.Framework.Core.Properties.Resources.bomb;
            this.mnuRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRemoveAll.Name = "mnuRemoveAll";
            this.mnuRemoveAll.Size = new System.Drawing.Size(79, 22);
            this.mnuRemoveAll.Text = "Remove all";
            this.mnuRemoveAll.Click += new System.EventHandler(this.mnuRemoveAll_Click);
            // 
            // mnuClose
            // 
            this.mnuClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuClose.Image = global::Finance.Framework.Core.Properties.Resources.accept;
            this.mnuClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(41, 22);
            this.mnuClose.Text = "OK";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPath.BackColor = System.Drawing.Color.Black;
            this.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPath.ForeColor = System.Drawing.Color.White;
            this.lblPath.Location = new System.Drawing.Point(1, 205);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(397, 23);
            this.lblPath.TabIndex = 2;
            this.lblPath.Text = "...";
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // FrmMessagesDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 229);
            this.ControlBox = false;
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.toolMenu);
            this.Controls.Add(this.treeView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMessagesDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Messages details";
            this.Load += new System.EventHandler(this.FrmMessagesDetails_Load);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripButton mnuRemove;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton mnuRemoveAll;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripLabel lblMessages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mnuClose;
    }
}