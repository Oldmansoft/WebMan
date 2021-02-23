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
