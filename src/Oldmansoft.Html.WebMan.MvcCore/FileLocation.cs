using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 文件定位
    /// </summary>
    public class FileLocation : IFormFile
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; private set; }

        private FileLocation() { }

        /// <summary>
        /// 创建文件定位
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="location">位置</param>
        /// <returns></returns>
        public static IFormFile Create(string fileName, string contentType, string location)
        {
            return new FileLocation()
            {
                FileName = fileName,
                ContentType = contentType,
                Location = location
            };
        }

        string IFormFile.ContentDisposition => throw new System.NotImplementedException();

        IHeaderDictionary IFormFile.Headers => throw new System.NotImplementedException();

        long IFormFile.Length => throw new System.NotImplementedException();

        string IFormFile.Name => throw new System.NotImplementedException();

        void IFormFile.CopyTo(Stream target)
        {
            throw new System.NotImplementedException();
        }

        Task IFormFile.CopyToAsync(Stream target, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        Stream IFormFile.OpenReadStream()
        {
            throw new System.NotImplementedException();
        }
    }
}
