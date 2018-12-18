using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class ListHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            var content = input.PropertyContent;
            var type = content.Property.PropertyType;
            if (type.IsGenericType && type.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                var itemType = type.GetGenericArguments()[0];
                if (itemType.IsEnum)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(content, input.Name, input.Value, Util.EnumProvider.Instance.GetDataItems(itemType, input.Source.GetCanNull(input.Name)));
                    return true;
                }
                else if (itemType == typeof(string))
                {
                    result = new Inputs.MultiSelect();
                    input.SetInputProperty(result);
                    result.Init(content, input.Name, input.Value, input.Source.Get(input.Name));
                    return true;
                }
                else if (itemType == typeof(HttpPostedFileBase) || itemType == typeof(HttpPostedFileWrapper))
                {
                    result = new Inputs.MultiFile();
                    input.SetInputProperty(result);
                    result.Init(content, input.Name, input.Value, null);
                    return true;
                }
                else if (!itemType.IsClass)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(content, input.Name, input.Value, input.Source.Get(input.Name));
                    return true;
                }
            }
            return false;
        }
    }
}
