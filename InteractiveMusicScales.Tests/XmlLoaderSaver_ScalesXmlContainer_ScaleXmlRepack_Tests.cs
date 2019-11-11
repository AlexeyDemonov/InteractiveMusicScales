using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InteractiveMusicScales.Managers;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class XmlLoaderSaver_ScalesXmlContainer_ScaleXmlRepack_Tests
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

            ScaleXmlRepack[] packedScales = new ScaleXmlRepack[originalScales.Length];

            for (int i = 0; i < originalScales.Length; i++)
            {
                var scale = originalScales[i];
                packedScales[i] = new ScaleXmlRepack() { Name = scale.Name, Sound = (int)scale.Sound, Keynote = (int)scale.KeynoteSound };
            }

            var container = new ScalesXmlContainer() { Scales = packedScales };

            var xmlLoaderSaver = new XmlLoaderSaver(catchAndLogExceptions: false);

            xmlLoaderSaver.Handle_SaveRequest("TestScales.xml", container);



            //Load and unpack
            var loadedContainer = (ScalesXmlContainer)xmlLoaderSaver.Handle_LoadRequest("TestScales.xml", typeof(ScalesXmlContainer));

            packedScales = loadedContainer.Scales;
            int length = loadedContainer.Scales.Length;
            Scale[] loadedScales = new Scale[length];

            for (int i = 0; i < length; i++)
            {
                var packedScale = loadedContainer.Scales[i];
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
