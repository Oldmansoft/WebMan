using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Document
{
    /// <summary>
    /// 启用资源
    /// </summary>
    public interface IEnabledResource
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        bool Enabled { get; set; }
    }
}
