using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 文件属性
    /// </summary>
    public class FileOptionAttribute : ValidationAttribute
    {
        /// <summary>
        /// 数量
        /// </summary>
        public uint Count { get; set; }
        
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
            Count = 0;
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
            var file = value as HttpPostedFileBase;
            if (file == null || file.FileName == null || file.ContentLength == 0) return true;

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

            result = false;
            foreach (var extendsion in Extensions)
            {
                if (file.FileName.Length > extendsion.Length && file.FileName.Substring(file.FileName.Length - extendsion.Length - 1, 4).ToLower() == string.Format(".{0}", extendsion).ToLower())
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
