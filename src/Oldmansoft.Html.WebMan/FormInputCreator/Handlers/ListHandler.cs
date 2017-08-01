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
            if (model.Property.PropertyType.IsGenericType && model.Property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                var itemType = model.Property.PropertyType.GetGenericArguments()[0];
                if (itemType.IsEnum)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(model.Name, input.ModelItem.Property.PropertyType, input.Value, input.Source.Contains(model.Name) ? input.Source.Get(model.Name) : Util.EnumProvider.Instance.GetDataItems(itemType));
                    return true;
                }
                else if (itemType == typeof(string))
                {
                    result = new Inputs.MultiSelect();
                    input.SetInputProperty(result);
                    result.Init(model.Name, input.ModelItem.Property.PropertyType, input.Value, input.Source.Get(model.Name));
                    return true;
                }
                else if (itemType == typeof(HttpPostedFileBase) || itemType == typeof(HttpPostedFileWrapper))
                {
                    var multiFile = new Inputs.MultiFile();
                    if (input.ModelItem.FileOption != null)
                    {
                        multiFile.FileOption = input.ModelItem.FileOption;
                    }
                    result = multiFile;
                    input.SetInputProperty(result);
                    result.Init(model.Name, input.ModelItem.Property.PropertyType, input.Value, null);
                    return true;
                }
                else if (!itemType.IsClass)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(model.Name, input.ModelItem.Property.PropertyType, input.Value, input.Source.Get(model.Name));
                    return true;
                }
            }
            return false;
        }
    }
}
