namespace ScanDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnScanner = new System.Windows.Forms.Button();
            this.PbxScanner = new System.Windows.Forms.PictureBox();
            this.TxtScannerCode = new System.Windows.Forms.TextBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PbxScanner)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnScanner
            // 
            this.BtnScanner.Location = new System.Drawing.Point(293, 51);
            this.BtnScanner.Name = "BtnScanner";
            this.BtnScanner.Size = new System.Drawing.Size(75, 23);
            this.BtnScanner.TabIndex = 0;
            this.BtnScanner.Text = "BtnScanner";
            this.BtnScanner.UseVisualStyleBackColor = true;
            this.BtnScanner.Click += new System.EventHandler(this.BtnScanner_Click);
            // 
            // PbxScanner
            // 
            this.PbxScanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbxScanner.Location = new System.Drawing.Point(85, 106);
            this.PbxScanner.Name = "PbxScanner";
            this.PbxScanner.Size = new System.Drawing.Size(314, 267);
            this.PbxScanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbxScanner.TabIndex = 1;
            this.PbxScanner.TabStop = false;
            // 
            // TxtScannerCode
            // 
            this.TxtScannerCode.Location = new System.Drawing.Point(85, 403);
            this.TxtScannerCode.Name = "TxtScannerCode";
            this.TxtScannerCode.Size = new System.Drawing.Size(314, 21);
            this.TxtScannerCode.TabIndex = 2;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(85, 51);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 3;
            this.BtnStart.Text = "BtnStart";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(189, 51);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 4;
            this.BtnStop.Text = "BtnStop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 454);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.TxtScannerCode);
            this.Controls.Add(this.PbxScanner);
            this.Controls.Add(this.BtnScanner);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PbxScanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnScanner;
        private System.Windows.Forms.PictureBox PbxScanner;
        private System.Windows.Forms.TextBox TxtScannerCode;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnStop;
    }
}

