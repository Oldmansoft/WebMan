using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Document
{
    /// <summary>
    /// 引用资源
    /// </summary>
    class Resource : ILinkResource, IScriptResource, ILinkScriptResource, IEnabledLinkResource, IEnabledScriptResource, IEnabledLinkScriptResource
    {
        public Element.Link Link { get; set; }

        public Element.ScriptResource Script { get; set; }

        public bool Enabled { get; set; }
    }
}
