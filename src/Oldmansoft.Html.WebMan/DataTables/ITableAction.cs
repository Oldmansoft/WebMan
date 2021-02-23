namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 表格动作
    /// </summary>
    public interface ITableAction
    {
        /// <summary>
        /// 先确认
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        ITableAction Confirm(string content);

        /// <summary>
        /// 提供参数
        /// 客户端选择的复选框值将用变量名为 SelectedId 的数组进行传递。
        /// </summary>
        /// <returns></returns>
        ITableAction SupportParameter();

        /// <summary>
        /// 需要选择
        /// 客户端不选择复选框时，会提示用户。
        /// </summary>
        /// <returns></returns>
        ITableAction NeedSelected();
    }
}
