using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oldmansoft.Html;

namespace Testing.Html
{
    [TestClass]
    public class UtExtend
    {
        [TestMethod]
        public void TestToKeyValues()
        {
            var source = new { name = "hello" };
            var result = source.GetKeyValues();
            Assert.AreEqual("hello", result["name"]);
        }
    }
}
