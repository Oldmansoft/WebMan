using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Util
{
    abstract class EnumFlags<T>
    {
        private IList<T> Values { get; set; }

        public EnumFlags()
        {
            Values = new List<T>();
            var type = typeof(T);
            if (!type.IsEnum) throw new ArgumentException("只支持泛型");
            foreach (var item in Enum.GetValues(type))
            {
                var value = (T)item;
                if (Ignore(value)) continue;
                Values.Add(value);
            }
        }

        protected virtual bool Ignore(T item)
        {
            return false;
        }

        protected abstract bool In(T source, T target);

        public T[] From(T items)
        {
            var result = new List<T>();
            foreach (var item in Values)
            {
                if (In(item, items))
                {
                    result.Add(item);
                }
            }
            return result.ToArray();
        }
    }
}
