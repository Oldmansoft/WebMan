using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    class FixedCount : Validator
    {
        public uint Count { get; set; }

        protected override void Set(JsonObject json)
        {
            json.Set("count", Count);
        }
    }
}
