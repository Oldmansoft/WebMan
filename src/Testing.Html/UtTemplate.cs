using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Oldmansoft.Html.Util;
using Oldmansoft.Html;

namespace Testing.Html
{
    [TestClass]
    public class UtTemplate
    {
        [TestMethod]
        public void TestFormat()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("0", "me");
            dictionary.Add("title", "world");
            var template = new HtmlTemplate("hello,{title}.call,{0}.");
            var outer = new Outer();
            template.Format(outer, dictionary);
            Assert.AreEqual("hello,world.call,me.", outer.ToString());
        }

        [TestMethod]
        public void TestLessParameterFormat()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("title", "world");
            var template = new HtmlTemplate("hello,{title}.call,{0}.");
            var outer = new Outer();
            template.Format(outer, dictionary);
            Assert.AreEqual("hello,world.call,.", outer.ToString());
        }

        [TestMethod]
        public void TestMoreParameterFormat()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("0", "me");
            dictionary.Add("title", "world");
            var template = new HtmlTemplate("hello,{title}.");
            var outer = new Outer();
            template.Format(outer, dictionary);
            Assert.AreEqual("hello,world.", outer.ToString());
        }

        class Outer : IHtmlOutput
        {
            private System.Text.StringBuilder Core = new System.Text.StringBuilder();

            public void Append(HtmlTag tag)
            {
                throw new NotImplementedException();
            }

            public void Append(HtmlAttribute attribute)
            {
                throw new NotImplementedException();
            }

            public void Append(string value)
            {
                Core.Append(value);
            }

            public void Append(HtmlChar c)
            {
                throw new NotImplementedException();
            }
            
            public override string ToString()
            {
                return Core.ToString();
            }
        }
    }
}
