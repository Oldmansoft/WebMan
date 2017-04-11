using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class PasswordHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref FormInput result)
        {
            if (input.ModelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Password)
            {
                result = new Inputs.Password(input.ModelItem.Name, GetStringValue(input));
                return true;
            }
            return false;
        }
    }
}
