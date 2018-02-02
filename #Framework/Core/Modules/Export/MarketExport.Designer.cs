namespace Finance.Framework.Core
{
    partial class MarketExport
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Data", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("MultiData", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("HistoryData", System.Windows.Forms.HorizontalAlignment.Left);
            this.lstStocks = new System.Windows.Forms.ListView();
            this.colStock = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colFirstDate = new System.Windows.Forms.ColumnHeader();
            this.colLastDate = new System.Windows.Forms.ColumnHeader();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.mnuClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolOut = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cboSpeed = new System.Windows.Forms.ToolStripComboBox();
            this.mnuSpeed = new System.Windows.Forms.ToolStripLabel();
            this.mnuPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLoad = new System.Windows.Forms.ToolStripDropDownButton();
            this.dataItemToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.multiDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.historyDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.dataItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picLoad = new System.Windows.Forms.PictureBox();
            this.toolMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // lstStocks
            // 
            this.lstStocks.AllowDrop = true;
            this.lstStocks.CheckBoxes = true;
            this.lstStocks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStock,
            this.colSize,
            this.colFirstDate,
            this.colLastDate});
            this.lstStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStocks.GridLines = true;
            listViewGroup1.Header = "Data";
            listViewGroup1.Name = "Data";
            listViewGroup2.Header = "MultiData";
            listViewGroup2.Name = "MultiData";
            listViewGroup3.Header = "HistoryData";
            listViewGroup3.Name = "HistoryData";
            this.lstStocks.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.lstStocks.Location = new System.Drawing.Point(0, 25);
            this.lstStocks.Name = "lstStocks";
            this.lstStocks.Size = new System.Drawing.Size(451, 180);
            this.lstStocks.TabIndex = 2;
            this.lstStocks.UseCompatibleStateImageBehavior = false;
            this.lstStocks.View = System.Windows.Forms.View.Details;
            // 
            // colStock
            // 
            this.colStock.Text = "Stock";
            this.colStock.Width = 160;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 38;
            // 
            // colFirstDate
            // 
            this.colFirstDate.Text = "Start";
            this.colFirstDate.Width = 120;
            // 
            // colLastDate
            // 
            this.colLastDate.Text = "End";
            this.colLastDate.Width = 120;
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClear,
            this.toolStripSeparator2,
            this.toolOut,
            this.toolStripSeparator1,
            this.cboSpeed,
            this.mnuSpeed,
            this.mnuPlay,
            this.toolStripSeparator3,
            this.mnuLoad,
            this.mnuSave});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(451, 25);
            this.toolMenu.TabIndex = 3;
            this.toolMenu.Text = "toolStrip1";
            this.toolMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip_MouseDown);
            // 
            // mnuClear
            // 
            this.mnuClear.Image = global::Finance.Framework.Core.Properties.Resources.table;
            this.mnuClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(52, 22);
            this.mnuClear.Text = "Clear";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolOut
            // 
            this.toolOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolOut.Image = global::Finance.Framework.Core.Properties.Resources.RelationshipsHS;
            this.toolOut.Name = "toolOut";
            this.toolOut.Size = new System.Drawing.Size(16, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cboSpeed
            // 
            this.cboSpeed.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cboSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpeed.DropDownWidth = 60;
            this.cboSpeed.Items.AddRange(new object[] {
            "x1",
            "x2",
            "x10",
            "x60",
            "x600",
            "x3600",
            "Instant"});
            this.cboSpeed.Name = "cboSpeed";
            this.cboSpeed.Size = new System.Drawing.Size(75, 25);
            this.cboSpeed.ToolTipText = "x1";
            this.cboSpeed.SelectedIndexChanged += new System.EventHandler(this.cboSpeed_TextUpdate);
            // 
            // mnuSpeed
            // 
            this.mnuSpeed.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuSpeed.Image = global::Finance.Framework.Core.Properties.Resources.car;
            this.mnuSpeed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSpeed.Name = "mnuSpeed";
            this.mnuSpeed.Size = new System.Drawing.Size(16, 22);
            // 
            // mnuPlay
            // 
            this.mnuPlay.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuPlay.Image = global::Finance.Framework.Core.Properties.Resources.resultset_next;
            this.mnuPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuPlay.Name = "mnuPlay";
            this.mnuPlay.Size = new System.Drawing.Size(23, 22);
            this.mnuPlay.Click += new System.EventHandler(this.mnuPlay_StopClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuLoad
            // 
            this.mnuLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataItemToolStripMenuItem1,
            this.multiDataToolStripMenuItem1,
            this.historyDataToolStripMenuItem1});
            this.mnuLoad.Image = global::Finance.Framework.Core.Properties.Resources.folder;
            this.mnuLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuLoad.Name = "mnuLoad";
            this.mnuLoad.Size = new System.Drawing.Size(29, 22);
            // 
            // dataItemToolStripMenuItem1
            // 
            this.dataItemToolStripMenuItem1.Image = global::Finance.Framework.Core.Properties.Resources.brick;
            this.dataItemToolStripMenuItem1.Name = "dataItemToolStripMenuItem1";
            this.dataItemToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.dataItemToolStripMenuItem1.Text = "Data";
            this.dataItemToolStripMenuItem1.Click += new System.EventHandler(this.mnuLoad_Click);
            // 
            // multiDataToolStripMenuItem1
            // 
            this.multiDataToolStripMenuItem1.Image = global::Finance.Framework.Core.Properties.Resources.bricks;
            this.multiDataToolStripMenuItem1.Name = "multiDataToolStripMenuItem1";
            this.multiDataToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.multiDataToolStripMenuItem1.Text = "MultiData";
            this.multiDataToolStripMenuItem1.Click += new System.EventHandler(this.mnuLoad_Click);
            // 
            // historyDataToolStripMenuItem1
            // 
            this.historyDataToolStripMenuItem1.Image = global::Finance.Framework.Core.Properties.Resources.chart_line;
            this.historyDataToolStripMenuItem1.Name = "historyDataToolStripMenuItem1";
            this.historyDataToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.historyDataToolStripMenuItem1.Text = "HistoryData";
            this.historyDataToolStripMenuItem1.Click += new System.EventHandler(this.mnuLoad_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataItemToolStripMenuItem,
            this.multiDataToolStripMenuItem,
            this.historyDataToolStripMenuItem});
            this.mnuSave.Image = global::Finance.Framework.Core.Properties.Resources.disk;
            this.mnuSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(29, 22);
            // 
            // dataItemToolStripMenuItem
            // 
            this.dataItemToolStripMenuItem.Image = global::Finance.Framework.Core.Properties.Resources.brick;
            this.dataItemToolStripMenuItem.Name = "dataItemToolStripMenuItem";
            this.dataItemToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.dataItemToolStripMenuItem.Text = "Data";
            this.dataItemToolStripMenuItem.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // multiDataToolStripMenuItem
            // 
            this.multiDataToolStripMenuItem.Image = global::Finance.Framework.Core.Properties.Resources.bricks;
            this.multiDataToolStripMenuItem.Name = "multiDataToolStripMenuItem";
            this.multiDataToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.multiDataToolStripMenuItem.Text = "MultiData";
            this.multiDataToolStripMenuItem.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // historyDataToolStripMenuItem
            // 
            this.historyDataToolStripMenuItem.Image = global::Finance.Framework.Core.Properties.Resources.chart_line;
            this.historyDataToolStripMenuItem.Name = "historyDataToolStripMenuItem";
            this.historyDataToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.historyDataToolStripMenuItem.Text = "HistoryData";
            this.historyDataToolStripMenuItem.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // picLoad
            // 
            this.picLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoad.BackColor = System.Drawing.Color.White;
            this.picLoad.Image = global::Finance.Framework.Core.Properties.Resources.Throbber;
            this.picLoad.Location = new System.Drawing.Point(3, 44);
            this.picLoad.Name = "picLoad";
            this.picLoad.Size = new System.Drawing.Size(445, 158);
            this.picLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoad.TabIndex = 4;
            this.picLoad.TabStop = false;
            this.picLoad.Visible = false;
            // 
            // MarketExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picLoad);
            this.Controls.Add(this.lstStocks);
            this.Controls.Add(this.toolMenu);
            this.DoubleBuffered = true;
            this.Name = "MarketExport";
            this.Size = new System.Drawing.Size(451, 205);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstStocks;
        private System.Windows.Forms.ColumnHeader colStock;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripButton mnuClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ColumnHeader colFirstDate;
        private System.Windows.Forms.ColumnHeader colLastDate;
        private System.Windows.Forms.ToolStripDropDownButton mnuLoad;
        private System.Windows.Forms.ToolStripMenuItem dataItemToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem multiDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem historyDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton mnuSave;
        private System.Windows.Forms.ToolStripMenuItem dataItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton mnuPlay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cboSpeed;
        private System.Windows.Forms.ToolStripLabel mnuSpeed;
        private System.Windows.Forms.PictureBox picLoad;

    }
}
