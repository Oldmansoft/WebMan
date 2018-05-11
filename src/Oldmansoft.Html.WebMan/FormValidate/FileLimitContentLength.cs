using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    class FileLimitContentLength : Validator
    {
        public uint Length { get; set; }

        protected override void Set(JsonObject json)
        {
            json.Set("length", Length);
        }
    }
}
