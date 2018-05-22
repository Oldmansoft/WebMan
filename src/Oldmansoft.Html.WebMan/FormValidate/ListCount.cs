using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    class ListCount : Validator
    {
        public uint? Fixed { get; set; }

        public bool? Inclusive { get; set; }

        public uint? Min { get; set; }

        public uint? Max { get; set; }

        protected override void Set(JsonObject json)
        {
            if (Fixed.HasValue) json.Set("fixed", Fixed.Value);
            if (Inclusive.HasValue) json.Set("inclusive", Inclusive.Value);
            if (Min.HasValue) json.Set("min", Min.Value);
            if (Max.HasValue) json.Set("max", Max.Value);
        }
    }
}
