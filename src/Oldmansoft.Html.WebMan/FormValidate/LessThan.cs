﻿using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    class LessThan : Validator
    {
        public object Value { get; set; }

        protected override void Set(JsonObject json)
        {
            json.Set("value", Value);
        }
    }
}
