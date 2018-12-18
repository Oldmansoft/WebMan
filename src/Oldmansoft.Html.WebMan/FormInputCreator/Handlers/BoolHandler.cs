using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class BoolHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(bool)))
            {
                result = new Inputs.Bool();
                input.SetInputProperty(result);
                result.Init(input.PropertyContent, input.Name, input.Value, null);
                return true;
            }
            return false;
        }
    }
}
