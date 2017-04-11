using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class BoolHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref FormInput result)
        {
            if (Util.ModelItemInfo.IsType(input.ModelItem.Property.PropertyType, typeof(bool)))
            {
                result = new Inputs.Bool(input.ModelItem.Name, (bool?)input.Value);
                return true;
            }
            return false;
        }
    }
}
