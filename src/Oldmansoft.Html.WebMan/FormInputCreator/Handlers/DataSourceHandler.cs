using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class DataSourceHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref FormInput result)
        {
            if (input.Source.Contains(input.ModelItem.Name))
            {
                result = new Inputs.Select(input.ModelItem.Name, GetStringValue(input), input.Source.Get(input.ModelItem.Name));
                return true;
            }
            return false;
        }
    }
}
