using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class DataTableAction
    {
        public string Text { get; set; }

        public string Location { get; set; }

        public LinkBehave Behave { get; set; }

        public DataTableAction(string text, string location, LinkBehave behave)
        {
            Text = text;
            Location = location;
            Behave = behave;
        }
    }
}
