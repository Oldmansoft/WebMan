using Oldmansoft.Html.WebMan.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Util
{
    class ContentTypeFlags : EnumFlags<ContentType>
    {
        protected override bool Ignore(ContentType item)
        {
            return item == ContentType.None;
        }

        protected override bool In(ContentType source, ContentType target)
        {
            return (source & target) == source;
        }
    }
}
