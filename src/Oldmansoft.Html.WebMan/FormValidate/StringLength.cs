using Oldmansoft.Html.Util;

namespace Oldmansoft.Html.WebMan.FormValidate
{
    class StringLength : Validator
    {
        public int? Min { get; set; }

        public int? Max { get; set; }

        protected override void Set(JsonObject json)
        {
            if (Min.HasValue) json.Set("min", Min.Value);
            if (Max.HasValue) json.Set("max", Max.Value);
        }
    }
}
