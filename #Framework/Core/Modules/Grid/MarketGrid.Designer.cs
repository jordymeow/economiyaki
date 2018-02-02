namespace Finance.Framework.Core
{
    partial class MarketGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.toolInputMessage = new System.Windows.Forms.ToolStripLabel();
            this.toolClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolOut = new System.Windows.Forms.ToolStripLabel();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAlertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAlertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolMenu.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowDrop = true;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(0, 25);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.RowHeadersWidth = 10;
            this.dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(281, 131);
            this.dataGridView.TabIndex = 1;
            // 
            // ColName
            // 
            this.ColName.HeaderText = "Name";
            this.ColName.Name = "ColName";
            this.ColName.ReadOnly = true;
            this.ColName.Width = 60;
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolInputMessage,
            this.toolClear,
            this.toolStripSeparator1,
            this.toolOut});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(281, 25);
            this.toolMenu.TabIndex = 9;
            this.toolMenu.Text = "toolStrip1";
            this.toolMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip_MouseDown);
            // 
            // toolInputMessage
            // 
            this.toolInputMessage.Name = "toolInputMessage";
            this.toolInputMessage.Size = new System.Drawing.Size(0, 22);
            // 
            // toolClear
            // 
            this.toolClear.Image = global::Finance.Framework.Core.Properties.Resources.table;
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
            // toolOut
            // 
            this.toolOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolOut.Image = global::Finance.Framework.Core.Properties.Resources.RelationshipsHS;
            this.toolOut.Name = "toolOut";
            this.toolOut.Size = new System.Drawing.Size(16, 22);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAlertToolStripMenuItem,
            this.removeAlertToolStripMenuItem});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(139, 48);
            // 
            // setAlertToolStripMenuItem
            // 
            this.setAlertToolStripMenuItem.Image = global::Finance.Framework.Core.Properties.Resources.clock_add;
            this.setAlertToolStripMenuItem.Name = "setAlertToolStripMenuItem";
            this.setAlertToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.setAlertToolStripMenuItem.Text = "Set alert";
            this.setAlertToolStripMenuItem.Click += new System.EventHandler(this.SetAlert_Click);
            // 
            // removeAlertToolStripMenuItem
            // 
            this.removeAlertToolStripMenuItem.Enabled = false;
            this.removeAlertToolStripMenuItem.Image = global::Finance.Framework.Core.Properties.Resources.clock_delete;
            this.removeAlertToolStripMenuItem.Name = "removeAlertToolStripMenuItem";
            this.removeAlertToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.removeAlertToolStripMenuItem.Text = "Remove alert";
            // 
            // MarketGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolMenu);
            this.Name = "MarketGrid";
            this.Size = new System.Drawing.Size(281, 156);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripLabel toolInputMessage;
        private System.Windows.Forms.ToolStripButton toolClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem setAlertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAlertToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolOut;

    }
}
