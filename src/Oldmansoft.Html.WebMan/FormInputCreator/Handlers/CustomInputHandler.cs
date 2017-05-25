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
            if (input.ModelItem.CustomInputType == null) return false;

            result = Activator.CreateInstance(input.ModelItem.CustomInputType) as IFormInput;
            result.Init(input.ModelItem.Name, input.Value, input.Source.Get(input.ModelItem.Name), input.Script, input.FormValidator);
            return true;
        }
    }
}
