using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 定义 HttpPostedFileBase 内容
    /// </summary>
    public class HttpPostedFileCustom : System.Web.HttpPostedFileBase
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        private string Name { get; set; }
        
        /// <summary>
        /// 文件类型
        /// </summary>
        private string Type { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// 创建定义 HttpPostedFileBase 内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="location"></param>
        public HttpPostedFileCustom(string fileName, string contentType, string location)
        {
            Name = fileName;
            Type = contentType;
            Location = location;
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
