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
        private DataTableLocation(Func<DataTablesRequest, System.Web.Mvc.JsonResult> dataSource)
        {
            Value = dataSource.Method.GetMethodLocation();
        }

        /// <summary>
        /// 创建数据列表源路径
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static ILocation Create(Func<DataTablesRequest, System.Web.Mvc.JsonResult> dataSource)
        {
            return new DataTableLocation(dataSource);
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
