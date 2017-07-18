using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    class Regexp : Validator
    {
        public string Pattern { get; set; }

        protected override void Set(JsonObject json)
        {
            json.Set("regexp", new JsonRaw(string.Format("/{0}/i", Pattern)));
        }
    }
}
