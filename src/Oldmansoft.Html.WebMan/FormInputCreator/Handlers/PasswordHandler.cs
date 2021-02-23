namespace Oldmansoft.Html.WebMan.FormInputCreator.Handlers
{
    class PasswordHandler : Handler
    {
        protected override bool Request(HandlerParameter input, ref Input.IFormInput result)
        {
            if (input.PropertyContent.DataType == System.ComponentModel.DataAnnotations.DataType.Password)
            {
                result = new Inputs.Password();
                input.SetInputProperty(result);
                result.Init(input.PropertyContent, input.Name, input.Value, null);
                return true;
            }
            return false;
        }
    }
}
