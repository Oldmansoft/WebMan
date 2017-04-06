﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 数据源
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class DataTableSource<TModel> where TModel : class
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
        internal DataTableSource(IEnumerable<TModel> source, DataTableRequest request, int totalCount)
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
            var items = ModelProvider.Instance.GetItems(typeof(TModel));
            foreach(var model in source)
            {
                var dataItem = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    var value = item.Property.GetValue(model);
                    if (value == null)
                    {
                        dataItem.Add(item.Name, string.Empty);
                    }
                    else
                    {
                        dataItem.Add(item.Name, ValueDisplay.Instance.Convert(item.Property.PropertyType, value, item));
                    }
                }
                data.Add(dataItem);
            }
        }
    }
}
