using DotNet.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using HtmlAP = HtmlAgilityPack;

namespace SearchForm
{
    public partial class MainForm : Form
    {
        #region 字段

        private Point _Point;//窗体拖动位置

        private Dictionary<string, string> _TemplateDict;//模板文件

        private string _TemplateDir;//模板文件路径

        private Encoding _Encode;//编码格式

        #endregion 字段

        #region 构造+初始

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _TemplateDict = GetTemplateList();//获取模板集合
            this.cbxEncode.SelectedIndex = 0;
            this.cbxType1.Items.Clear();
            this.cbxType2.Items.Clear();
            if (_TemplateDict != null && _TemplateDict.Count > 0)
            {
                foreach (var item in _TemplateDict)
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
                this.cbxEncode.SelectedIndex = 0;
                this.tbxReadPath.Text = "";
                this.tbxSavePath.Text = "";
            }
            if (e.TabPageIndex == 1)
            {
                this.cbxType2.SelectedIndex = 0;
                this.cbxPage.SelectedIndex = 0;
                this.tbxName.Text = "";
                this.tbxValue.Text = "";
                GetNodeList();
            }
        }

        #endregion tab切换

        #region **************************************tab页1***************************************

        #region 编码格式

        private void cbxEncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((System.Windows.Forms.ComboBox)(sender)).SelectedItem.ToString())
            {
                case "Default/GB2312":
                    _Encode = Encoding.Default;
                    break;

                case "UTF-8":
                    _Encode = Encoding.UTF8;
                    break;

                default:
                    _Encode = Encoding.Default;
                    break;
            }
        }

        #endregion 编码格式

        #region 保存路径

        private void tbxSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "txt|*.txt";
            saveDialog.FileName = "某某某";
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
            if (this.tbxSign.Text.Trim() == "")
            {
                MessageBox.Show("请输入读取标记!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            try
            {
                StringBuilder strContent = new StringBuilder();
                string htmlPath = Path.GetDirectoryName(this.tbxReadPath.Text.Trim());//当前具体站点
                HtmlAP.HtmlDocument htmlDoc = new HtmlAP.HtmlDocument();
                string htmlstr = GetGeneralContent(this.tbxReadPath.Text.Trim());
                htmlDoc.LoadHtml(htmlstr);
                HtmlAP.HtmlNode rootNode = htmlDoc.DocumentNode;
                List<string> catalogXpathList = GetNodeXpathList("目录");
                if (catalogXpathList == null)
                {
                    return;
                }
                foreach (var item in catalogXpathList)
                {
                    HtmlAP.HtmlNode catalogNode = rootNode.SelectSingleNode(item);
                    if (catalogNode == null)
                    {
                        continue;
                    }
                    HtmlAP.HtmlNodeCollection nodeList = GetALLNodeList(catalogNode);
                    if (nodeList == null)
                    {
                        continue;
                    }
                    foreach (var node in nodeList)
                    {
                        if (this.tbxSign.Text.ToLower().Split('*').Contains(node.Name.ToLower()))
                        {
                            strContent.AppendLine(node.InnerText);
                            if (node.Attributes == null || node.Attributes.Count <= 0 || node.Attributes["href"] == null)
                            {
                                continue;
                            }
                            string contentPath = node.Attributes["href"].Value + "";
                            if (contentPath.IndexOf("http://") == -1)
                            {
                                contentPath = htmlPath + "\\" + contentPath;
                            }
                            strContent.Append(GetContent(contentPath));
                        }
                    }
                }
                FileHelper.FileSave(this.tbxSavePath.Text, strContent.ToString().Replace("&nbsp;", ""));
                MessageBox.Show("抓取成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show("抓取失败!原因:\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion 开始检索

        #endregion **************************************tab页1***************************************

        #region **************************************tab页2***************************************

        #region 页类别

        private void cbxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetNodeList();
        }

        #endregion 页类别

        #region 添加节点

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tbxName.Text.Trim() == "" || this.tbxValue.Text.Trim() == "")
                {
                    MessageBox.Show("请输入要添加的节点名或值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (this.tbxName.Text.Trim().Length > 20)
                {
                    MessageBox.Show("节点名小于20字符!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Regex.IsMatch(this.tbxName.Text.Trim(), @"^[1-9]\d*$"))
                {
                    MessageBox.Show("节点名不能为纯数字!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                XmlHelper xhelp = new XmlHelper(_TemplateDict[this.cbxType2.Text]);
                if (xhelp.GetNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()) != null)
                {
                    MessageBox.Show("节点名重复!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (xhelp.InsertNode(this.tbxName.Text.Trim(), this.tbxValue.Text.Trim(), false, "//Template/" + this.cbxPage.Text))
                {
                    MessageBox.Show("添加成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GetNodeList();
                }
                else
                {
                    MessageBox.Show("添加失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败!错误信息:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion 添加节点

        #region 修改节点

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tbxName.Text.Trim() == "" || this.tbxValue.Text.Trim() == "")
                {
                    MessageBox.Show("请输入要添加的节点名或值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (this.tbxName.Text.Trim().Length > 20)
                {
                    MessageBox.Show("节点名小于20字符!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                XmlHelper xhelp = new XmlHelper(_TemplateDict[this.cbxType2.Text]);
                if (xhelp.GetNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()) == null)
                {
                    MessageBox.Show("该节点不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (xhelp.UpdateNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim(), this.tbxValue.Text.Trim()))
                {
                    MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GetNodeList();
                }
                else
                {
                    MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新失败!错误信息" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion 修改节点

        #region 删除节点

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.tbxValue.Text = "";
                if (this.tbxName.Text.Trim() == "")
                {
                    MessageBox.Show("请输入要删除的节点名!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                XmlHelper xhelp = new XmlHelper(_TemplateDict[this.cbxType2.Text]);
                if (xhelp.GetNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()) == null)
                {
                    MessageBox.Show("该节点不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (xhelp.RemoveNodes("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()))
                {
                    MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GetNodeList();
                }
                else
                {
                    MessageBox.Show("删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败!错误原因:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion 删除节点

        #endregion **************************************tab页2***************************************

        #region **************************************公共方法***************************************

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

        #region 加载节点集合

        /// <summary>
        /// 加载节点集合
        /// </summary>
        private void GetNodeList()
        {
            XmlHelper xhelp = new XmlHelper(_TemplateDict[this.cbxType2.Text]);
            XmlNodeList nodeList = xhelp.GetNode("//Template/" + this.cbxPage.Text).ChildNodes;
            this.rtbxXml.Clear();
            if (nodeList != null && nodeList.Count > 0)
            {
                foreach (XmlNode item in nodeList)
                {
                    this.rtbxXml.AppendText("节点名:" + item.Name.PadRight(20, ' ') + "\t值:" + item.InnerText + "\r\n");
                }
            }
        }

        #endregion 加载节点集合

        #region 获取xpath

        /// <summary>
        /// 获取xpath
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private List<string> GetNodeXpathList(string pageType)
        {
            List<string> rList = new List<string>();
            XmlHelper xhelp = new XmlHelper(_TemplateDict[this.cbxType2.Text]);
            XmlNodeList nodeList = xhelp.GetNodeList("//Template/" + pageType);
            if (nodeList != null && nodeList.Count > 0)
            {
                foreach (XmlNode item in nodeList)
                {
                    rList.Add(item.InnerText);
                }
            }
            return rList;
        }

        #endregion 获取xpath

        #region 根据url抓取所有页面内容

        /// <summary>
        /// 根据url抓取所有页面内容
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private string GetGeneralContent(string strUrl)
        {
            string strMsg = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(strUrl);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                strMsg = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strMsg.Trim("\r\n".ToCharArray()).Trim("\n".ToCharArray());
        }

        #endregion 根据url抓取所有页面内容

        #region 递归求出所有的节点

        /// <summary>
        /// 递归求出所有的节点
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        private HtmlAP.HtmlNodeCollection GetALLNodeList(HtmlAP.HtmlNode rootNode, HtmlAP.HtmlNodeCollection nodeList = null)
        {
            if (nodeList == null)
            {
                nodeList = new HtmlAP.HtmlNodeCollection(rootNode);
            }
            HtmlAP.HtmlNodeCollection newList = rootNode.ChildNodes;
            if (newList != null)
            {
                foreach (var item in newList)
                {
                    GetALLNodeList(item, nodeList);
                }
            }
            if (newList != null && newList.Count == 1 && newList[0].Name.ToLower() == "#text")
            {
                nodeList.Add(rootNode);
            }
            return nodeList;
        }

        #endregion 递归求出所有的节点

        #region 获取内文文件的内容

        /// <summary>
        /// 获取内文文件的内容
        /// </summary>
        /// <param name="htmlPath"></param>
        /// <returns></returns>
        private string GetContent(string htmlPath)
        {
            StringBuilder strContent = new StringBuilder();
            try
            {
                htmlPath = htmlPath.Replace("\\", "/").Replace("http:/", "http://");
                HtmlAP.HtmlDocument htmlDoc = new HtmlAP.HtmlDocument();
                string htmlstr = GetGeneralContent(htmlPath);
                htmlDoc.LoadHtml(htmlstr);
                HtmlAP.HtmlNode rootNode = htmlDoc.DocumentNode;
                List<string> contentXpathList = GetNodeXpathList("内文");
                if (contentXpathList == null)
                {
                    return "";
                }
                foreach (var item in contentXpathList)
                {
                    HtmlAP.HtmlNode contentNode = rootNode.SelectSingleNode(item);
                    if (contentNode == null)
                    {
                        continue;
                    }
                    strContent.Append(contentNode.InnerHtml.Replace("<br/>", "\r\n").Replace("<br>", "\r\n"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strContent.ToString();
        }

        #endregion 获取内文文件的内容

        #endregion **************************************公共方法***************************************
    }
}