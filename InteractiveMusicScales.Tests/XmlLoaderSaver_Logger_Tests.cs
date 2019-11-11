using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class XmlLoaderSaver_Logger_Tests
    {
        static XmlLoaderSaver xmlLoaderSaver;

        [ClassInitialize]
        public static void InitializeFields(TestContext testContext)
        {
            xmlLoaderSaver = new XmlLoaderSaver(catchAndLogExceptions: true);
        }

        [TestInitialize]
        public void DeleteLogBeforeEachTest()
        {
            if(File.Exists("Errors.log"))
                File.Delete("Errors.log");
        }

        [TestMethod]
        public void Write_Read_CorrectData_NoErrors_NoExceptions()
        {
            var originalSample = new XmlableClass() { StringValue="Sample", IntValue=100 };
            xmlLoaderSaver.Handle_SaveRequest("test.xml", originalSample);

            var loadedSample = (XmlableClass)xmlLoaderSaver.Handle_LoadRequest("test.xml", typeof(XmlableClass));

            Assert.IsTrue(!(File.Exists("Errors.log")));
            Assert.IsNotNull(loadedSample);
        }

        [TestMethod]
        public void Write_IncorretData_ErrorsLogged_NoExceptions()
        {
            var incorrectSample = new nonXmlableClass();
            xmlLoaderSaver.Handle_SaveRequest("test.xml", incorrectSample);

            Assert.IsTrue(File.Exists("Errors.log"));
        }

        [TestMethod]
        public void Load_IncorretData_ErrorsLogged_NoExceptions_NullReturned()
        {
            File.WriteAllText("test.xml", "abrakadabra");
            
            var loadedSample = xmlLoaderSaver.Handle_LoadRequest("test.xml", typeof(XmlableClass));

            Assert.IsTrue(File.Exists("Errors.log"));
            Assert.IsNull(loadedSample);
        }
    }
}
