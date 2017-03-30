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
    /// <typeparam name="TModel"></typeparam>
    public class DataTablesSource<TModel> where TModel : class
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
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <param name="totalCount"></param>
        public DataTablesSource(IEnumerable<TModel> source, DataTablesRequest request, int totalCount = 0)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (request == null) throw new ArgumentNullException("request");
            if (totalCount == 0) totalCount = source.Count();
            draw = request.draw;
            recordsTotal = totalCount;
            recordsFiltered = totalCount;
            data = new List<Dictionary<string, string>>();
            SetData(source);
        }

        private void SetData(IEnumerable<TModel> source)
        {
            var type = typeof(TModel);
            var propertys = type.GetProperties();
            foreach(var item in source)
            {
                var model = new Dictionary<string, string>();
                foreach (var property in propertys)
                {
                    var value = property.GetValue(item);
                    if (value == null)
                    {
                        model.Add(property.Name, string.Empty);
                    }
                    else
                    {
                        model.Add(property.Name, ValueDisplay.Instance.Convert(property.PropertyType, value));
                    }
                }
                data.Add(model);
            }
        }
    }
}
