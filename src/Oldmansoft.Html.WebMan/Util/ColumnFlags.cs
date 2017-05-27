using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Util
{
    class ColumnFlags : EnumFlags<Column>
    {
        protected override bool In(Column source, Column target)
        {
            return (source & target) == source;
        }
    }
}
