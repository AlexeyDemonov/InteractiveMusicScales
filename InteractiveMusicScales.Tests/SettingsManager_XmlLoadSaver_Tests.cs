using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class SettingsManager_XmlLoadSaver_Tests
    {
        [TestMethod]
        public void Write_Load_Compare_Equal_NoException()
        {
            var settingsManager = new SettingsManager();
            var xmlLoaderSaver = new XmlLoaderSaver(catchAndLogExceptions: false);

            settingsManager.Request_Load += xmlLoaderSaver.Handle_LoadRequest;
            settingsManager.Request_Save += xmlLoaderSaver.Handle_SaveRequest;

            //Pack and save
            Note[] originalNotes = new Note[]
            {
                new Note(Sound.A),
                new Note(Sound.B),
                new Note(Sound.C)
            };

            var originalSettings = new SettingsRequestEventArgs(Semitone.Sharp, Semitone.Hidden, Semitone.Flat, originalNotes, 5);

            settingsManager.Handle_SaveSettingsRequest(originalSettings);

            //Load and unpack
            var loadedSettings = settingsManager.Handle_LoadSettingsRequest();

            //Compare
            Assert.AreEqual(originalSettings.PianorollSemitone, loadedSettings.PianorollSemitone);
            Assert.AreEqual(originalSettings.FretboardSemitone, loadedSettings.FretboardSemitone);
            Assert.AreEqual(originalSettings.CircleSemitone, loadedSettings.CircleSemitone);
            Assert.AreEqual(originalSettings.LastVisibleString, loadedSettings.LastVisibleString);

            for (int i = 0; i < originalSettings.FretboardStrings.Length; i++)
            {
                Assert.AreEqual(originalSettings.FretboardStrings[i], loadedSettings.FretboardStrings[i]);
            }
        }
    }
}