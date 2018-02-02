namespace Finance.Framework.Core
{
    partial class MarketGraph
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
            this.toolInputMessage = new System.Windows.Forms.ToolStripLabel();
            this.mnuClear = new System.Windows.Forms.ToolStripButton();
            this.mnuColors = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuPerfMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.toolMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOut,
            this.toolInputMessage,
            this.mnuClear,
            this.mnuColors,
            this.mnuPerfMode,
            this.toolStripSeparator2});
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
            // mnuClear
            // 
            this.mnuClear.Image = global::Finance.Framework.Core.Properties.Resources.chart_curve;
            this.mnuClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(52, 22);
            this.mnuClear.Text = "Clear";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // mnuColors
            // 
            this.mnuColors.Image = global::Finance.Framework.Core.Properties.Resources.color_swatch;
            this.mnuColors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuColors.Name = "mnuColors";
            this.mnuColors.Size = new System.Drawing.Size(66, 22);
            this.mnuColors.Text = "Colors";
            // 
            // mnuPerfMode
            // 
            this.mnuPerfMode.Image = global::Finance.Framework.Core.Properties.Resources.chart_line;
            this.mnuPerfMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuPerfMode.Name = "mnuPerfMode";
            this.mnuPerfMode.Size = new System.Drawing.Size(51, 22);
            this.mnuPerfMode.Text = "Perf.";
            this.mnuPerfMode.Click += new System.EventHandler(this.mnuPerfMode_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // zedGraph
            // 
            this.zedGraph.AllowDrop = true;
            this.zedGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraph.Location = new System.Drawing.Point(0, 25);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0;
            this.zedGraph.ScrollMaxX = 0;
            this.zedGraph.ScrollMaxY = 0;
            this.zedGraph.ScrollMaxY2 = 0;
            this.zedGraph.ScrollMinX = 0;
            this.zedGraph.ScrollMinY = 0;
            this.zedGraph.ScrollMinY2 = 0;
            this.zedGraph.Size = new System.Drawing.Size(450, 225);
            this.zedGraph.TabIndex = 10;
            // 
            // MarketGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zedGraph);
            this.Controls.Add(this.toolMenu);
            this.Name = "MarketGraph";
            this.Size = new System.Drawing.Size(450, 250);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripLabel toolOut;
        private System.Windows.Forms.ToolStripLabel toolInputMessage;
        private System.Windows.Forms.ToolStripButton mnuPerfMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuClear;
        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.ToolStripDropDownButton mnuColors;
    }
}
