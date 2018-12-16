﻿using System;
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
            if (input.Source.Contains(input.PropertyContent.Name))
            {
                result = new Inputs.Select();
                input.SetInputProperty(result);
                result.Init(input.PropertyContent, input.Value, input.Source.Get(input.PropertyContent.Name));
                return true;
            }
            return false;
        }
    }
}
