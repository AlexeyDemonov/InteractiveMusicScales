using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InteractiveMusicScales.Managers;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class XmlLoadSaver_ScalesXmlContainer_ScaleXmlRepack_Tests
    {
        [TestMethod]
        public void Write_Load_Compare_Equal_NoException()
        {

            //Pack and save
            Scale[] originalScales = new Scale[]
            {
                new Scale("C major", keynoteSound:Sound.C, scaleSound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B),
                new Scale("G major", keynoteSound:Sound.G, scaleSound:Sound.C | Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("D major", keynoteSound:Sound.D, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
            };

            ScaleXmlRepack[] scalesPacked = new ScaleXmlRepack[originalScales.Length];

            for (int i = 0; i < originalScales.Length; i++)
            {
                var scale = originalScales[i];
                scalesPacked[i] = new ScaleXmlRepack() { Name = scale.Name, Sound = (int)scale.Sound, Keynote = (int)scale.KeynoteSound };
            }

            var container = new ScalesXmlContainer() { Scales = scalesPacked };

            var xmlLoadSaver = new XmlLoadSaver(catchAndLogExceptions: false);

            xmlLoadSaver.Handle_SaveRequest("TEST.xml", container);



            //Load and unpack
            var loadedContainer = (ScalesXmlContainer)xmlLoadSaver.Handle_LoadRequest("TEST.xml", typeof(ScalesXmlContainer));

            scalesPacked = loadedContainer.Scales;

            Scale[] loadedScales = new Scale[scalesPacked.Length];

            for (int i = 0; i < scalesPacked.Length; i++)
            {
                var packedScale = scalesPacked[i];
                loadedScales[i] = new Scale( packedScale.Name, (Sound)packedScale.Keynote, (Sound)packedScale.Sound );
            }



            //Compare
            for (int i = 0; i < originalScales.Length; i++)
            {
                Scale originalScale = originalScales[i];
                Scale loadedScale = loadedScales[i];

                Assert.AreEqual( originalScale.Name, loadedScale.Name );
                Assert.AreEqual( originalScale.Sound, loadedScale.Sound );
                Assert.AreEqual( originalScale.KeynoteSound, loadedScale.KeynoteSound);
            }
        }
    }
}
