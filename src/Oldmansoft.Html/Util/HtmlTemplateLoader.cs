using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// 模板加载器
    /// </summary>
    public class HtmlTemplateLoader
    {
        private string BasePath { get; set; }

        private System.Collections.Concurrent.ConcurrentDictionary<string, HtmlTemplate> Templates { get; set; }

        /// <summary>
        /// 创建模板加载器
        /// </summary>
        /// <param name="path"></param>
        public HtmlTemplateLoader(string path)
        {
            BasePath = string.Format("{0}{1}/", AppDomain.CurrentDomain.BaseDirectory, path);
            Templates = new System.Collections.Concurrent.ConcurrentDictionary<string, HtmlTemplate>();
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
