using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SearchForm
{
    public partial class MainForm : Form
    {
        private Point _Point;//窗体拖动位置
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
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

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 0)
            {
                this.cbxType.SelectedIndex = 0;
                this.tbxSavePath.Text = "";
            }
        }
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.tbxSavePath.Text.Trim() == "")
            {
                MessageBox.Show("请选择保存路径!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(this.tbxSavePath.Text.Trim())))
            {
                MessageBox.Show("保存路径不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //_Point = new Point(e.X, e.Y);

            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    this.Location = new Point(this.Location.X - (_Point.X - e.X), this.Location.Y - (_Point.Y - e.Y));
            //}
        }

    }
}
