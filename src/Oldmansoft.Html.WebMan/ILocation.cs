using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 路径提供者
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// 路径
        /// </summary>
        string Location { get; }
    }
}
