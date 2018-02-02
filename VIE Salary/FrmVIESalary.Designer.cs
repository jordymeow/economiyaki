namespace VIE_Realtime_Salary
{
    partial class FrmVIESalary
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.marketGraphHistory = new Finance.Framework.Core.MarketGraph();
            this.marketGridHistory = new Finance.Framework.Core.MarketGrid();
            this.marketGraphRealtime = new Finance.Framework.Core.MarketGraph();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // marketGraphHistory
            // 
            this.marketGraphHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.marketGraphHistory.Color_1 = System.Drawing.Color.RoyalBlue;
            this.marketGraphHistory.Color_2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.marketGraphHistory.Color_3 = System.Drawing.Color.Green;
            this.marketGraphHistory.Color_4 = System.Drawing.Color.Pink;
            this.marketGraphHistory.Color_5 = System.Drawing.Color.PowderBlue;
            this.marketGraphHistory.ID = new System.Guid("2d3e7006-fa6c-4a36-8613-1fa0c719ad53");
            this.marketGraphHistory.Location = new System.Drawing.Point(174, 3);
            this.marketGraphHistory.Name = "marketGraphHistory";
            this.marketGraphHistory.ShowMenuBar = false;
            this.marketGraphHistory.Size = new System.Drawing.Size(671, 289);
            this.marketGraphHistory.TabIndex = 2;
            // 
            // marketGridHistory
            // 
            this.marketGridHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.marketGridHistory.ID = new System.Guid("6e29e344-3f51-4f79-9a23-60e8a1b689fa");
            this.marketGridHistory.Location = new System.Drawing.Point(-2, 3);
            this.marketGridHistory.Name = "marketGridHistory";
            this.marketGridHistory.ShowMenuBar = false;
            this.marketGridHistory.Size = new System.Drawing.Size(170, 549);
            this.marketGridHistory.TabIndex = 1;
            // 
            // marketGraphRealtime
            // 
            this.marketGraphRealtime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.marketGraphRealtime.Color_1 = System.Drawing.Color.Yellow;
            this.marketGraphRealtime.Color_2 = System.Drawing.Color.Purple;
            this.marketGraphRealtime.Color_3 = System.Drawing.Color.Green;
            this.marketGraphRealtime.Color_4 = System.Drawing.Color.Pink;
            this.marketGraphRealtime.Color_5 = System.Drawing.Color.PowderBlue;
            this.marketGraphRealtime.ID = new System.Guid("2d3e7006-fa6c-4a36-8613-1fa0c719ad53");
            this.marketGraphRealtime.Location = new System.Drawing.Point(174, 292);
            this.marketGraphRealtime.Name = "marketGraphRealtime";
            this.marketGraphRealtime.ShowMenuBar = false;
            this.marketGraphRealtime.Size = new System.Drawing.Size(671, 260);
            this.marketGraphRealtime.TabIndex = 0;
            // 
            // FrmVIESalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 553);
            this.Controls.Add(this.marketGraphHistory);
            this.Controls.Add(this.marketGridHistory);
            this.Controls.Add(this.marketGraphRealtime);
            this.Name = "FrmVIESalary";
            this.ShowIcon = false;
            this.Text = "VIE Realtime Salary";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private Finance.Framework.Core.MarketGraph marketGraphRealtime;
        private Finance.Framework.Core.MarketGrid marketGridHistory;
        private Finance.Framework.Core.MarketGraph marketGraphHistory;






    }
}

