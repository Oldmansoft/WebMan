using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    /// <summary>
    /// 相同
    /// </summary>
    class Identical : Validator
    {
        public string OtherProperty { get; set; }

        protected override void Set(JsonObject json)
        {
            json.Set("field", OtherProperty);
        }
    }
}
