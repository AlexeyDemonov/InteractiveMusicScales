using System;
using System.Threading;
using System.Collections.Generic;
using InteractiveMusicScales.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class LocalizationManager_XmlLoaderSaver_Tests
    {
        [TestMethod]
        public void Write_Load_Compare_NoDefaultCulture_Equal_NoException()
        {
            RunTestingParametrized("en-US", null);
        }

        [TestMethod]
        public void Write_Load_Compare_DefaultCultureDiffers_Equal_NoException()
        {
            RunTestingParametrized("ru-RU", "en-US");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Write_Load_Compare_DefaultCultureEquals_ReturnsNull()
        {
            RunTestingParametrized("en-US", "en-US");
        }

        void RunTestingParametrized(string cultureToRunIn, string defaultCultureToSet)
        {
            var localizationManager = new LocalizationManager(defaultCultureToSet);
            var xmlLoaderSaver = new XmlLoaderSaver(catchAndLogExceptions: false);

            localizationManager.Request_Load += xmlLoaderSaver.Handle_LoadRequest;
            //localizationManager.Request_Save += xmlLoaderSaver.Handle_SaveRequest; //Testing that this event is indeed unavailable at localizationManager

            //Pack and save
            var originalDictionary = new Dictionary<string, string>()
            {
                { "Interactive Music Scales" , "00" },
                { "Clear UI [Esc]" , "01" },
                { "Save current scale [Ctrl+S]" , "02" },
                { "Delete selected scale [Delete]" , "03" },

                { "Cannot save the new scale while other scale is in display" , "04" },
                { "Cannot save empty scale" , "05" },
                { "Scale successfully saved" , "06" },
                { "To delete the scale one must first select it" , "07" },
                { "Cannot delete basic scale" , "08" },
                { "Delete this scale? Are you sure?" , "09" },
                { "Scale deleted" , "10" },
            };

            var packedEntries = new List<LocalizationXmlEntry>();
            foreach (var pair in originalDictionary)
            {
                packedEntries.Add(new LocalizationXmlEntry() { Key = pair.Key, Value = pair.Value });
            }

            var container = new LocalizationXmlRepack() { Entries = packedEntries.ToArray() };

            xmlLoaderSaver.Handle_SaveRequest($"Localization\\{cultureToRunIn}.xml", container);



            //Load and unpack
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureToRunIn);
            var loadedDictionary = localizationManager.Handle_LoadLocalizationRequest();


            //Compare
            foreach (var pair in originalDictionary)
            {
                Assert.AreEqual(originalDictionary[pair.Key], loadedDictionary[pair.Key]);
            }
        }
    }
}
