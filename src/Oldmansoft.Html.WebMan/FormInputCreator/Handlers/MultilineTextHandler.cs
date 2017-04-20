using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class MultilineTextHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (input.ModelItem.DataType == System.ComponentModel.DataAnnotations.DataType.MultilineText)
            {
                result = new Inputs.Textarea();
                result.Init(input.ModelItem.Name, input.Value, null, null);
                return true;
            }
            return false;
        }
    }
}
