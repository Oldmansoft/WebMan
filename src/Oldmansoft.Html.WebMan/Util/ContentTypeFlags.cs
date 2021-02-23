using Oldmansoft.Html.WebMan.Annotations;

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
