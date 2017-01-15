using DotNet.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
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

        private string _TemplateName;//当前模板名

        private Encoding _Encode;//编码格式

        private System.Timers.Timer _TimerStatistics;//定时器

        private delegate void DelegateBar();//委托控制进度条

        private List<NodeModel> _NodeList = new List<NodeModel>();//所有节点

        //要剔除的字 符号
        private List<string> replaces = new List<string>() { "&nbsp;", "<", ">", "《", "》", "冇" };

        #endregion 字段

        #region 构造+初始

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _TemplateDict = GetTemplateList();//获取模板集合
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
            this.cbxEncode.SelectedIndex = 0;
            this.cbxPage.SelectedIndex = 0;
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

        #region tab

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1)
            {
                LoadNodeList();
            }
            if (e.TabPageIndex == 2)
            {
                LoadChar();
            }
        }

        #endregion tab

        #region 关闭

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗?", "友情提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
        }

        #endregion 关闭

        #region **************************************tab页1***************************************

        #region 读取路径更改时, 动态更改内文路径
        private void tbxReadPath_TextChanged(object sender, EventArgs e)
        {
            this.tbxContentPath.Text = this.tbxReadPath.Text.Trim();
        }
        #endregion

        #region 文件保存地址

        private void tbxBookName_TextChanged(object sender, EventArgs e)
        {
            this.tbxSavePath.Text = Path.GetDirectoryName(this.tbxSavePath.Text.Trim()) + "\\" +
                (this.tbxBookName.Text.Trim() == "" ? "保存文件" : this.tbxBookName.Text.Trim()) + ".txt";
        }

        #endregion 文件保存地址

        #region 模板名

        private void cbxType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _TemplateName = this.cbxType1.Text;
        }

        #endregion 模板名

        #region 编码格式

        private void cbxEncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((System.Windows.Forms.ComboBox)(sender)).SelectedItem.ToString())
            {
                case "Default/GB2312":
                    _Encode = Encoding.Default;
                    break;

                case "UTF8":
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
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "请选择保存路径";
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbxSavePath.Text = folderDialog.SelectedPath + "\\" + (this.tbxBookName.Text.Trim() == "" ? "保存文件" : this.tbxBookName.Text.Trim()) + ".txt";
            }
        }

        #endregion 保存路径

        #region 开始检索

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.tbxReadPath.Text.Trim() == "")
            {
                MessageBox.Show("请输入读取路径!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            if (this.tbxSign.Text.Trim() == "")
            {
                MessageBox.Show("请输入读取标记!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            if (this.tbxSavePath.Text.Trim() == "")
            {
                MessageBox.Show("请输入保存路径!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(this.tbxSavePath.Text.Trim())))
            {
                MessageBox.Show("保存路径不存在!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            try
            {
                _TimerStatistics = new System.Timers.Timer();
                _TimerStatistics.Interval = 1;
                _TimerStatistics.Elapsed += new System.Timers.ElapsedEventHandler(TimerStatistics_Elapsed);
                _TimerStatistics.Enabled = true;
                _TimerStatistics.Start();

                this.btnStart.Enabled = false;
                StringBuilder strContent = new StringBuilder();

                this.progressBar1.Value = 0;
                this.lblPercent.Text = "0%";

                List<NodeModel> nodeList = GetNodeModelList();
                if (nodeList.Count <= 0)
                {
                    this.btnStart.Enabled = true;
                    MessageBox.Show("抓取失败!原因:\r\n未找到任何可以跟踪的节点!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int theardSize = int.Parse(ConfigurationManager.AppSettings["TheardSize"] ?? "100");
                this.progressBar1.Maximum = nodeList.Count;
                for (int i = 0; i < nodeList.Count / theardSize + 1; i++)
                {
                    Thread th1 = new Thread(new ParameterizedThreadStart(InsertNodeModelList));
                    th1.Start(nodeList.Skip(theardSize * i).Take(theardSize).ToList());
                }
            }
            catch (Exception ex)
            {
                this.btnStart.Enabled = true;
                MessageBox.Show("抓取失败!原因:\r\n" + ex.Message, "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion 开始检索

        #region Timer委托方法

        private void TimerStatistics_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_TimerStatistics.Interval == 1)
            {
                _TimerStatistics.Interval = 5000;
            }
            if (_NodeList.Count == this.progressBar1.Maximum && _NodeList.Count != 0)
            {
                try
                {
                    _TimerStatistics.Stop();
                    _TimerStatistics.Close();
                    StringBuilder strContent = new StringBuilder();
                    var nodeList = _NodeList.OrderBy(f => f.Id).ToList() ?? new List<NodeModel>();
                    nodeList.ForEach(a =>
                    {
                        strContent.AppendLine(a.Name);
                        if (a.NodeType == 1)
                        {
                            strContent.AppendLine(a.Content);
                        }
                    });
                    string sstr = new Regex("<.*?>|《.*?》").Replace(strContent.ToString(), "");

                    if (this.tbxBookName.Text.Trim() != "")
                    {
                        replaces.Add(this.tbxBookName.Text.Trim());
                    }
                    replaces.ForEach(a =>
                    {
                        sstr = sstr.Replace(a, "");
                    });
                    FileHelper.FileSave(this.tbxSavePath.Text, sstr);
                    MessageBox.Show("抓取成功!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("抓取失败!原因:\r\n" + ex.Message, "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ControlBtn();
                    _NodeList = new List<NodeModel>();
                    GC.Collect();
                }
            }
        }

        #endregion Timer委托方法

        #endregion **************************************tab页1***************************************

        #region **************************************tab页2***************************************

        #region 模板名

        private void cbxType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _TemplateName = this.cbxType2.Text;
        }

        #endregion 模板名

        #region 页类别

        private void cbxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNodeList();
        }

        #endregion 页类别

        #region 添加节点

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tbxName.Text.Trim() == "" || this.tbxValue.Text.Trim() == "")
                {
                    MessageBox.Show("请输入要添加的节点名或值!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (this.tbxName.Text.Trim().Length > 20)
                {
                    MessageBox.Show("节点名小于20字符!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (Regex.IsMatch(this.tbxName.Text.Trim(), @"^[1-9]\d*$"))
                {
                    MessageBox.Show("节点名不能为纯数字!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                XmlHelper xhelp = new XmlHelper(_TemplateDict[_TemplateName]);
                if (xhelp.GetNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()) != null)
                {
                    MessageBox.Show("节点名重复!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (xhelp.InsertNode(this.tbxName.Text.Trim(), this.tbxValue.Text.Trim(), false, "//Template/" + this.cbxPage.Text))
                {
                    MessageBox.Show("添加成功!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    LoadNodeList();
                }
                else
                {
                    MessageBox.Show("添加失败!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败!错误信息:" + ex.Message, "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
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
                    MessageBox.Show("请输入要修改的节点名或值!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (this.tbxName.Text.Trim().Length > 20)
                {
                    MessageBox.Show("节点名小于20字符!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                XmlHelper xhelp = new XmlHelper(_TemplateDict[_TemplateName]);
                if (xhelp.GetNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()) == null)
                {
                    MessageBox.Show("该节点不存在!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (xhelp.UpdateNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim(), this.tbxValue.Text.Trim()))
                {
                    MessageBox.Show("更新成功!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    LoadNodeList();
                }
                else
                {
                    MessageBox.Show("更新失败!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新失败!错误信息" + ex.Message, "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
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
                    MessageBox.Show("请输入要删除的节点名!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                XmlHelper xhelp = new XmlHelper(_TemplateDict[_TemplateName]);
                if (xhelp.GetNode("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()) == null)
                {
                    MessageBox.Show("该节点不存在!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (xhelp.RemoveNodes("//Template/" + this.cbxPage.Text + "/" + this.tbxName.Text.Trim()))
                {
                    MessageBox.Show("删除成功!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    LoadNodeList();
                }
                else
                {
                    MessageBox.Show("删除失败!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败!错误原因:" + ex.Message, "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        #endregion 删除节点

        #endregion **************************************tab页2***************************************

        #region **************************************tab页3***************************************

        private void btnAddChart_Click(object sender, EventArgs e)
        {
            if (this.tbxChar.Text.Trim() == "")
            {
                MessageBox.Show("请输入要添加的特殊字符!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            var str = replaces.Find(p => p == this.tbxChar.Text.Trim());
            if (str != null)
            {
                MessageBox.Show("已有该特殊字符!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                replaces.Add(this.tbxChar.Text.Trim());
                LoadChar();
            }
        }

        private void btnCharDel_Click(object sender, EventArgs e)
        {
            if (this.tbxChar.Text.Trim() == "")
            {
                MessageBox.Show("请输入要删除的特殊字符!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            var str = replaces.Find(p => p == this.tbxChar.Text.Trim());
            if (str == null)
            {
                MessageBox.Show("该特殊字符不存在!", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                replaces.Remove(this.tbxChar.Text.Trim());
                LoadChar();
            }
        }

        #endregion **************************************tab页3***************************************

        #region **************************************公共方法***************************************

        #region 获取模板集合

        /// <summary>
        /// 获取模板集合
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetTemplateList()
        {
            _TemplateDir = Application.StartupPath + "\\Template\\";
            string[] files = Directory.GetFiles(_TemplateDir);
            return files.ToDictionary(k => Path.GetFileNameWithoutExtension(k), v => v);
        }

        #endregion 获取模板集合

        #region 加载节点集合

        /// <summary>
        /// 加载节点集合
        /// </summary>
        private void LoadNodeList()
        {
            XmlHelper xhelp = new XmlHelper(_TemplateDict[_TemplateName]);
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
            XmlHelper xhelp = new XmlHelper(_TemplateDict[_TemplateName]);
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
                StreamReader reader = new StreamReader(response.GetResponseStream(), _Encode);

                strMsg = reader.ReadToEnd();

                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
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
            }
            return strContent.ToString();
        }

        #endregion 获取内文文件的内容

        #region 加载特殊字符

        /// <summary>
        /// 加载特殊字符
        /// </summary>
        private void LoadChar()
        {
            this.rtbxChar.Clear();
            replaces.ForEach(a =>
            {
                this.rtbxChar.AppendText(a + "\r\n");
            });
        }

        #endregion 加载特殊字符

        #region 获取节点自定义实体集合

        /// <summary>
        /// 获取节点自定义实体集合
        /// </summary>
        /// <returns></returns>
        private List<NodeModel> GetNodeModelList()
        {
            List<NodeModel> nModelList = new List<NodeModel>();
            HtmlAP.HtmlDocument htmlDoc = new HtmlAP.HtmlDocument();
            string htmlstr = GetGeneralContent(this.tbxReadPath.Text.Trim());
            htmlDoc.LoadHtml(htmlstr);
            HtmlAP.HtmlNode rootNode = htmlDoc.DocumentNode;
            List<string> catalogXpathList = GetNodeXpathList("目录");
            if (catalogXpathList == null)
            {
                return nModelList;
            }
            int num = 0;
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
                        NodeModel model = new NodeModel(num++);
                        model.Name = node.InnerText;// 章节名或者卷名
                        if (node.Attributes == null || node.Attributes.Count <= 0 || node.Attributes["href"] == null)
                        {
                            nModelList.Add(model);
                            continue;
                        }
                        string contentPath = node.Attributes["href"].Value + "";
                        if (contentPath.IndexOf("http://") == -1)
                        {
                            contentPath = this.tbxContentPath.Text.Trim() + contentPath;
                        }
                        model.NodeType = 1;
                        model.AttrHref = contentPath;
                        nModelList.Add(model);
                    }
                }
            }
            return nModelList;
        }

        #endregion 获取节点自定义实体集合

        #region 委托

        /// <summary>
        /// 委托控制进度条
        /// </summary>
        private void ControlBar()
        {
            if (this.progressBar1.InvokeRequired || this.lblPercent.InvokeRequired)
            {
                this.progressBar1.Invoke(new DelegateBar(ControlBar));
            }
            else
            {
                ++this.progressBar1.Value;
                this.lblPercent.Text = (int)((double)this.progressBar1.Value / (double)this.progressBar1.Maximum * 100) + "%";
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 委托控制进度条
        /// </summary>
        private void ControlBtn()
        {
            if (this.btnStart.InvokeRequired)
            {
                this.btnStart.Invoke(new DelegateBar(ControlBtn));
            }
            else
            {
                this.btnStart.Enabled = true;
            }
        }

        #endregion 委托

        #region 往全局变量里添加节点

        /// <summary>
        /// 往全局变量里添加节点
        /// </summary>
        /// <param name="objList"></param>
        private void InsertNodeModelList(object objList)
        {
            var nodeList = (List<NodeModel>)objList;
            if (nodeList == null || nodeList.Count <= 0)
            {
                return;
            }

            nodeList.ForEach(a =>
            {
                if (a.NodeType == 1)
                {
                    a.Content = GetContent(a.AttrHref);
                }
                ControlBar();
            });
            _NodeList.AddRange(nodeList);
        }

        #endregion 往全局变量里添加节点

        #endregion **************************************公共方法***************************************


    }
}