using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class FinalHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref FormInput result)
        {
            result = new Inputs.Text(input.ModelItem.Name, GetStringValue(input));
            return true;
        }
    }
}
