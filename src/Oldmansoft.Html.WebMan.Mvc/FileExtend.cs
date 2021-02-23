using Oldmansoft.Html.WebMan.FormInputCreator;
using Oldmansoft.Html.WebMan.Input;
using System.Web;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 文件支持扩展
    /// </summary>
    public static class FileExtend
    {
        /// <summary>
        /// 注册
        /// </summary>
        public static void Register()
        {
            DataTable.Register(new DataTables.ValueDealer.FileLocationDisplay());
            GlobalOption.Register(new FormInputCreator.Handlers.FileHandler());
            GlobalOption.Register(typeof(HttpPostedFileBase), DealFile);
            GlobalOption.Register(typeof(HttpPostedFileWrapper), DealFile);
        }

        private static IFormInput DealFile(HandlerParameter input, ModelPropertyContent content)
        {
            var result = new FormInputCreator.Inputs.MultiFile();
            input.SetInputProperty(result);
            result.Init(content, input.Name, input.Value, null);
            return result;
        }
    }
}
