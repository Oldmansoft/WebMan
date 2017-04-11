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
        protected override bool Request(HandlerParameter input, ref FormInput result)
        {
            var model = input.ModelItem;
            if (model.Property.PropertyType.IsGenericType && model.Property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                var itemType = model.Property.PropertyType.GetGenericArguments()[0];
                if (itemType.IsEnum)
                {
                    result = new Inputs.CheckBoxList(
                        model.Name,
                        GetStringValues(input),
                        input.Source.Contains(model.Name) ? input.Source.Get(model.Name) : Util.EnumProvider.Instance.GetDataItems(itemType)
                    );
                    return true;
                }
                else if (itemType == typeof(string))
                {
                    result = new Inputs.MultiSelect(
                        model.Name,
                        GetStringValues(input),
                        input.Source.Get(model.Name)
                    );
                    return true;
                }
                else if (!itemType.IsClass)
                {
                    result = new Inputs.CheckBoxList(
                        model.Name,
                        GetStringValues(input),
                        input.Source.Get(model.Name)
                    );
                    return true;
                }
                else if (itemType == typeof(HttpPostedFileBase))
                {

                }
                else if (itemType == typeof(HttpPostedFileWrapper))
                {

                }
            }
            return false;
        }
    }
}
