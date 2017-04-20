using Oldmansoft.Html.WebMan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class NumberHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(byte)))
            {
                result = new Inputs.Number();
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(short)))
            {
                result = new Inputs.Number();
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(uint)))
            {
                result = new Inputs.Number();
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(ulong)))
            {
                result = new Inputs.Number();
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(int)))
            {
                result = new Inputs.Number();
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(long)))
            {
                result = new Inputs.Number();
            }
            if (result == null) return false;

            result.Init(input.ModelItem.Name, input.Value, null, null);
            return true;
        }
    }
}
