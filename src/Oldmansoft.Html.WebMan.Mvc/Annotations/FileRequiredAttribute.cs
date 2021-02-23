using System.ComponentModel.DataAnnotations;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 指定文件域不能为空
    /// 注意，此验证只在客户端有效，服务端不验证
    /// </summary>
    public class FileRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
