using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class DateHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref FormInput result)
        {
            if (input.ModelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Date)
            {
                result = new Inputs.Date(input.ModelItem.Name, (DateTime?)input.Value);
                return true;
            }
            return false;
        }
    }
}
