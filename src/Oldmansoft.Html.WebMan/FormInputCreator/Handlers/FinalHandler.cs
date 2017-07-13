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
            result.Init(input.ModelItem.Name, input.ModelItem.Property.PropertyType, input.Value, null, input.Script, input.FormValidator);
            return true;
        }
    }
}
