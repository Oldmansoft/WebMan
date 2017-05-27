using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Util
{
    class ColumnOffsetFlags : EnumFlags<ColumnOffset>
    {
        protected override bool In(ColumnOffset source, ColumnOffset target)
        {
            return (source & target) == source;
        }
    }
}
