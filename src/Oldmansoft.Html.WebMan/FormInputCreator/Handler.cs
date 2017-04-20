using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    abstract class Handler : Util.ChainOfResponsibilityHandler<HandlerParameter, Input.IFormInput>
    {
    }
}
