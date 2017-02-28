using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 内容输出接口
    /// </summary>
    public interface IHtmlOutput
    {
        /// <summary>
        /// 添加字符串
        /// </summary>
        /// <param name="value"></param>
        void Append(string value);

        /// <summary>
        /// 添加属性名
        /// </summary>
        /// <param name="attribute"></param>
        void Append(HtmlAttribute attribute);

        /// <summary>
        /// 添加标签名
        /// </summary>
        /// <param name="tag"></param>
        void Append(HtmlTag tag);

        /// <summary>
        /// 添加字符
        /// </summary>
        void Append(HtmlChar c);

        /// <summary>
        /// 获取序号生成器
        /// </summary>
        IGenerator<int> Generator { get; }
        
        /// <summary>
        /// 存储项
        /// </summary>
        IList<string> Items { get; }

        /// <summary>
        /// 当完成时
        /// </summary>
        Action<IHtmlOutput> OnCompleted { get; set; }
    }
}
