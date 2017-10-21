using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 列表数据源
    /// </summary>
    public class ListDataSource
    {
        private IDictionary<string, List<ListDataItem>> Source { get; set; }

        /// <summary>
        /// 创建数据源
        /// </summary>
        public ListDataSource()
        {
            Source = new Dictionary<string, List<ListDataItem>>();
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal bool Contains(string key)
        {
            return Source.ContainsKey(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal IList<ListDataItem> Get(string key)
        {
            if (!Contains(key))
            {
                return new List<ListDataItem>();
            }
            return Source[key];
        }

        /// <summary>
        /// 获取
        /// 可空
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal IList<ListDataItem> GetCanNull(string key)
        {
            if (!Contains(key))
            {
                return null;
            }
            return Source[key];
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="list"></param>
        public void Set(string key, IList<ListDataItem> list)
        {
            if (list == null) return;
            if (list.Count == 0) return;
            if (Contains(key))
            {
                Source[key] = new List<ListDataItem>(list);
            }
            else
            {
                Source.Add(key, new List<ListDataItem>(list));
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<ListDataItem> this[string key]
        {
            get
            {
                if (!Contains(key))
                {
                    Source.Add(key, new List<ListDataItem>());
                }
                return Source[key];
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            Source.Remove(key);
        }
    }
}
