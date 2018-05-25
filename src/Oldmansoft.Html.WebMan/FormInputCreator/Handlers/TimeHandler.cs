using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class TimeHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (input.ModelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Time)
            {
                result = new Inputs.Time();
                input.SetInputProperty(result);
                result.Init(input.ModelItem, input.ModelItem.Property.PropertyType, input.Value, null);
                return true;
            }
            return false;
        }
    }
}
