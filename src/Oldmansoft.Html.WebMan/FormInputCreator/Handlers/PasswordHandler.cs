﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class PasswordHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (input.ModelItem.DataType == System.ComponentModel.DataAnnotations.DataType.Password)
            {
                result = new Inputs.Password();
                input.SetInputProperty(result);
                result.Init(input.ModelItem, input.Value, null);
                return true;
            }
            return false;
        }
    }
}
