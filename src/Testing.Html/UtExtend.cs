using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oldmansoft.Html.Util;

namespace Testing.Html
{
    [TestClass]
    public class UtExtend
    {
        [TestMethod]
        public void TestToKeyValues()
        {
            var source = new { name = "hello" };
            var result = source.ToKeyValues();
            Assert.AreEqual("hello", result["name"]);
        }
    }
}
