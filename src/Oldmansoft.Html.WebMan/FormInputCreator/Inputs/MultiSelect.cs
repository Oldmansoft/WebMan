using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.FormInputCreator.Inputs
{
    class MultiSelect : CheckBoxList
    {
        public MultiSelect(string name, IEnumerable<string> values, IList<ListDataItem> options)
            : base(name, values, options)
        {
        }

        public override void SetInputMode()
        {
            Tag = HtmlTag.Select;
            Attribute(HtmlAttribute.Name, Name);
            foreach (var option in Options)
            {
                var item = new HtmlElement(HtmlTag.Option);
                Append(item);
                item.Attribute(HtmlAttribute.Value, option.Value);
                if (Values.Contains(option.Value))
                {
                    item.Attribute(HtmlAttribute.Selected, "selected");
                }
                item.Text(option.Text);
            }
            SetAttribute(this);
        }
    }
}
