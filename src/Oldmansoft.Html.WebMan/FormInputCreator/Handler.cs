using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    abstract class Handler : Util.ChainOfResponsibilityHandler<HandlerParameter, FormInput>
    {
        protected string GetStringValue(HandlerParameter input)
        {
            return input.Value == null ? string.Empty : input.Value.ToString();
        }
    }
}
