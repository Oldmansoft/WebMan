namespace Oldmansoft.Html
{
    /// <summary>
    /// 元素接口
    /// </summary>
    public interface IHtmlElement : IHtmlNode
    {
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string Attribute(HtmlAttribute name);
        
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IHtmlElement Attribute(HtmlAttribute name, string value);
        
        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IHtmlElement RemoveAttribute(HtmlAttribute name);

        /// <summary>
        /// 获取数据属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string Data(string name);

        /// <summary>
        /// 设置数据属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IHtmlElement Data(string name, string value);

        /// <summary>
        /// 移除数据属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IHtmlElement RemoveData(string name);

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IHtmlElement AddClass(string name);

        /// <summary>
        /// 移除样式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IHtmlElement RemoveClass(string name);

        /// <summary>
        /// 获取样式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string Css(string name);

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IHtmlElement Css(string name, string value);

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        IHtmlElement Css(object properties);

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        IHtmlElement Append(IHtmlNode node);

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        IHtmlElement Prepend(IHtmlNode node);

        /// <summary>
        /// 元素后贴
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        IHtmlElement After(IHtmlNode node);

        /// <summary>
        /// 元素前贴
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        IHtmlElement Before(IHtmlNode node);

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IHtmlElement Text(string text);

        /// <summary>
        /// 客户端事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        IHtmlElement OnClient(HtmlEvent e, string script);
    }
}
