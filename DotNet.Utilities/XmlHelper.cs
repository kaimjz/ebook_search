using System;
using System.Collections;
using System.IO;
using System.Text;
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
        /// xml节点
        /// </summary>
        private XmlElement XmlElement { get; set; }

        #endregion 字段

        #region 构造方法

        //public XmlHelper()
        //{
        //}

        /// <summary>
        /// 实例化XmlHelper对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的物理路径</param>
        /// <param name="isCreateXmlDoc">是否创建xml对象</param>
        public XmlHelper(string xmlFilePath, bool isCreateXmlDoc = true)
        {
            XmlFilePath = xmlFilePath;
            if (isCreateXmlDoc)
            {
                CreateXmlElement();
            }
        }

        #endregion 构造方法

        #region 创建Xml文档

        /// <summary>
        /// 创建一个带有根节点的Xml文件
        /// </summary>
        /// <param name="rootName">根节点名称</param>
        /// <param name="encode">编码方式:"gb2312"，"UTF-8"等常见的</param>
        /// <returns></returns>
        public bool CreateXmlFile(string rootName, Encoding encode)
        {
            try
            {
                XmlDeclaration xmldecl;
                xmldecl = XmlDoc.CreateXmlDeclaration("1.0", encode.ToString(), null);
                XmlDoc.AppendChild(xmldecl);
                XmlElement = XmlDoc.CreateElement("", rootName, "");
                XmlDoc.AppendChild(XmlElement);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion 创建Xml文档

        #region 插入一个节点和它的若干子节点

        /// <summary>
        /// 插入一个节点和它的若干子节点
        /// </summary>
        /// <param name="NewNodeName">插入的节点名称</param>
        /// <param name="NewNodeInnerText">插入的节点内容</param>
        /// <param name="HasAttributes">此节点是否具有属性，True为有，False为无</param>
        /// <param name="fatherXPath">此插入节点的父节点,要匹配的XPath表达式</param>
        /// <param name="htAtt">此节点的属性 可为null</param>
        /// <param name="htSubNode">子节点的属性 可为null</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool InsertNode(string NewNodeName, string NewNodeInnerText, bool HasAttributes, string fatherXPath, Hashtable htAtt = null, Hashtable htSubNode = null)
        {
            try
            {
                XmlNode root = XmlDoc.SelectSingleNode(fatherXPath);
                XmlElement = XmlDoc.CreateElement(NewNodeName);
                XmlElement.InnerText = NewNodeInnerText;
                if (htAtt != null && HasAttributes)//若此节点有属性，则先添加属性
                {
                    SetAttributes(XmlElement, htAtt);
                    SetNodes(XmlElement, htSubNode);//添加完此节点属性后，再添加它的子节点和它们的InnerText
                }
                else
                {
                    SetNodes(XmlElement, htSubNode);//若此节点无属性，那么直接添加它的子节点
                }
                root.AppendChild(XmlElement);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion 插入一个节点和它的若干子节点

        #region 更新节点

        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="fatherXPath">需要更新的节点,要匹配的XPath表达式(例如:"//节点名//子节点名)</param>
        /// <param name="htAtt">需要更新的属性表，Key代表需要更新的属性，Value代表更新后的值</param>
        /// <param name="htSubNode">需要更新的子节点的属性表，Key代表需要更新的子节点名字Name,Value代表更新后的值InnerText</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool UpdateNode(string xPath, string innerText, Hashtable htAtt = null, Hashtable htSubNode = null)
        {
            try
            {
                XmlNode root = XmlDoc.SelectSingleNode(xPath);
                UpdateNode(root, innerText, htAtt, htSubNode);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion 更新节点

        #region 删除指定节点下的节点

        /// <summary>
        /// 删除指定节点下的节点
        /// </summary>
        /// <param name="fatherXPath">制定节点,要匹配的XPath表达式(例如:"//节点名//子节点名)</param>
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool RemoveNodes(string xpath)
        {
            try
            {
                XmlNode xmlnode = XmlDoc.SelectSingleNode(xpath);
                xmlnode.RemoveAll();
                xmlnode.ParentNode.RemoveChild(xmlnode);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion 删除指定节点下的节点

        #region 私有方法

        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="xe">节点所处的Element</param>
        /// <param name="htAttribute">节点属性，Key代表属性名称，Value代表属性值</param>
        private void SetAttributes(XmlElement xe, Hashtable htAttribute)
        {
            foreach (DictionaryEntry de in htAttribute)
            {
                xe.SetAttribute(de.Key.ToString(), de.Value.ToString());
            }
        }

        /// <summary>
        /// 增加子节点
        /// </summary>
        /// <param name="rootNode">上级节点名称</param>
        /// <param name="rootXe">父根节点所属的Element</param>
        /// <param name="SubNodes">子节点属性，Key为Name值，Value为InnerText值</param>
        private void SetNodes(XmlElement rootXe, Hashtable SubNodes)
        {
            if (SubNodes == null)
                return;
            foreach (DictionaryEntry de in SubNodes)
            {
                XmlElement subNode = XmlDoc.CreateElement(de.Key.ToString());
                subNode.InnerText = de.Value.ToString();
                rootXe.AppendChild(subNode);
            }
        }

        /// <summary>
        /// 更新节点属性和子节点InnerText值。
        /// </summary>
        /// <param name="root">根节点名字</param>
        /// <param name="htAtt">需要更改的属性名称和值</param>
        /// <param name="htSubNode">需要更改InnerText的子节点名字和值</param>
        private void UpdateNode(XmlNode root, string innerText, Hashtable htAtt, Hashtable htSubNode)
        {
            XmlElement = (XmlElement)root;
            if (!string.IsNullOrEmpty(innerText) && XmlElement.InnerText != innerText)
            {
                XmlElement.InnerText = innerText;
            }
            if (XmlElement.HasAttributes && htAtt != null)//如果节点如属性，则先更改它的属性
            {
                foreach (DictionaryEntry de in htAtt)//遍历属性哈希表
                {
                    if (XmlElement.HasAttribute(de.Key.ToString()))//如果节点有需要更改的属性
                    {
                        XmlElement.SetAttribute(de.Key.ToString(), de.Value.ToString());//则把哈希表中相应的值Value赋给此属性Key
                    }
                }
            }
            if (XmlElement.HasChildNodes && htSubNode != null)//如果有子节点，则修改其子节点的InnerText
            {
                XmlNodeList xnl = XmlElement.ChildNodes;
                foreach (XmlNode xn1 in xnl)
                {
                    XmlElement xe = (XmlElement)xn1;
                    foreach (DictionaryEntry de in htSubNode)
                    {
                        if (xe.Name == de.Key.ToString())//htSubNode中的key存储了需要更改的节点名称，
                        {
                            xe.InnerText = de.Value.ToString();//htSubNode中的Value存储了Key节点更新后的数据
                        }
                    }
                }
            }
        }

        #endregion 私有方法

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

        /// <summary>
        /// 获取指定XPath表达式的节点对象
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public XmlNodeList GetNodeList(string xPath)
        {
            //返回XPath节点
            return XmlElement.SelectNodes(xPath);
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