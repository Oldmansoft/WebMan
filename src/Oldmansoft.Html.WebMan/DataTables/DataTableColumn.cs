using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    class DataTableColumn
    {
        public string Name { get; private set; }

        public System.Reflection.PropertyInfo Property { get; private set; }

        public string Text { get; private set; }
        
        public DataTableColumn(string name, System.Reflection.PropertyInfo property, string text)
        {
            Name = name;
            Property = property;
            Text = text;
        }

        public bool Visible(string primaryKey)
        {
            return Name != primaryKey;
        }
    }
}
