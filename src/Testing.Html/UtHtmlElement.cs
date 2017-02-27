using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oldmansoft.Html;

namespace Testing.Html
{
    [TestClass]
    public class UtHtmlElement
    {
        [TestMethod]
        public void TestCombine()
        {
            var i = new HtmlElement(HtmlTag.I);
            i.After(new HtmlElement(HtmlTag.Img));
            i.Prepend(new HtmlElement(HtmlTag.B));

            var outer = new HtmlOutput();
            outer.Add((i as IHtmlNode).Parent);
            Assert.AreEqual("<i><b></b></i><img/>", outer.Complete());
        }

        [TestMethod]
        public void TestCombineOther()
        {
            var i = new HtmlElement(HtmlTag.I);
            var img = new HtmlElement(HtmlTag.Img);
            i.After(img);

            var b = new HtmlElement(HtmlTag.B);
            b.After(img);

            var outer = new HtmlOutput();
            outer.Add((i as IHtmlNode).Parent);
            Assert.AreEqual("<i></i>", outer.Complete());
        }
    }
}
