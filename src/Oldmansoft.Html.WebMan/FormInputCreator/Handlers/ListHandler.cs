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
            var model = input.ModelItem;
            var type = model.Property.PropertyType;
            if (type.IsGenericType && type.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                var itemType = type.GetGenericArguments()[0];
                if (itemType.IsEnum)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(model.Name, type, input.Value, Util.EnumProvider.Instance.GetDataItems(itemType, input.Source.GetCanNull(model.Name)));
                    return true;
                }
                else if (itemType == typeof(string))
                {
                    result = new Inputs.MultiSelect();
                    input.SetInputProperty(result);
                    result.Init(model.Name, type, input.Value, input.Source.Get(model.Name));
                    return true;
                }
                else if (itemType == typeof(HttpPostedFileBase) || itemType == typeof(HttpPostedFileWrapper))
                {
                    var multiFile = new Inputs.MultiFile();
                    if (model.FileOption != null)
                    {
                        multiFile.FileOption = model.FileOption;
                    }
                    result = multiFile;
                    input.SetInputProperty(result);
                    result.Init(model.Name, type, input.Value, null);
                    return true;
                }
                else if (!itemType.IsClass)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(model.Name, type, input.Value, input.Source.Get(model.Name));
                    return true;
                }
            }
            return false;
        }
    }
}
