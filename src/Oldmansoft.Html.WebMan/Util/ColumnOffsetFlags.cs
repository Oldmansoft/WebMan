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
