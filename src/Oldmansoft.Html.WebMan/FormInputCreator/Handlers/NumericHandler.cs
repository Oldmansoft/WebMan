using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class NumericHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(float)))
            {
                result = new Inputs.Numeric();
            }
            else if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(double)))
            {
                result = new Inputs.Numeric();
            }
            else if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(decimal)))
            {
                result = new Inputs.Numeric();
            }
            if (result == null) return false;

            input.SetInputProperty(result);
            result.Init(input.PropertyContent, input.Name, input.Value, null);
            return true;
        }
    }
}
