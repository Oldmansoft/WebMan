namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class FileHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            var type = typeof(Microsoft.AspNetCore.Http.IFormFile);
            if (input.PropertyContent.Property.PropertyType == type ||
                input.PropertyContent.Property.PropertyType.IsSubclassOf(type))
            {
                result = new Inputs.File();
                input.SetInputProperty(result);
                result.Init(input.PropertyContent, input.Name, input.Value, null);
                return true;
            }
            return false;
        }
    }
}
