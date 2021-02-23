using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 文件属性
    /// </summary>
    public class FileOptionAttribute : ValidationAttribute
    {
        /// <summary>
        /// 限制内容大小
        /// </summary>
        public uint LimitContentLength { get; set; }
        
        /// <summary>
        /// 允许上传的文件扩展名
        /// </summary>
        public string[] Extensions { get; set; }

        /// <summary>
        /// 提供删除
        /// </summary>
        public bool SupportDelete { get; set; }

        /// <summary>
        /// 接受类型
        /// </summary>
        public ContentType Accept { get; set; }

        /// <summary>
        /// 文件数量
        /// </summary>
        public FileOptionAttribute(params string[] extensions)
        {
            if (extensions != null && extensions.Length > 0)
            {
                Extensions = extensions;
            }
            else
            {
                Extensions = new string[] { "jpeg", "jpg", "gif", "png" };
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            if (value is IEnumerable<IFormFile>)
            {
                foreach (var item in value as IEnumerable<IFormFile>)
                {
                    if (!ValidFile(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            return ValidFile(value as IFormFile);
        }

        private bool ValidFile(IFormFile file)
        {
            if (file == null || file.FileName == null || file.Length == 0) return true;

            if (LimitContentLength > 0 && file.Length > LimitContentLength) return false;

            var result = false;
            if (Accept != ContentType.None)
            {
                foreach (var item in Accept.ToArray())
                {
                    if (item.In(file.ContentType))
                    {
                        result = true;
                    }
                }
                if (!result) return false;
            }

            var fileExtensionName = System.IO.Path.GetExtension(file.FileName).ToLower();
            foreach (var extendsion in Extensions)
            {
                if (fileExtensionName == string.Format(".{0}", extendsion).ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
