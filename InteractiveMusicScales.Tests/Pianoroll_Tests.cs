using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class Pianoroll_Tests
    {
        static Note testSingleNote;
        static Note[] testNotes;
        static Pianoroll testPianoroll;

        [ClassInitialize]
        public static void InitializeFields(TestContext arg)
        {
            testSingleNote = new Note(Sound.E);
            testNotes = new Note[] { new Note(Sound.C), new Note(Sound.D), new Note(Sound.E) };
            testPianoroll = new Pianoroll(testNotes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NotesNull_ArgumentNullException()
        {
            new Pianoroll(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NotesEmpty_ArgumentException()
        {
            new Pianoroll(new Note[0]);
        }

        [TestMethod]
        public void Ctor_CorrectArgs_NoException()
        {
            new Pianoroll(testNotes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndexer_KeyNegative_ArgumentException()
        {
            var returnedNote = testPianoroll[-1];
        }

        [TestMethod]
        public void GetIndexer_Key0_C()
        {
            var returnedNote = testPianoroll[0];
            Assert.AreEqual(new Note(Sound.C), returnedNote);
        }

        [TestMethod]
        public void GetIndexer_Key2_E()
        {
            var returnedNote = testPianoroll[2];
            Assert.AreEqual(new Note(Sound.E), returnedNote);
        }

        [TestMethod]
        public void GetIndexer_Key3_C()
        {
            var returnedNote = testPianoroll[3];
            Assert.AreEqual(new Note(Sound.C), returnedNote);
        }

        [TestMethod]
        public void GetIndexer_Key25_D()
        {
            var returnedNote = testPianoroll[25];
            Assert.AreEqual(new Note(Sound.D), returnedNote);
        }
    }
}
