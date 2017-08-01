using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class EnumHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            var type = input.ModelItem.Property.PropertyType;
            if (type.IsEnum)
            {
                result = new Inputs.RadioList();
                input.SetInputProperty(result);
                result.Init(input.ModelItem.Name, input.ModelItem.Property.PropertyType, input.Value, Util.EnumProvider.Instance.GetDataItems(type));
                return true;
            }
            else if (Util.EnumProvider.IsNullableEnum(type))
            {
                result = new Inputs.RadioList();
                input.SetInputProperty(result);
                result.Init(input.ModelItem.Name, input.ModelItem.Property.PropertyType, input.Value, Util.EnumProvider.Instance.GetDataItems(type.GenericTypeArguments[0]));
                return true;
            }
            return false;
        }
    }
}
