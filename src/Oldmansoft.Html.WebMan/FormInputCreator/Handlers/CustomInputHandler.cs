using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class CustomInputHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref IFormInput result)
        {
            if (input.ModelItem.CustomInput == null) return false;

            input.ModelItem.CustomInput.Init(input.ModelItem.Name, input.Value, input.Source.Get(input.ModelItem.Name), null);
            result = input.ModelItem.CustomInput;
            return true;
        }
    }
}
