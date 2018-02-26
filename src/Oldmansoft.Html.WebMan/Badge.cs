using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 徽章脚本
    /// </summary>
    public class Badge : Element.Script
    {
        private ILocation Location { get; set; }

        private int Value { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="location">位置</param>
        /// <param name="value">值</param>
        public Badge(ILocation location, int value)
        {
            if (location == null) throw new ArgumentNullException("location");
            Location = location;
            Value = value;
        }

        internal override bool HasContent()
        {
            return true;
        }

        internal override void SetListFromContent(IList<string> list)
        {
            list.Add(string.Format("$man.badge(\"{0}\", \"{1}\");", Location.Path, Value == 0 ? "" : Value.ToString()));
            base.SetListFromContent(list);
        }
    }
}
