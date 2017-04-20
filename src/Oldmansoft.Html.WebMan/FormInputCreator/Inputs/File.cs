using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.Html.WebMan.Input;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class File : FormInput
    {
        private string Name { get; set; }

        /// <summary>
        /// 允许的扩展名
        /// </summary>
        public string[] AllowedExtensions { get; set; }
        
        public override void Init(string name, object value, IList<ListDataItem> options, ScriptRegister scripts)
        {
            Name = name;
            AllowedExtensions = new string[] { "jpeg", "jpg", "gif", "png" };
        }

        public override void SetInputMode(bool disabled, bool readony, string hint)
        {
            Attribute(HtmlAttribute.Type, "file");
            Attribute(HtmlAttribute.Name, Name);
            SetAttribute(this, disabled, readony, hint);
            AddClass("form-control");
        }

        public override void SetViewMode()
        {
            Tag = HtmlTag.Div;
            AddClass("control-value");
        }
    }
}
