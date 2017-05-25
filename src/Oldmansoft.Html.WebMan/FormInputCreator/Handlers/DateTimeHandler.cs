using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class DateTimeHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (input.ModelItem.DataType == System.ComponentModel.DataAnnotations.DataType.DateTime)
            {
                result = new Inputs.DateTimeInput();
                result.Init(input.ModelItem.Name, input.Value, null, input.Script, input.FormValidator);
                return true;
            }
            return false;
        }
    }
}
