namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格列
    /// </summary>
    class DataTableColumn : ITableColumn
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 属性
        /// </summary>
        public System.Reflection.PropertyInfo Property { get; private set; }

        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible { get; set; }
        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="text"></param>
        /// <param name="visible"></param>
        public DataTableColumn(string name, System.Reflection.PropertyInfo property, string text, bool visible)
        {
            Name = name;
            Property = property;
            Text = text;
            Visible = visible;
        }
    }
}
