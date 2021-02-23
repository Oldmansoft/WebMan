namespace Oldmansoft.Html.WebMan.Input
{
    /// <summary>
    /// 定制输入
    /// </summary>
    public interface ICustomInput : IFormInput
    {
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="parameter">参数</param>
        void Set(object[] parameter);   
    }
}
