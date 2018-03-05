using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 主页文档
    /// </summary>
    [Obsolete("请使用 ManageDoucment")]
    public class MainDocument : ManageDocument
    {
        /// <summary>
        /// 创建文档
        /// </summary>
        /// <param name="defaultLink"></param>
        public MainDocument(ILocation defaultLink)
            : base(defaultLink)
        { }
    }
}
