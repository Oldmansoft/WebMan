﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class EnumHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            var model = input.ModelItem;
            var type = model.Property.PropertyType;
            if (type.IsEnum)
            {
                result = new Inputs.RadioList();
                input.SetInputProperty(result);
                result.Init(model, input.Value, input.Source.Contains(model.Name) ? input.Source.Get(model.Name) : Util.EnumProvider.Instance.GetDataItems(type));
                return true;
            }
            else if (Util.EnumProvider.IsNullableEnum(type))
            {
                result = new Inputs.RadioList();
                input.SetInputProperty(result);
                result.Init(model, input.Value, input.Source.Contains(model.Name) ? input.Source.Get(model.Name) : Util.EnumProvider.Instance.GetDataItems(type.GenericTypeArguments[0]));
                return true;
            }
            return false;
        }
    }
}
