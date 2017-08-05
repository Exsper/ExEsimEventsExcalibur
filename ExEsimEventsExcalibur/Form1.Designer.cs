namespace ExEsimEventsExcalibur
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.InTextBox = new System.Windows.Forms.TextBox();
            this.OutTextBox = new System.Windows.Forms.TextBox();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // InTextBox
            // 
            this.InTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InTextBox.Location = new System.Drawing.Point(0, 0);
            this.InTextBox.Multiline = true;
            this.InTextBox.Name = "InTextBox";
            this.InTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InTextBox.Size = new System.Drawing.Size(584, 104);
            this.InTextBox.TabIndex = 0;
            this.InTextBox.TextChanged += new System.EventHandler(this.InTextBox_TextChanged);
            this.InTextBox.MouseEnter += new System.EventHandler(this.InTextBox_MouseEnter);
            // 
            // OutTextBox
            // 
            this.OutTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutTextBox.Location = new System.Drawing.Point(0, 0);
            this.OutTextBox.Multiline = true;
            this.OutTextBox.Name = "OutTextBox";
            this.OutTextBox.ReadOnly = true;
            this.OutTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutTextBox.Size = new System.Drawing.Size(584, 312);
            this.OutTextBox.TabIndex = 1;
            this.OutTextBox.MouseEnter += new System.EventHandler(this.OutTextBox_MouseEnter);
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.InTextBox);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.OutTextBox);
            this.MainSplitContainer.Size = new System.Drawing.Size(584, 422);
            this.MainSplitContainer.SplitterDistance = 106;
            this.MainSplitContainer.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 422);
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "Form1";
            this.Text = "ExEsimEventsExcalibur v1.03";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel1.PerformLayout();
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox InTextBox;
        private System.Windows.Forms.TextBox OutTextBox;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
    }
}

