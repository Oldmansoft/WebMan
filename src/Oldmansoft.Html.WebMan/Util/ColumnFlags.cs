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
