using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Util;
using Oldmansoft.Html.WebMan.FormInputCreator.Handlers;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    class InputCreator : ChainOfResponsibility<HandlerParameter, FormInput>
    {
        public static readonly InputCreator Instance = new InputCreator();

        private InputCreator() { }

        protected override ChainOfResponsibilityHandler<HandlerParameter, FormInput> InitChain()
        {
            var result = new StartHandler();
            result.SetNext(new ListHandler())
                .SetNext(new FileHandler())
                .SetNext(new DataSourceHandler())
                .SetNext(new BoolHandler())
                .SetNext(new NumberHandler())
                .SetNext(new DateHandler())
                .SetNext(new TimeHandler())
                .SetNext(new DateTimeHandler())
                .SetNext(new EnumHandler())
                .SetNext(new PasswordHandler())
                .SetNext(new MultilineTextHandler())
                .SetNext(new FinalHandler());
            return result;
        }
    }
}
