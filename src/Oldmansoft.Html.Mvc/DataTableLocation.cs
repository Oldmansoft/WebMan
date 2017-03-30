using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Mvc
{
    /// <summary>
    /// 数据列表源路径
    /// </summary>
    public class DataTableLocation : ILocation
    {
        private string Value { get; set; }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="dataSource"></param>
        public DataTableLocation(Func<DataTableRequest, System.Web.Mvc.JsonResult> dataSource)
        {
            Value = dataSource.Method.GetMethodLocation();
        }
        
        string ILocation.Location
        {
            get
            {
                return Value;
            }
        }
    }
}
