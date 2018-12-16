﻿using System;
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
            if (input.PropertyContent.DataType == System.ComponentModel.DataAnnotations.DataType.MultilineText)
            {
                result = new Inputs.Textarea();
                input.SetInputProperty(result);
                result.Init(input.PropertyContent, input.Value, null);
                return true;
            }
            return false;
        }
    }
}
