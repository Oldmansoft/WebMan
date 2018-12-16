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
            if (input.PropertyContent.CustomInput == null) return false;

            var customer = Activator.CreateInstance(input.PropertyContent.CustomInput.InputType) as ICustomInput;
            customer.Set(input.PropertyContent.CustomInput.Parameter);

            result = customer;
            input.SetInputProperty(result);
            var options = Util.EnumProvider.Instance.GetDataItems(input.PropertyContent.Property.PropertyType, input.Source.GetCanNull(input.PropertyContent.Name));
            result.Init(input.PropertyContent, input.Value, options);
            return true;
        }
    }
}
