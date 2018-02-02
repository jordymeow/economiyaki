namespace Finance.Jumbler
{
    partial class FrmToolbox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmToolbox));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picPython = new System.Windows.Forms.PictureBox();
            this.picReuters = new System.Windows.Forms.PictureBox();
            this.picSpreadsheet = new System.Windows.Forms.PictureBox();
            this.picExport = new System.Windows.Forms.PictureBox();
            this.picGrid = new System.Windows.Forms.PictureBox();
            this.picGraph = new System.Windows.Forms.PictureBox();
            this.picLogger = new System.Windows.Forms.PictureBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPython)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReuters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpreadsheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogger)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.23077F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.76923F));
            this.tableLayoutPanel1.Controls.Add(this.picPython, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.picGraph, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.picReuters, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.picLogger, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.picGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.picExport, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.picSpreadsheet, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(68, 139);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // picPython
            // 
            this.picPython.Image = global::Finance.Jumbler.Properties.Resources.brick_edit;
            this.picPython.Location = new System.Drawing.Point(38, 74);
            this.picPython.Name = "picPython";
            this.picPython.Size = new System.Drawing.Size(24, 25);
            this.picPython.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picPython.TabIndex = 7;
            this.picPython.TabStop = false;
            this.picPython.Tag = "";
            this.picPython.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPython_MouseDown);
            // 
            // picReuters
            // 
            this.picReuters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picReuters.Image = global::Finance.Jumbler.Properties.Resources.xtra;
            this.picReuters.Location = new System.Drawing.Point(6, 6);
            this.picReuters.Name = "picReuters";
            this.picReuters.Size = new System.Drawing.Size(23, 25);
            this.picReuters.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picReuters.TabIndex = 5;
            this.picReuters.TabStop = false;
            this.picReuters.Tag = "";
            this.picReuters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picData_MouseDown);
            // 
            // picSpreadsheet
            // 
            this.picSpreadsheet.Image = global::Finance.Jumbler.Properties.Resources.excel;
            this.picSpreadsheet.Location = new System.Drawing.Point(6, 74);
            this.picSpreadsheet.Name = "picSpreadsheet";
            this.picSpreadsheet.Size = new System.Drawing.Size(23, 25);
            this.picSpreadsheet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSpreadsheet.TabIndex = 11;
            this.picSpreadsheet.TabStop = false;
            this.picSpreadsheet.Tag = "";
            this.picSpreadsheet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSpreadsheet_MouseDown);
            // 
            // picExport
            // 
            this.picExport.Image = global::Finance.Jumbler.Properties.Resources.database_refresh;
            this.picExport.Location = new System.Drawing.Point(6, 108);
            this.picExport.Name = "picExport";
            this.picExport.Size = new System.Drawing.Size(23, 25);
            this.picExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picExport.TabIndex = 10;
            this.picExport.TabStop = false;
            this.picExport.Tag = "";
            this.picExport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picExport_MouseDown);
            // 
            // picGrid
            // 
            this.picGrid.Image = global::Finance.Jumbler.Properties.Resources.ShowGridlinesHS;
            this.picGrid.Location = new System.Drawing.Point(6, 40);
            this.picGrid.Name = "picGrid";
            this.picGrid.Size = new System.Drawing.Size(23, 25);
            this.picGrid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picGrid.TabIndex = 6;
            this.picGrid.TabStop = false;
            this.picGrid.Tag = "";
            this.picGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picGrid_MouseDown);
            // 
            // picGraph
            // 
            this.picGraph.Image = global::Finance.Jumbler.Properties.Resources.chart_curve;
            this.picGraph.Location = new System.Drawing.Point(38, 40);
            this.picGraph.Name = "picGraph";
            this.picGraph.Size = new System.Drawing.Size(24, 25);
            this.picGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picGraph.TabIndex = 9;
            this.picGraph.TabStop = false;
            this.picGraph.Tag = "";
            this.picGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picGraph_MouseDown);
            // 
            // picLogger
            // 
            this.picLogger.Image = global::Finance.Jumbler.Properties.Resources.application_xp_terminal;
            this.picLogger.Location = new System.Drawing.Point(38, 6);
            this.picLogger.Name = "picLogger";
            this.picLogger.Size = new System.Drawing.Size(23, 25);
            this.picLogger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLogger.TabIndex = 4;
            this.picLogger.TabStop = false;
            this.picLogger.Tag = "";
            this.picLogger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picLogger_MouseDown);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "disconnect.png");
            // 
            // FrmToolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(68, 141);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmToolbox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Toolbox";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPython)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReuters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpreadsheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox picLogger;
        private System.Windows.Forms.PictureBox picGraph;
        private System.Windows.Forms.PictureBox picPython;
        private System.Windows.Forms.PictureBox picGrid;
        private System.Windows.Forms.PictureBox picSpreadsheet;
        private System.Windows.Forms.PictureBox picExport;
        private System.Windows.Forms.PictureBox picReuters;
        private System.Windows.Forms.ImageList imageList;

    }
}