using InteractiveMusicScales.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class XmlLoaderSaver_SettingsXmlRepack_Tests
    {
        [TestMethod]
        public void Write_Load_Compare_Equal_NoException()
        {
            //Pack and save
            Note[] originalNotes = new Note[]
            {
                new Note(Sound.A),
                new Note(Sound.B),
                new Note(Sound.C)
            };

            var originalSettings = new SettingsRequestEventArgs(Semitone.Sharp, Semitone.Hidden, Semitone.Flat, originalNotes, 5);

            var packedNotes = new int[originalNotes.Length];
            for (int i = 0; i < packedNotes.Length; i++)
            {
                packedNotes[i] = (int)originalNotes[i].Sound;
            }

            var container = new SettingsXmlRepack()
            {
                PianorollSemitone = (int)originalSettings.PianorollSemitone,
                FretboardSemitone = (int)originalSettings.FretboardSemitone,
                CircleSemitone = (int)originalSettings.CircleSemitone,

                FretboardStrings = packedNotes,

                LastVisibleString = originalSettings.LastVisibleString
            };

            var xmlLoaderSaver = new XmlLoaderSaver(catchAndLogExceptions: false);

            xmlLoaderSaver.Handle_SaveRequest("TestSettings.xml", container);

            //Load and unpack
            container = (SettingsXmlRepack)xmlLoaderSaver.Handle_LoadRequest("TestSettings.xml", typeof(SettingsXmlRepack));

            int length = container.FretboardStrings.Length;
            var unpackedNotes = new Note[length];
            for (int i = 0; i < length; i++)
            {
                unpackedNotes[i] = new Note((Sound)container.FretboardStrings[i]);
            }

            var loadedSettings = new SettingsRequestEventArgs
                (
                    (Semitone)container.PianorollSemitone,
                    (Semitone)container.FretboardSemitone,
                    (Semitone)container.CircleSemitone,
                    unpackedNotes,
                    container.LastVisibleString
                );

            //Compare
            Assert.AreEqual(originalSettings.PianorollSemitone, loadedSettings.PianorollSemitone);
            Assert.AreEqual(originalSettings.FretboardSemitone, loadedSettings.FretboardSemitone);
            Assert.AreEqual(originalSettings.CircleSemitone, loadedSettings.CircleSemitone);
            Assert.AreEqual(originalSettings.LastVisibleString, loadedSettings.LastVisibleString);

            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(originalSettings.FretboardStrings[i], loadedSettings.FretboardStrings[i]);
            }
        }
    }
}