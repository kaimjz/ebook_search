namespace SearchForm
{
    public class NodeModel
    {
        public NodeModel()
        {
        }

        public NodeModel(int prev)
        {
            this.Id = prev + 1;
            this.Name = "";
            this.NodeType = 0;
            this.AttrHref = "";
        }

        public int Id { get; set; }

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 节点类型  0卷名,1章节名(有属性href)
        /// </summary>
        public int NodeType { get; set; }

        /// <summary>
        /// 属性href
        /// </summary>
        public string AttrHref { get; set; }

        /// <summary>
        /// 节点内容
        /// </summary>
        public string Content { get; set; }
    }
}