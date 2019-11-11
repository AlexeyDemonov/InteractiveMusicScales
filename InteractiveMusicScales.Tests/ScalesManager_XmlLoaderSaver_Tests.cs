using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class ScalesManager_XmlLoaderSaver_TeststTest1
    {
        [TestInitialize]
        public void DeleteFileBeforeTest()
        {
            if (File.Exists("AdditionalScales.xml"))
                File.Delete("AdditionalScales.xml");
        }

        [TestMethod]
        public void Write_Load_Compare_Equal_NoException()
        {
            var scalesManager = new ScalesManager();
            var xmlLoaderSaver = new XmlLoaderSaver(catchAndLogExceptions: false);

            scalesManager.Request_Load += xmlLoaderSaver.Handle_LoadRequest;
            scalesManager.Request_Save += xmlLoaderSaver.Handle_SaveRequest;

            //Pack and save
            Scale[] originalScales = new Scale[]
            {
                new Scale("C major", keynoteSound:Sound.C, scaleSound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B),
                new Scale("G major", keynoteSound:Sound.G, scaleSound:Sound.C | Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("D major", keynoteSound:Sound.D, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
            };

            var task = scalesManager.Handle_SaveAdditionalScalesRequestAwaitable(originalScales);

            //Wait
            task.Wait();

            //Load and unpack
            var loadedScales = scalesManager.Handle_LoadAdditionalScalesRequest();

            //Compare
            for (int i = 0; i < originalScales.Length; i++)
            {
                Scale originalScale = originalScales[i];
                Scale loadedScale = loadedScales[i];

                Assert.AreEqual(originalScale.Name, loadedScale.Name);
                Assert.AreEqual(originalScale.Sound, loadedScale.Sound);
                Assert.AreEqual(originalScale.KeynoteSound, loadedScale.KeynoteSound);
            }
        }
    }
}