using Oldmansoft.Html.WebMan.Input;
using System;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class CustomInputHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref IFormInput result)
        {
            var customInput = input.PropertyContent.Attributes.Get<Annotations.CustomInputAttribute>();
            if (customInput == null) return false;

            var customer = Activator.CreateInstance(customInput.InputType) as ICustomInput;
            customer.Set(customInput.Parameter);

            result = customer;
            input.SetInputProperty(result);
            var options = Util.EnumProvider.Instance.GetDataItems(input.PropertyContent.Property.PropertyType, input.Source.GetCanNull(input.Name));
            result.Init(input.PropertyContent, input.Name, input.Value, options);
            return true;
        }
    }
}
