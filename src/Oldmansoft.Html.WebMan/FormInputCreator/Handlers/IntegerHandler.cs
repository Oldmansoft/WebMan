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
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(byte)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(short)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(uint)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(ulong)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(int)))
            {
                result = new Inputs.Integer();
            }
            else if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(long)))
            {
                result = new Inputs.Integer();
            }
            if (result == null) return false;

            input.SetInputProperty(result);
            result.Init(input.ModelItem, input.Value, null);
            return true;
        }
    }
}
