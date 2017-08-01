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

            var customer = Activator.CreateInstance(input.ModelItem.CustomInput.InputType) as ICustomInput;
            customer.Set(input.ModelItem.CustomInput.Parameter);

            result = customer;
            IList<ListDataItem> options;
            if (!TryGetEnumList(input.ModelItem.Property.PropertyType, out options))
            {
                options = input.Source.Get(input.ModelItem.Name);
            }
            input.SetInputProperty(result);
            result.Init(input.ModelItem.Name, input.ModelItem.Property.PropertyType, input.Value, options);
            return true;
        }

        private bool TryGetEnumList(Type type, out IList<ListDataItem> options)
        {
            options = null;
            if (type.IsGenericType && type.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)))
            {
                var itemType = type.GetGenericArguments()[0];
                if (itemType.IsEnum)
                {
                    options = Util.EnumProvider.Instance.GetDataItems(itemType);
                    return true;
                }
            }
            return false;
        }
    }
}
