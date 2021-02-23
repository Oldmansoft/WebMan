namespace Oldmansoft.Html.WebMan.FormInputCreator
{
    /// <summary>
    /// 表单输入创建处理
    /// </summary>
    public abstract class Handler : Util.ChainOfResponsibilityHandler<HandlerParameter, Input.IFormInput>
    {
    }
}
