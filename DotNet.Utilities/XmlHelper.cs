using System.IO;
using System.Xml;

namespace DotNet.Utilities
{
    public class XmlHelper
    {
        #region 字段

        /// <summary>
        /// xml物理路径
        /// </summary>
        private string XmlFilePath { get; set; }

        /// <summary>
        /// xml文档
        /// </summary>
        private XmlDocument XmlDoc { get; set; }

        /// <summary>
        /// xml根节点
        /// </summary>
        private XmlElement XmlElement { get; set; }

        #endregion 字段

        #region 构造方法

        /// <summary>
        /// 实例化XmlHelper对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的物理路径</param>
        public XmlHelper(string xmlFilePath)
        {
            XmlFilePath = xmlFilePath;
            CreateXmlElement();
        }

        #endregion 构造方法

        #region 创建Xml的根节点

        /// <summary>
        /// 创建Xml的根节点
        /// </summary>
        private void CreateXmlElement()
        {
            //创建一个XML对象
            XmlDoc = new XmlDocument();

            if (File.Exists(XmlFilePath))
            {
                //加载XML文件
                XmlDoc.Load(this.XmlFilePath);
            }

            //为XML的根节点赋值
            XmlElement = XmlDoc.DocumentElement;
        }

        #endregion 创建Xml的根节点

        #region 获取指定XPath表达式的节点对象

        /// <summary>
        /// 获取指定XPath表达式的节点对象
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public XmlNode GetNode(string xPath)
        {
            //返回XPath节点
            return XmlElement.SelectSingleNode(xPath);
        }

        #endregion 获取指定XPath表达式的节点对象

        #region 获取指定XPath表达式节点的值

        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public string GetValue(string xPath)
        {
            //返回XPath节点的值
            return XmlElement.SelectSingleNode(xPath).InnerText;
        }

        #endregion 获取指定XPath表达式节点的值

        #region 获取指定XPath表达式节点的属性值

        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public string GetAttributeValue(string xPath, string attributeName)
        {
            //返回XPath节点的属性值
            return XmlElement.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }

        #endregion 获取指定XPath表达式节点的属性值

        #region 新增节点

        /// <summary>
        /// 新增节点。
        /// </summary>
        /// <param name="xmlNode">要插入的Xml节点</param>
        public void AppendNode(XmlNode xmlNode)
        {
            //导入节点
            XmlNode node = XmlDoc.ImportNode(xmlNode, true);

            //将节点插入到根节点下
            XmlElement.AppendChild(node);
        }

        #endregion 新增节点

        #region 删除节点

        /// <summary>
        /// 删除指定XPath表达式的节点
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public void RemoveNode(string xPath)
        {
            //获取要删除的节点
            XmlNode node = XmlDoc.SelectSingleNode(xPath);

            //删除节点
            XmlElement.RemoveChild(node);
        }

        #endregion 删除节点

        #region 保存Xml文件

        /// <summary>
        /// 保存Xml文件
        /// </summary>
        public void Save()
        {
            //保存XML文件
            XmlDoc.Save(this.XmlFilePath);
        }

        #endregion 保存Xml文件

        #region 静态方法

        #region 创建根节点对象

        /// <summary>
        /// 创建根节点对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的物理路径</param>
        private static XmlElement CreateRootElement(string xmlFilePath)
        {
            //定义变量，表示XML文件的绝对路径
            string filePath = xmlFilePath;

            //创建XmlDocument对象
            XmlDocument xmlDocument = new XmlDocument();
            //加载XML文件
            xmlDocument.Load(filePath);

            //返回根节点
            return xmlDocument.DocumentElement;
        }

        #endregion 创建根节点对象

        #region 静态方法-获取指定XPath表达式节点的值

        /// <summary>
        /// 静态方法-获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public static string GetValue(string xmlFilePath, string xPath)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);

            //返回XPath节点的值
            return rootElement.SelectSingleNode(xPath).InnerText;
        }

        #endregion 静态方法-获取指定XPath表达式节点的值

        #region 静态方法-获取指定XPath表达式节点的属性值

        /// <summary>
        /// 静态方法-获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public static string GetAttributeValue(string xmlFilePath, string xPath, string attributeName)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);

            //返回XPath节点的属性值
            return rootElement.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }

        #endregion 静态方法-获取指定XPath表达式节点的属性值

        #endregion 静态方法
    }
}