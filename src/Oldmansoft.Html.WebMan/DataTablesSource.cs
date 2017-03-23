using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 数据源
    /// </summary>
    public class DataTablesSource
    {
        /// <summary>
        /// 绘制计数器
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// 过滤后的记录数
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// 表中中需要显示的数据
        /// </summary>
        public List<Dictionary<string, string>> data { get; set; }

        /// <summary>
        /// 可以定义一个错误来描述服务器出了问题后的友好提示
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="request"></param>
        /// <param name="totalCount"></param>
        /// <param name="source"></param>
        public DataTablesSource(DataTablesRequest request, int totalCount, IEnumerable<object> source)
        {
            draw = request.draw;
            recordsTotal = totalCount;
            recordsFiltered = totalCount;
            data = new List<Dictionary<string, string>>();
            SetData(source);
        }

        private void SetData(IEnumerable<object> source)
        {
            foreach(var item in source)
            {
                var model = new Dictionary<string, string>();
                foreach (var property in item.GetType().GetProperties())
                {
                    var value = property.GetValue(item);
                    if(value == null)
                    {
                        model.Add(property.Name, string.Empty);
                        continue;
                    }
                    string valueString;
                    if (property.PropertyType == typeof(Guid))
                    {
                        valueString = ((Guid)value).ToString("N");
                    }
                    else
                    {
                        valueString = value.ToString().HtmlEncode();
                    }
                    model.Add(property.Name, valueString);
                }
                data.Add(model);
            }
        }
    }
}
