using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class IntegerHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(byte)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(short)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(uint)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(ulong)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(int)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelPropertyContent.IsType(input.PropertyContent.Property.PropertyType, typeof(long)))
            {
                result = new Inputs.Integer();
            }
            if (result == null) return false;

            input.SetInputProperty(result);
            result.Init(input.PropertyContent, input.Name, input.Value, null);
            return true;
        }
    }
}
