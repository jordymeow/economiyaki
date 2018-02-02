namespace Finance.Framework.Core
{
    partial class MarketPython
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
            Fireball.Windows.Forms.LineMarginRender lineMarginRender1 = new Fireball.Windows.Forms.LineMarginRender();
            this.codeEditor = new Fireball.Windows.Forms.CodeEditorControl();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.snippetsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snippetsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataItemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.historyDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyDataContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.multiHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syntaxDocument = new Fireball.Syntax.SyntaxDocument(this.components);
            this.snippetsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.toolInputMessage = new System.Windows.Forms.ToolStripButton();
            this.toolOut = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolOutputMessage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPlay = new System.Windows.Forms.ToolStripButton();
            this.mnuStep = new System.Windows.Forms.ToolStripButton();
            this.mnuDebug = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuVarsBox = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.contextMenu.SuspendLayout();
            this.snippetsContextMenu.SuspendLayout();
            this.toolMenu.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeEditor
            // 
            this.codeEditor.ActiveView = Fireball.Windows.Forms.CodeEditor.ActiveView.BottomRight;
            this.codeEditor.AutoListPosition = null;
            this.codeEditor.AutoListSelectedText = "a123";
            this.codeEditor.AutoListVisible = false;
            this.codeEditor.BackColor = System.Drawing.Color.AliceBlue;
            this.codeEditor.ContextMenuStrip = this.contextMenu;
            this.codeEditor.CopyAsRTF = false;
            this.codeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor.Document = this.syntaxDocument;
            this.codeEditor.FontSize = 9F;
            this.codeEditor.GutterMarginWidth = 0;
            this.codeEditor.HighLightActiveLine = true;
            this.codeEditor.HighLightedLineColor = System.Drawing.Color.LightSteelBlue;
            this.codeEditor.InfoTipCount = 1;
            this.codeEditor.InfoTipPosition = null;
            this.codeEditor.InfoTipSelectedIndex = 1;
            this.codeEditor.InfoTipVisible = false;
            lineMarginRender1.Bounds = new System.Drawing.Rectangle(0, 0, 25, 14);
            this.codeEditor.LineMarginRender = lineMarginRender1;
            this.codeEditor.Location = new System.Drawing.Point(0, 0);
            this.codeEditor.LockCursorUpdate = false;
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.Saved = false;
            this.codeEditor.ShowScopeIndicator = false;
            this.codeEditor.ShowTabGuides = true;
            this.codeEditor.Size = new System.Drawing.Size(492, 284);
            this.codeEditor.SmoothScroll = false;
            this.codeEditor.SplitviewH = -4;
            this.codeEditor.SplitviewV = -4;
            this.codeEditor.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(219)))), ((int)(((byte)(214)))));
            this.codeEditor.TabIndex = 0;
            this.codeEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snippetsMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(116, 26);
            // 
            // snippetsMenuItem
            // 
            this.snippetsMenuItem.DropDown = this.snippetsContextMenu;
            this.snippetsMenuItem.Image = global::Finance.Framework.Core.Properties.Resources.page_white_code;
            this.snippetsMenuItem.Name = "snippetsMenuItem";
            this.snippetsMenuItem.Size = new System.Drawing.Size(115, 22);
            this.snippetsMenuItem.Text = "Snippets";
            // 
            // snippetsContextMenu
            // 
            this.snippetsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataItemMenuItem,
            this.dataMenuItem,
            this.toolStripMenuItem1,
            this.historyDataMenuItem,
            this.multiHistoryMenuItem});
            this.snippetsContextMenu.Name = "snippetsContextMenu";
            this.snippetsContextMenu.OwnerItem = this.snippetsDropDownButton;
            this.snippetsContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.snippetsContextMenu.Size = new System.Drawing.Size(131, 98);
            // 
            // dataItemMenuItem
            // 
            this.dataItemMenuItem.DropDown = this.dataItemContextMenu;
            this.dataItemMenuItem.Name = "dataItemMenuItem";
            this.dataItemMenuItem.Size = new System.Drawing.Size(130, 22);
            this.dataItemMenuItem.Text = "Single";
            // 
            // dataItemContextMenu
            // 
            this.dataItemContextMenu.Name = "dataContextMenu";
            this.dataItemContextMenu.OwnerItem = this.dataItemMenuItem;
            this.dataItemContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // dataMenuItem
            // 
            this.dataMenuItem.DropDown = this.dataContextMenu;
            this.dataMenuItem.Name = "dataMenuItem";
            this.dataMenuItem.Size = new System.Drawing.Size(130, 22);
            this.dataMenuItem.Text = "Multi";
            // 
            // dataContextMenu
            // 
            this.dataContextMenu.Name = "dataContextMenu";
            this.dataContextMenu.OwnerItem = this.dataMenuItem;
            this.dataContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(127, 6);
            // 
            // historyDataMenuItem
            // 
            this.historyDataMenuItem.DropDown = this.historyDataContextMenu;
            this.historyDataMenuItem.Name = "historyDataMenuItem";
            this.historyDataMenuItem.Size = new System.Drawing.Size(130, 22);
            this.historyDataMenuItem.Text = "History";
            // 
            // historyDataContextMenu
            // 
            this.historyDataContextMenu.Name = "dataContextMenu";
            this.historyDataContextMenu.OwnerItem = this.historyDataMenuItem;
            this.historyDataContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // multiHistoryMenuItem
            // 
            this.multiHistoryMenuItem.Name = "multiHistoryMenuItem";
            this.multiHistoryMenuItem.Size = new System.Drawing.Size(130, 22);
            this.multiHistoryMenuItem.Text = "MultiHistory";
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        "from Finance.Framework.Core import *\r",
        "from Finance.Framework.Types import *\r",
        "\r",
        "reuters = InitiateReuters()\r",
        "reuters.RequestRealtime(\"EURJPY=\", (\"F_Bid\", \"F_Ask\"))\r",
        "\r",
        "# Static/Realtime data with 1 stock\r",
        "def Single():\r",
        "\treturn i\r",
        "\t \r",
        "# Static/Realtime data with x stocks\r",
        "def Multi():\r",
        "\t return i\r",
        "\t \r",
        "# History with 1 stock\r",
        "def History():\r",
        "\treturn i\r",
        "\r",
        "# History with x stocks\r",
        "def MultiHistory():\r",
        "\treturn i"};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = false;
            this.syntaxDocument.UndoStep = 0;
            // 
            // snippetsDropDownButton
            // 
            this.snippetsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.snippetsDropDownButton.DropDown = this.snippetsContextMenu;
            this.snippetsDropDownButton.Image = global::Finance.Framework.Core.Properties.Resources.page_white_code;
            this.snippetsDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.snippetsDropDownButton.Name = "snippetsDropDownButton";
            this.snippetsDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.snippetsDropDownButton.Text = "Code Snippets";
            // 
            // toolMenu
            // 
            this.toolMenu.AllowDrop = true;
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolInputMessage,
            this.toolOut,
            this.toolStripSeparator3,
            this.toolOutputMessage,
            this.toolStripSeparator2,
            this.mnuPlay,
            this.mnuStep,
            this.mnuDebug,
            this.toolStripSeparator1,
            this.mnuVarsBox,
            this.toolStripSeparator5,
            this.snippetsDropDownButton,
            this.toolStripSeparator4});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(498, 25);
            this.toolMenu.TabIndex = 1;
            this.toolMenu.Text = "toolStrip1";
            this.toolMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip_MouseDown);
            // 
            // toolInputMessage
            // 
            this.toolInputMessage.Image = global::Finance.Framework.Core.Properties.Resources.email;
            this.toolInputMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolInputMessage.Name = "toolInputMessage";
            this.toolInputMessage.Size = new System.Drawing.Size(33, 22);
            this.toolInputMessage.Text = "0";
            this.toolInputMessage.Visible = false;
            this.toolInputMessage.Click += new System.EventHandler(this.toolInputMessage_Click);
            // 
            // toolOut
            // 
            this.toolOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolOut.Image = global::Finance.Framework.Core.Properties.Resources.RelationshipsHS;
            this.toolOut.Name = "toolOut";
            this.toolOut.Size = new System.Drawing.Size(16, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuPlay
            // 
            this.mnuPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuPlay.Image = global::Finance.Framework.Core.Properties.Resources.resultset_next;
            this.mnuPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuPlay.Name = "mnuPlay";
            this.mnuPlay.Size = new System.Drawing.Size(23, 22);
            this.mnuPlay.Text = "Play";
            this.mnuPlay.Click += new System.EventHandler(this.mnuPlay_Click);
            // 
            // mnuStep
            // 
            this.mnuStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuStep.Image = global::Finance.Framework.Core.Properties.Resources.DataContainer_MoveLastHS;
            this.mnuStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStep.Name = "mnuStep";
            this.mnuStep.Size = new System.Drawing.Size(23, 22);
            this.mnuStep.Text = "Step by step";
            this.mnuStep.Click += new System.EventHandler(this.mnuStep_Click);
            // 
            // mnuDebug
            // 
            this.mnuDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuDebug.Image = global::Finance.Framework.Core.Properties.Resources.bug;
            this.mnuDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDebug.Name = "mnuDebug";
            this.mnuDebug.Size = new System.Drawing.Size(23, 22);
            this.mnuDebug.Text = "Debug";
            this.mnuDebug.Click += new System.EventHandler(this.mnuDebug_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuVarsBox
            // 
            this.mnuVarsBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuVarsBox.Image = global::Finance.Framework.Core.Properties.Resources.VSObject_Type_Friend;
            this.mnuVarsBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuVarsBox.Name = "mnuVarsBox";
            this.mnuVarsBox.Size = new System.Drawing.Size(23, 22);
            this.mnuVarsBox.Text = "Variables box";
            this.mnuVarsBox.Click += new System.EventHandler(this.mnuVarsBox_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(3, 28);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.codeEditor);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.txtLog);
            this.splitContainer.Size = new System.Drawing.Size(492, 339);
            this.splitContainer.SplitterDistance = 284;
            this.splitContainer.TabIndex = 2;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(492, 51);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // MarketPython
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolMenu);
            this.Name = "MarketPython";
            this.Size = new System.Drawing.Size(498, 370);
            this.contextMenu.ResumeLayout(false);
            this.snippetsContextMenu.ResumeLayout(false);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Fireball.Windows.Forms.CodeEditorControl codeEditor;
        private Fireball.Syntax.SyntaxDocument syntaxDocument;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripButton mnuPlay;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolInputMessage;
        private System.Windows.Forms.ToolStripButton toolOutputMessage;
        private System.Windows.Forms.ToolStripButton mnuDebug;
        private System.Windows.Forms.ToolStripDropDownButton snippetsDropDownButton;
        private System.Windows.Forms.ToolStripLabel toolOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip snippetsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem dataItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem snippetsMenuItem;
        private System.Windows.Forms.ContextMenuStrip dataItemContextMenu;
        private System.Windows.Forms.ContextMenuStrip historyDataContextMenu;
        private System.Windows.Forms.ContextMenuStrip dataContextMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem multiHistoryMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton mnuVarsBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ToolStripButton mnuStep;
    }
}
