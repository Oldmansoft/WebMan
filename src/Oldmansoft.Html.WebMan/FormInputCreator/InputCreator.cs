using Oldmansoft.Html.WebMan.FormInputCreator.Handlers;
using Oldmansoft.Html.WebMan.Util;
using System.Collections.Generic;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    class InputCreator : ChainOfResponsibility<HandlerParameter, Input.IFormInput>
    {
        private static readonly List<Handler> WaitedToSetNext = new List<Handler>();

        private static InputCreator _Instance;

        public static InputCreator Instance
        {
            get
            {
                if (_Instance == null) _Instance = new InputCreator();
                return _Instance;
            }
        }

        public static void Register(Handler handler)
        {
            WaitedToSetNext.Add(handler);
        }

        private InputCreator() { }

        protected override ChainOfResponsibilityHandler<HandlerParameter, Input.IFormInput> InitChain()
        {
            var result = new StartHandler();
            var next = result.SetNext(new CustomInputHandler()).SetNext(new ListHandler());

            foreach(var handler in WaitedToSetNext)
            {
                next = next.SetNext(handler);
            }
            next.SetNext(new DataSourceHandler())
            .SetNext(new BoolHandler())
            .SetNext(new IntegerHandler())
            .SetNext(new NumericHandler())
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
