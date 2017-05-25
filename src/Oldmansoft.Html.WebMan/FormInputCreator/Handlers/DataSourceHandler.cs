using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class DataSourceHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (input.Source.Contains(input.ModelItem.Name))
            {
                result = new Inputs.Select();
                result.Init(input.ModelItem.Name, input.Value, input.Source.Get(input.ModelItem.Name), input.Script, input.FormValidator);
                return true;
            }
            return false;
        }
    }
}
