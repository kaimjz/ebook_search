using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SearchForm
{
    public partial class MainForm : Form
    {
        #region 字段

        private Point _Point;//窗体拖动位置

        private Dictionary<string, string> _TemplateDic;//模板文件

        private string _TemplateDir;//模板文件路径

        #endregion 字段

        #region 构造+初始

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _TemplateDic = GetTemplateList();//获取模板集合
            this.cbxType1.Items.Clear();
            this.cbxType2.Items.Clear();
            if (_TemplateDic != null && _TemplateDic.Count > 0)
            {
                foreach (var item in _TemplateDic)
                {
                    this.cbxType1.Items.Add(item.Key);
                    this.cbxType2.Items.Add(item.Key);
                }
                this.cbxType1.SelectedIndex = 0;
                this.cbxType2.SelectedIndex = 0;
            }
        }

        #endregion 构造+初始

        #region 窗体拖动

        private void tabMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X - (_Point.X - e.X), this.Location.Y - (_Point.Y - e.Y));
            }
        }

        private void tabMain_MouseDown(object sender, MouseEventArgs e)
        {
            _Point = new Point(e.X, e.Y);
        }

        #endregion 窗体拖动

        #region tab切换

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 0)
            {
                this.cbxType1.SelectedIndex = 0;
                this.tbxReadPath.Text = "";
                this.tbxSavePath.Text = "";
            }
        }

        #endregion tab切换

        #region 保存路径

        private void tbxSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "txt|*.txt";
            saveDialog.FileName = "说";
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbxSavePath.Text = saveDialog.FileName;
            }
        }

        #endregion 保存路径

        #region 开始检索

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.tbxReadPath.Text.Trim() == "")
            {
                MessageBox.Show("请输入读取路径!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.tbxSavePath.Text.Trim() == "")
            {
                MessageBox.Show("请输入保存路径!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(this.tbxSavePath.Text.Trim())))
            {
                MessageBox.Show("保存路径不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion 开始检索

        #region 获取模板集合

        /// <summary>
        /// 获取模板集合
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetTemplateList()
        {
            _TemplateDir = Directory.GetParent(Application.StartupPath).Parent.FullName + "\\Template\\";
            string[] files = Directory.GetFiles(_TemplateDir);
            return files.ToDictionary(k => Path.GetFileNameWithoutExtension(k), v => v);
        }

        #endregion 获取模板集合
    }
}