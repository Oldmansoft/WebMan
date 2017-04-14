using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class File : FormInput
    {
        /// <summary>
        /// 允许的扩展名
        /// </summary>
        public string[] AllowedExtensions { get; set; }

        /// <summary>
        /// 创建文件上传
        /// </summary>
        /// <param name="name"></param>
        public File(string name)
            : base(name)
        {
            AllowedExtensions = new string[] { "jpeg", "jpg", "gif", "png" };
        }

        public override void SetInputMode()
        {
            Attribute(HtmlAttribute.Type, "file");
            Attribute(HtmlAttribute.Name, Name);
            SetAttribute(this);
            AddClass("form-control");
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
        }
    }
}
