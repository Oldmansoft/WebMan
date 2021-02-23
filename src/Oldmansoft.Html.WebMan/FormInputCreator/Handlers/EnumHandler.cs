namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class EnumHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            var content = input.PropertyContent;
            var type = content.Property.PropertyType;
            if (type.IsEnum)
            {
                result = new Inputs.RadioList();
                input.SetInputProperty(result);
                result.Init(content, input.Name, input.Value, input.Source.Contains(input.Name) ? input.Source.Get(input.Name) : Util.EnumProvider.Instance.GetDataItems(type));
                return true;
            }
            else if (Util.EnumProvider.IsNullableEnum(type))
            {
                result = new Inputs.RadioList();
                input.SetInputProperty(result);
                result.Init(content, input.Name, input.Value, input.Source.Contains(input.Name) ? input.Source.Get(input.Name) : Util.EnumProvider.Instance.GetDataItems(type.GenericTypeArguments[0]));
                return true;
            }
            return false;
        }
    }
}
