namespace Finance.Framework.Core.Graphics.Excel
{
    partial class FrmLinker
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
            this.lstLinks = new System.Windows.Forms.ListView();
            this.colCell = new System.Windows.Forms.ColumnHeader();
            this.colDataDefinition = new System.Windows.Forms.ColumnHeader();
            this.itemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuModify = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDataDefinition = new System.Windows.Forms.TextBox();
            this.txtCell = new System.Windows.Forms.TextBox();
            this.itemContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLinks
            // 
            this.lstLinks.AllowColumnReorder = true;
            this.lstLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCell,
            this.colDataDefinition});
            this.lstLinks.ContextMenuStrip = this.itemContextMenu;
            this.lstLinks.FullRowSelect = true;
            this.lstLinks.GridLines = true;
            this.lstLinks.LabelEdit = true;
            this.lstLinks.Location = new System.Drawing.Point(3, 47);
            this.lstLinks.MultiSelect = false;
            this.lstLinks.Name = "lstLinks";
            this.lstLinks.ShowGroups = false;
            this.lstLinks.Size = new System.Drawing.Size(240, 111);
            this.lstLinks.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstLinks.TabIndex = 3;
            this.lstLinks.UseCompatibleStateImageBehavior = false;
            this.lstLinks.View = System.Windows.Forms.View.Details;
            this.lstLinks.DoubleClick += new System.EventHandler(this.lstLinks_DoubleClick);
            // 
            // colCell
            // 
            this.colCell.Text = "Cell";
            this.colCell.Width = 116;
            // 
            // colDataDefinition
            // 
            this.colDataDefinition.Text = "Data definition";
            this.colDataDefinition.Width = 113;
            // 
            // itemContextMenu
            // 
            this.itemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuModify,
            this.mnuRemove});
            this.itemContextMenu.Name = "itemContextMenu";
            this.itemContextMenu.Size = new System.Drawing.Size(114, 48);
            // 
            // mnuModify
            // 
            this.mnuModify.Image = global::Finance.Framework.Core.Properties.Resources.page_edit;
            this.mnuModify.Name = "mnuModify";
            this.mnuModify.Size = new System.Drawing.Size(113, 22);
            this.mnuModify.Text = "Modify";
            this.mnuModify.Click += new System.EventHandler(this.mnuModify_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Image = global::Finance.Framework.Core.Properties.Resources.cross;
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Size = new System.Drawing.Size(113, 22);
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::Finance.Framework.Core.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(215, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(28, 29);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Image = global::Finance.Framework.Core.Properties.Resources.accept;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(3, 164);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(240, 29);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Close";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cell:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Data definition:";
            // 
            // txtDataDefinition
            // 
            this.txtDataDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataDefinition.Location = new System.Drawing.Point(59, 21);
            this.txtDataDefinition.Name = "txtDataDefinition";
            this.txtDataDefinition.Size = new System.Drawing.Size(151, 20);
            this.txtDataDefinition.TabIndex = 8;
            // 
            // txtCell
            // 
            this.txtCell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCell.Location = new System.Drawing.Point(4, 21);
            this.txtCell.Name = "txtCell";
            this.txtCell.Size = new System.Drawing.Size(49, 20);
            this.txtCell.TabIndex = 7;
            // 
            // FrmLinker
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(246, 198);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDataDefinition);
            this.Controls.Add(this.txtCell);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstLinks);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLinker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Linker";
            this.itemContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListView lstLinks;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ColumnHeader colCell;
        private System.Windows.Forms.ColumnHeader colDataDefinition;
        private System.Windows.Forms.ContextMenuStrip itemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuModify;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDataDefinition;
        private System.Windows.Forms.TextBox txtCell;
    }
}