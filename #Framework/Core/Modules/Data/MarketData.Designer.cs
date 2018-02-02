namespace Finance.Framework.Core
{
    partial class MarketData
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
            this.components = new System.ComponentModel.Container();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.toolOut = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolOutputMessage = new System.Windows.Forms.ToolStripButton();
            this.mnuStart = new System.Windows.Forms.ToolStripButton();
            this.mnuMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.realtimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTimer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAdd = new System.Windows.Forms.ToolStripButton();
            this.mnuRemove = new System.Windows.Forms.ToolStripButton();
            this.toolInputMessage = new System.Windows.Forms.ToolStripLabel();
            this.pnlDataItem = new System.Windows.Forms.FlowLayoutPanel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOut,
            this.toolStripSeparator2,
            this.toolOutputMessage,
            this.mnuStart,
            this.mnuMode,
            this.mnuTimer,
            this.toolStripSeparator3,
            this.mnuAdd,
            this.mnuRemove,
            this.toolInputMessage});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(250, 25);
            this.toolMenu.TabIndex = 0;
            this.toolMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip_MouseDown);
            // 
            // toolOut
            // 
            this.toolOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolOut.Image = global::Finance.Framework.Core.Properties.Resources.RelationshipsHS;
            this.toolOut.Name = "toolOut";
            this.toolOut.Size = new System.Drawing.Size(16, 22);
            this.toolOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolOut_MouseDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolOutputMessage
            // 
            this.toolOutputMessage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolOutputMessage.Image = global::Finance.Framework.Core.Properties.Resources.email;
            this.toolOutputMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOutputMessage.Name = "toolOutputMessage";
            this.toolOutputMessage.Size = new System.Drawing.Size(33, 22);
            this.toolOutputMessage.Text = "0";
            this.toolOutputMessage.Visible = false;
            this.toolOutputMessage.Click += new System.EventHandler(this.toolOutputMessage_Click);
            // 
            // mnuStart
            // 
            this.mnuStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuStart.Image = global::Finance.Framework.Core.Properties.Resources.resultset_next;
            this.mnuStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.Size = new System.Drawing.Size(23, 22);
            this.mnuStart.Text = "Play";
            this.mnuStart.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // mnuMode
            // 
            this.mnuMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.realtimeToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.staticToolStripMenuItem});
            this.mnuMode.Image = global::Finance.Framework.Core.Properties.Resources.control_equalizer_blue;
            this.mnuMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuMode.Name = "mnuMode";
            this.mnuMode.Size = new System.Drawing.Size(43, 22);
            this.mnuMode.Text = "R";
            this.mnuMode.ToolTipText = "Mode";
            // 
            // realtimeToolStripMenuItem
            // 
            this.realtimeToolStripMenuItem.Checked = true;
            this.realtimeToolStripMenuItem.CheckOnClick = true;
            this.realtimeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.realtimeToolStripMenuItem.Name = "realtimeToolStripMenuItem";
            this.realtimeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.realtimeToolStripMenuItem.Text = "Realtime";
            this.realtimeToolStripMenuItem.ToolTipText = "RT";
            this.realtimeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ModeMenuItem_CheckedChanged);
            this.realtimeToolStripMenuItem.Click += new System.EventHandler(this.ModeMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.CheckOnClick = true;
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ModeMenuItem_CheckedChanged);
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.ModeMenuItem_Click);
            // 
            // staticToolStripMenuItem
            // 
            this.staticToolStripMenuItem.CheckOnClick = true;
            this.staticToolStripMenuItem.Name = "staticToolStripMenuItem";
            this.staticToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.staticToolStripMenuItem.Text = "Static";
            this.staticToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ModeMenuItem_CheckedChanged);
            this.staticToolStripMenuItem.Click += new System.EventHandler(this.ModeMenuItem_Click);
            // 
            // mnuTimer
            // 
            this.mnuTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuTimer.Image = global::Finance.Framework.Core.Properties.Resources.ExpirationHS;
            this.mnuTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuTimer.Name = "mnuTimer";
            this.mnuTimer.Size = new System.Drawing.Size(23, 22);
            this.mnuTimer.Text = "Timer";
            this.mnuTimer.Click += new System.EventHandler(this.mnuTimer_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuAdd
            // 
            this.mnuAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuAdd.Image = global::Finance.Framework.Core.Properties.Resources.add;
            this.mnuAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.Size = new System.Drawing.Size(23, 22);
            this.mnuAdd.Text = "Add";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuRemove.Image = global::Finance.Framework.Core.Properties.Resources.delete;
            this.mnuRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Size = new System.Drawing.Size(23, 22);
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.Visible = false;
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // toolInputMessage
            // 
            this.toolInputMessage.Name = "toolInputMessage";
            this.toolInputMessage.Size = new System.Drawing.Size(0, 22);
            // 
            // pnlDataItem
            // 
            this.pnlDataItem.AutoScroll = true;
            this.pnlDataItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlDataItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDataItem.Location = new System.Drawing.Point(0, 25);
            this.pnlDataItem.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDataItem.Name = "pnlDataItem";
            this.pnlDataItem.Size = new System.Drawing.Size(250, 27);
            this.pnlDataItem.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MarketData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlDataItem);
            this.Controls.Add(this.toolMenu);
            this.MinimumSize = new System.Drawing.Size(0, 25);
            this.Name = "MarketData";
            this.Size = new System.Drawing.Size(250, 52);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripLabel toolOut;
        private System.Windows.Forms.ToolStripButton mnuStart;
        private System.Windows.Forms.ToolStripLabel toolInputMessage;
        private System.Windows.Forms.ToolStripButton toolOutputMessage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuAdd;
        private System.Windows.Forms.ToolStripButton mnuRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.FlowLayoutPanel pnlDataItem;
        private System.Windows.Forms.ToolStripButton mnuTimer;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripDropDownButton mnuMode;
        private System.Windows.Forms.ToolStripMenuItem staticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realtimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;

    }
}
