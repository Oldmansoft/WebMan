using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    abstract class Handler : Util.ChainOfResponsibilityHandler<HandlerParameter, FormInput>
    {
        protected string GetStringValue(HandlerParameter input)
        {
            return input.Value == null ? string.Empty : input.Value.ToString();
        }

        protected IList<string> GetStringValues(HandlerParameter input)
        {
            var result = new List<string>();
            if (input.Value == null) return result;
            foreach(var item in input.Value as IEnumerable)
            {
                if (item == null) continue;
                result.Add(item.ToString());
            }
            return result;
        }
    }
}
