using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class Html : Textarea
    {
        public Html(string name, string value)
            : base(name, value)
        {
        }

        public override void SetInputMode()
        {
            base.SetInputMode();
            AddClass("html-edit");
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
            Append(new HtmlRaw(Value));
        }
    }
}
