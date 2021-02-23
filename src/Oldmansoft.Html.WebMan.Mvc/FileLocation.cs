using System.Web;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 文件定位
    /// </summary>
    public class FileLocation : HttpPostedFileBase
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        private string Name;

        /// <summary>
        /// 文件类型
        /// </summary>
        private string Type;

        /// <summary>
        /// 文件地址
        /// </summary>
        public string Location { get; private set; }

        private FileLocation() { }

        /// <summary>
        /// 创建文件定位
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="location">位置</param>
        /// <returns></returns>
        public static HttpPostedFileBase Create(string fileName, string contentType, string location)
        {
            return new FileLocation()
            {
                Name = fileName,
                Type = contentType,
                Location = location
            };
        }

        /// <summary>
        /// The MIME content type of the file.
        /// </summary>
        public override string ContentType
        {
            get
            {
                return Type;
            }
        }

        /// <summary>
        /// The name of the file on the client, which includes the directory path.
        /// </summary>
        public override string FileName
        {
            get
            {
                return Name;
            }
        }
    }
}
