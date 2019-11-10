using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InteractiveMusicScales.Managers;
using System.Collections.Generic;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class XmlLoadSaver_LocalizationXmlRepack_Tests
    {
        [TestMethod]
        public void Write_Load_Compare_Equal_NoException()
        {

            //Pack and save
            var originalDictionary = new Dictionary<string,string>()
            {
                {"apple","яблоко"},
                {"orange","апельсинка"},
                {"pizza","пицца"}
            };

            var packedEntries = new List<LocalizationXmlEntry>();
            foreach (var pair in originalDictionary)
            {
                packedEntries.Add(new LocalizationXmlEntry() { Key=pair.Key, Value=pair.Value });
            }

            var container = new LocalizationXmlRepack() { Entries = packedEntries.ToArray() };

            var xmlLoadSaver = new XmlLoadSaver(catchAndLogExceptions: false);

            xmlLoadSaver.Handle_SaveRequest("Localization\\Test\\TestLocalization.xml", container);



            //Load and unpack
            container = (LocalizationXmlRepack)xmlLoadSaver.Handle_LoadRequest("Localization\\Test\\TestLocalization.xml", typeof(LocalizationXmlRepack));

            var unpackedEntries = container.Entries;

            var loadedDictionary = new Dictionary<string, string>();

            foreach (var entry in unpackedEntries)
            {
                loadedDictionary[entry.Key] = entry.Value;
            }



            //Compare
            foreach (var pair in originalDictionary)
            {
                Assert.AreEqual(originalDictionary[pair.Key], loadedDictionary[pair.Key]);
            }
        }
    }
}
