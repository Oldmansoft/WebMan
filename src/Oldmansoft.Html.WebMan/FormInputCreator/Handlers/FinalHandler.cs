using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class FinalHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            result = new Inputs.Text();
            input.SetInputProperty(result);
            result.Init(input.PropertyContent, input.Name, input.Value, null);
            return true;
        }
    }
}
