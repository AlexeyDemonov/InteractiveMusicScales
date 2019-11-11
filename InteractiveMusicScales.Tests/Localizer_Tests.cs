using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class Localizer_Tests
    {
        [TestMethod]
        public void Ctor_Null_NoException()
        {
            var test = new Localizer(null);
        }

        [TestMethod]
        public void Ctor_Dictionary_NoException()
        {
            var test = new Localizer(new Dictionary<string, string>());
        }

        [TestMethod]
        public void Indexer_NullDictionary_ReturnsKey()
        {
            var test = new Localizer(null);
            string key = "image";
            Assert.AreEqual(key, test[key]);
        }

        [TestMethod]
        public void Indexer_EmptyDictionary_ReturnsKey()
        {
            var test = new Localizer(new Dictionary<string, string>());
            string key = "image";
            Assert.AreEqual(key, test[key]);
        }

        [TestMethod]
        public void Indexer_Dictionary_IncorrectKey_ReturnsKey()
        {
            var test = new Localizer(new Dictionary<string, string>() { { "key", "value" } });
            string key = "image";
            Assert.AreEqual(key, test[key]);
        }

        [TestMethod]
        public void Indexer_Dictionary_CorrectKey_ReturnsValue()
        {
            var test = new Localizer(new Dictionary<string, string>() { { "key", "value" } });
            string key = "key";
            Assert.AreEqual("value", test[key]);
        }
    }
}