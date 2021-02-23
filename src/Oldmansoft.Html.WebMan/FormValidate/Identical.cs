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
