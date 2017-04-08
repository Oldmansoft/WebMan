using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class NumberHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref FormInput result)
        {
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(byte)))
            {
                result = new Inputs.Number(input.ModelItem.Name, (byte?)input.Value);
                return true;
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(short)))
            {
                result = new Inputs.Number(input.ModelItem.Name, (short?)input.Value);
                return true;
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(uint)))
            {
                result = new Inputs.Number(input.ModelItem.Name, (uint?)input.Value);
                return true;
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(ulong)))
            {
                result = new Inputs.Number(input.ModelItem.Name, (ulong?)input.Value);
                return true;
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(int)))
            {
                result = new Inputs.Number(input.ModelItem.Name, (int?)input.Value);
                return true;
            }
            if (ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(long)))
            {
                result = new Inputs.Number(input.ModelItem.Name, (long?)input.Value);
                return true;
            }
            return false;
        }
    }
}
