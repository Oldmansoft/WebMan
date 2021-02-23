using System;
using System.Collections.Concurrent;
using System.Text;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// 模板加载器
    /// </summary>
    public class HtmlTemplateLoader
    {
        private readonly string BasePath;

        private readonly ConcurrentDictionary<string, HtmlTemplate> Templates;

        /// <summary>
        /// 创建模板加载器
        /// </summary>
        /// <param name="path"></param>
        public HtmlTemplateLoader(string path)
        {
            BasePath = string.Format("{0}{1}/", AppDomain.CurrentDomain.BaseDirectory, path);
            Templates = new ConcurrentDictionary<string, HtmlTemplate>();
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HtmlTemplate Load(string name)
        {
            if (Templates.ContainsKey(name)) return Templates[name];

            string value = System.IO.File.ReadAllText(System.IO.Path.Combine(BasePath, string.Format("{0}.html", name)), Encoding.UTF8);
            Templates[name] = new HtmlTemplate(value);
            return Templates[name];
        }
    }
}
