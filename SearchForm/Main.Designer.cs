namespace SearchForm
{
    partial class MainForm
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblRemark = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.lblSavePath = new System.Windows.Forms.Label();
            this.tbxSavePath = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(481, 241);
            this.tabMain.TabIndex = 0;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMain_Selected);
            this.tabMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabMain_MouseDown);
            this.tabMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabMain_MouseMove);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Controls.Add(this.tbxSavePath);
            this.tabPage1.Controls.Add(this.lblSavePath);
            this.tabPage1.Controls.Add(this.lblTemplate);
            this.tabPage1.Controls.Add(this.cbxType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(473, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "主窗体";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(3, 247);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(65, 12);
            this.lblRemark.TabIndex = 1;
            this.lblRemark.Text = "自制小工具";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(473, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "配置XML";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cbxType
            // 
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbxType.Location = new System.Drawing.Point(109, 30);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(96, 20);
            this.cbxType.TabIndex = 0;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(29, 33);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(59, 12);
            this.lblTemplate.TabIndex = 2;
            this.lblTemplate.Text = "爬虫模板:";
            // 
            // lblSavePath
            // 
            this.lblSavePath.AutoSize = true;
            this.lblSavePath.Location = new System.Drawing.Point(31, 71);
            this.lblSavePath.Name = "lblSavePath";
            this.lblSavePath.Size = new System.Drawing.Size(59, 12);
            this.lblSavePath.TabIndex = 3;
            this.lblSavePath.Text = "保存地址:";
            // 
            // tbxSavePath
            // 
            this.tbxSavePath.Location = new System.Drawing.Point(109, 68);
            this.tbxSavePath.Name = "tbxSavePath";
            this.tbxSavePath.Size = new System.Drawing.Size(207, 21);
            this.tbxSavePath.TabIndex = 4;
            this.tbxSavePath.Click += new System.EventHandler(this.tbxSavePath_Click);
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Location = new System.Drawing.Point(174, 140);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(479, 263);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗体";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label lblSavePath;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.TextBox tbxSavePath;
        private System.Windows.Forms.Button btnStart;
    }
}

