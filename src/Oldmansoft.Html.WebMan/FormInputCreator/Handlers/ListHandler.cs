using Oldmansoft.Html.WebMan.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class ListHandler : Handler
    {
        private static readonly Dictionary<Type, Func<HandlerParameter, ModelPropertyContent, IFormInput>> ItemHandlers = new Dictionary<Type, Func<HandlerParameter, ModelPropertyContent, IFormInput>>();

        public static void Register(Type type, Func<HandlerParameter, ModelPropertyContent, IFormInput> func)
        {
            if (ItemHandlers.ContainsKey(type)) return;
            ItemHandlers.Add(type, func);
        }

        static ListHandler()
        {
            Register(typeof(string), DealString);
        }

        private static IFormInput DealString(HandlerParameter input, ModelPropertyContent content)
        {
            var result = new Inputs.MultiSelect();
            input.SetInputProperty(result);
            result.Init(content, input.Name, input.Value, input.Source.Get(input.Name));
            return result;
        }

        protected override bool Request(HandlerParameter input, ref IFormInput result)
        {
            var content = input.PropertyContent;
            var type = content.Property.PropertyType;
            if (type.IsGenericType && type.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                var itemType = type.GetGenericArguments()[0];
                if (itemType.IsEnum)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(content, input.Name, input.Value, Util.EnumProvider.Instance.GetDataItems(itemType, input.Source.GetCanNull(input.Name)));
                    return true;
                }
                else if (ItemHandlers.TryGetValue(itemType, out Func<HandlerParameter, ModelPropertyContent, IFormInput> deal))
                {
                    result = deal(input, content);
                    return true;
                }
                else if (!itemType.IsClass)
                {
                    result = new Inputs.CheckBoxList();
                    input.SetInputProperty(result);
                    result.Init(content, input.Name, input.Value, input.Source.Get(input.Name));
                    return true;
                }
            }
            return false;
        }
    }
}
