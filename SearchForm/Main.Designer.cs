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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblPercent = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxSign = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbxBookName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxReadPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSavePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxType1 = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cbxEncode = new System.Windows.Forms.ComboBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.tbxValue = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.rtbxXml = new System.Windows.Forms.RichTextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cbxType2 = new System.Windows.Forms.ComboBox();
            this.listView4 = new System.Windows.Forms.ListView();
            this.cbxPage = new System.Windows.Forms.ComboBox();
            this.listView3 = new System.Windows.Forms.ListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rtbxChar = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCharDel = new System.Windows.Forms.Button();
            this.btnAddChart = new System.Windows.Forms.Button();
            this.tbxChar = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Controls.Add(this.tabPage3);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(479, 283);
            this.tabMain.TabIndex = 0;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMain_Selected);
            this.tabMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabMain_MouseDown);
            this.tabMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabMain_MouseMove);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblPercent);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.tbxSign);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Controls.Add(this.tbxBookName);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.tbxReadPath);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbxSavePath);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cbxType1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.cbxEncode);
            this.tabPage1.Controls.Add(this.listView2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(471, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "主窗体";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(323, 211);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(0, 12);
            this.lblPercent.TabIndex = 14;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(104, 206);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(215, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(291, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "卷名和章名标签(*间隔)";
            // 
            // tbxSign
            // 
            this.tbxSign.Location = new System.Drawing.Point(104, 127);
            this.tbxSign.Name = "tbxSign";
            this.tbxSign.Size = new System.Drawing.Size(181, 21);
            this.tbxSign.TabIndex = 11;
            this.tbxSign.Text = "dt*a";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "编码格式:";
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Location = new System.Drawing.Point(354, 206);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(68, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbxBookName
            // 
            this.tbxBookName.Location = new System.Drawing.Point(104, 53);
            this.tbxBookName.Name = "tbxBookName";
            this.tbxBookName.Size = new System.Drawing.Size(319, 21);
            this.tbxBookName.TabIndex = 4;
            this.tbxBookName.Text = "仙国大帝";
            this.tbxBookName.TextChanged += new System.EventHandler(this.tbxBookName_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "读取书名:";
            // 
            // tbxReadPath
            // 
            this.tbxReadPath.Location = new System.Drawing.Point(104, 90);
            this.tbxReadPath.Name = "tbxReadPath";
            this.tbxReadPath.Size = new System.Drawing.Size(319, 21);
            this.tbxReadPath.TabIndex = 4;
            this.tbxReadPath.Text = "http://www.cangqionglongqi.com/xianguodadi/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "读取地址:";
            // 
            // tbxSavePath
            // 
            this.tbxSavePath.Location = new System.Drawing.Point(104, 164);
            this.tbxSavePath.Name = "tbxSavePath";
            this.tbxSavePath.Size = new System.Drawing.Size(318, 21);
            this.tbxSavePath.TabIndex = 4;
            this.tbxSavePath.Text = "C:\\Users\\kaimj\\Desktop\\仙国大帝.txt";
            this.tbxSavePath.Click += new System.EventHandler(this.tbxSavePath_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "读取标记:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 211);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "读取进度:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "保存地址:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "爬虫模板:";
            // 
            // cbxType1
            // 
            this.cbxType1.BackColor = System.Drawing.SystemColors.Window;
            this.cbxType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxType1.FormattingEnabled = true;
            this.cbxType1.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbxType1.Location = new System.Drawing.Point(104, 18);
            this.cbxType1.Name = "cbxType1";
            this.cbxType1.Size = new System.Drawing.Size(105, 20);
            this.cbxType1.TabIndex = 0;
            this.cbxType1.SelectedIndexChanged += new System.EventHandler(this.cbxType1_SelectedIndexChanged);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(102, 17);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(108, 22);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // cbxEncode
            // 
            this.cbxEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEncode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxEncode.FormattingEnabled = true;
            this.cbxEncode.Items.AddRange(new object[] {
            "Default/GB2312",
            "UTF8"});
            this.cbxEncode.Location = new System.Drawing.Point(317, 16);
            this.cbxEncode.Name = "cbxEncode";
            this.cbxEncode.Size = new System.Drawing.Size(105, 20);
            this.cbxEncode.TabIndex = 6;
            this.cbxEncode.SelectedIndexChanged += new System.EventHandler(this.cbxEncode_SelectedIndexChanged);
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(315, 15);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(108, 22);
            this.listView2.TabIndex = 10;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.btnUpdate);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btnDel);
            this.tabPage2.Controls.Add(this.btn_Add);
            this.tabPage2.Controls.Add(this.tbxValue);
            this.tabPage2.Controls.Add(this.tbxName);
            this.tabPage2.Controls.Add(this.rtbxXml);
            this.tabPage2.Controls.Add(this.label33);
            this.tabPage2.Controls.Add(this.cbxType2);
            this.tabPage2.Controls.Add(this.listView4);
            this.tabPage2.Controls.Add(this.cbxPage);
            this.tabPage2.Controls.Add(this.listView3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(471, 257);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "配置XML";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "页类别:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(194, 90);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "值:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "节点名:";
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(320, 90);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 7;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(75, 90);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 7;
            this.btn_Add.Text = "添加";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // tbxValue
            // 
            this.tbxValue.Location = new System.Drawing.Point(319, 55);
            this.tbxValue.Name = "tbxValue";
            this.tbxValue.Size = new System.Drawing.Size(106, 21);
            this.tbxValue.TabIndex = 6;
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(103, 55);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(106, 21);
            this.tbxName.TabIndex = 6;
            // 
            // rtbxXml
            // 
            this.rtbxXml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbxXml.Location = new System.Drawing.Point(-2, 135);
            this.rtbxXml.Margin = new System.Windows.Forms.Padding(0);
            this.rtbxXml.Name = "rtbxXml";
            this.rtbxXml.Size = new System.Drawing.Size(478, 125);
            this.rtbxXml.TabIndex = 5;
            this.rtbxXml.Text = "";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(26, 20);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(59, 12);
            this.label33.TabIndex = 4;
            this.label33.Text = "爬虫模板:";
            // 
            // cbxType2
            // 
            this.cbxType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxType2.FormattingEnabled = true;
            this.cbxType2.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbxType2.Location = new System.Drawing.Point(105, 18);
            this.cbxType2.Name = "cbxType2";
            this.cbxType2.Size = new System.Drawing.Size(103, 20);
            this.cbxType2.TabIndex = 3;
            this.cbxType2.SelectedIndexChanged += new System.EventHandler(this.cbxType2_SelectedIndexChanged);
            // 
            // listView4
            // 
            this.listView4.Location = new System.Drawing.Point(103, 17);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(106, 22);
            this.listView4.TabIndex = 13;
            this.listView4.UseCompatibleStateImageBehavior = false;
            // 
            // cbxPage
            // 
            this.cbxPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxPage.FormattingEnabled = true;
            this.cbxPage.Items.AddRange(new object[] {
            "目录",
            "内文"});
            this.cbxPage.Location = new System.Drawing.Point(323, 17);
            this.cbxPage.Name = "cbxPage";
            this.cbxPage.Size = new System.Drawing.Size(100, 20);
            this.cbxPage.TabIndex = 10;
            this.cbxPage.SelectedIndexChanged += new System.EventHandler(this.cbxPage_SelectedIndexChanged);
            // 
            // listView3
            // 
            this.listView3.Location = new System.Drawing.Point(319, 16);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(105, 22);
            this.listView3.TabIndex = 14;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtbxChar);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.btnCharDel);
            this.tabPage3.Controls.Add(this.btnAddChart);
            this.tabPage3.Controls.Add(this.tbxChar);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(471, 257);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "去除特殊字符";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rtbxChar
            // 
            this.rtbxChar.Location = new System.Drawing.Point(-2, 128);
            this.rtbxChar.Margin = new System.Windows.Forms.Padding(0);
            this.rtbxChar.Name = "rtbxChar";
            this.rtbxChar.Size = new System.Drawing.Size(478, 133);
            this.rtbxChar.TabIndex = 14;
            this.rtbxChar.Text = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(35, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 13;
            this.label13.Text = "特殊字符:";
            // 
            // btnCharDel
            // 
            this.btnCharDel.Location = new System.Drawing.Point(282, 88);
            this.btnCharDel.Name = "btnCharDel";
            this.btnCharDel.Size = new System.Drawing.Size(75, 23);
            this.btnCharDel.TabIndex = 11;
            this.btnCharDel.Text = "删除";
            this.btnCharDel.UseVisualStyleBackColor = true;
            this.btnCharDel.Click += new System.EventHandler(this.btnCharDel_Click);
            // 
            // btnAddChart
            // 
            this.btnAddChart.Location = new System.Drawing.Point(100, 88);
            this.btnAddChart.Name = "btnAddChart";
            this.btnAddChart.Size = new System.Drawing.Size(75, 23);
            this.btnAddChart.TabIndex = 11;
            this.btnAddChart.Text = "添加";
            this.btnAddChart.UseVisualStyleBackColor = true;
            this.btnAddChart.Click += new System.EventHandler(this.btnAddChart_Click);
            // 
            // tbxChar
            // 
            this.tbxChar.Location = new System.Drawing.Point(100, 41);
            this.tbxChar.Name = "tbxChar";
            this.tbxChar.Size = new System.Drawing.Size(257, 21);
            this.tbxChar.TabIndex = 10;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(2, 286);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(71, 12);
            this.lblRemark.TabIndex = 1;
            this.lblRemark.Text = "小爬虫 v1.0";
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Location = new System.Drawing.Point(442, 286);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(29, 12);
            this.lblClose.TabIndex = 2;
            this.lblClose.Text = "关闭";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(479, 307);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗体";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbxType1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSavePath;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cbxType2;
        private System.Windows.Forms.TextBox tbxReadPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbxXml;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox tbxValue;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.ComboBox cbxEncode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxPage;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.TextBox tbxSign;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.TextBox tbxBookName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAddChart;
        private System.Windows.Forms.TextBox tbxChar;
        private System.Windows.Forms.RichTextBox rtbxChar;
        private System.Windows.Forms.Button btnCharDel;
        private System.Windows.Forms.Label lblClose;
    }
}

