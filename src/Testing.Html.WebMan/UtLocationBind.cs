using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oldmansoft.Html.WebMan;

namespace Testing.Html.WebMan
{
    [TestClass]
    public class UtLocationBind
    {
        [TestMethod]
        public void TestBindString()
        {
            var path = "index";
            var key = "key";
            var value = "12";
            var location = new LocationBind<string>(Location.Create(path));
            location.Set(key, value);
            Assert.AreEqual(string.Format("{0}?{1}={2}", path, key, value), location.Path);
        }

        [TestMethod]
        public void TestBindList()
        {
            var path = "index";
            var key = "key";
            var value = "12";
            var list = new List<string>();
            list.Add(value);
            list.Add(value);
            var location = new LocationBind<List<string>>(Location.Create(path));
            location.Set(key, list);
            Assert.AreEqual(string.Format("{0}?{1}={2}&{1}={2}", path, key, value), location.Path);
        }

        [TestMethod]
        public void TestBindMore()
        {
            var path = "index";
            var key = "key";
            var value = "12";
            var otherKey = "value";
            var otherValue = "hello";
            var location = new LocationBind<string>(Location.Create(path));
            location.Set(key, value);
            location.Set(otherKey, otherValue);
            Assert.AreEqual(string.Format("{0}?{1}={2}&{3}={4}", path, key, value, otherKey, otherValue), location.Path);
        }
    }
}
