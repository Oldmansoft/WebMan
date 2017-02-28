using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 序号生成器
    /// </summary>
    /// <typeparam name="T">序号类型</typeparam>
    public interface IGenerator<T>
    {
        /// <summary>
        /// 获取下一个值
        /// </summary>
        /// <returns></returns>
        T Next();
    }
}
