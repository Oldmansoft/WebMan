using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class FileHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            var type = typeof(System.Web.HttpPostedFileBase);
            if (input.ModelItem.Property.PropertyType == type ||
                input.ModelItem.Property.PropertyType.IsSubclassOf(type))
            {
                result = new Inputs.File();
                result.Init(input.ModelItem.Name, input.Value, null, null);
                return true;
            }
            return false;
        }
    }
}
