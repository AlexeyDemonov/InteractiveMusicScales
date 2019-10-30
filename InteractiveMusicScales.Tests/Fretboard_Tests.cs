using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class Fretboard_Tests
    {
        static Note testSingleNote;
        static Note[] testNotes;
        static Fretboard testFretboard;

        [ClassInitialize]
        public static void InitializeFields(TestContext arg)
        {
            testSingleNote = new Note(Sound.E);
            testNotes = new Note[] { new Note(Sound.C), new Note(Sound.D), new Note(Sound.E) };
            testFretboard = new Fretboard(testNotes, 3);
        }

        [TestInitialize]
        public void RefreshFretboard0String()
        {
            testFretboard[0] = new Note(Sound.C);
        }


        [TestMethod]
        [ExpectedException (typeof(ArgumentNullException))]
        public void Ctor_NotesNull_ArgumentNullException()
        {
            new Fretboard(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NotesEmpty_ArgumentException()
        {
            new Fretboard(new Note[0], 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_StringsZero_ArgumentException()
        {
            new Fretboard(testNotes, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_StringsNegative_ArgumentException()
        {
            new Fretboard(testNotes, -1);
        }

        [TestMethod]
        public void Ctor_CorrectArgs_NoException()
        {
            new Fretboard(testNotes, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndexer_StringNegative_ArgumentException()
        {
            var test = testFretboard[-1, 0];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndexer_StringIndex3_ArgumentException()
        {
            var test = testFretboard[3, 0];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndexer_StringIndex5_ArgumentException()
        {
            var test = testFretboard[5, 0];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndexer_FretNegative_ArgumentException()
        {
            var test = testFretboard[0, -1];
        }

        [TestMethod]
        public void GetIndexer_String1Fret0_C()
        {
            var returnedNote = testFretboard[1, 0];

            Assert.AreEqual(new Note(Sound.C), returnedNote);
        }

        [TestMethod]
        public void GetIndexer_String1Fret2_E()
        {
            var returnedNote = testFretboard[1, 2];

            Assert.AreEqual(new Note(Sound.E), returnedNote);
        }

        [TestMethod]
        public void GetIndexer_String1Fret3_C()
        {
            var returnedNote = testFretboard[1, 3];

            Assert.AreEqual(new Note(Sound.C), returnedNote);
        }

        [TestMethod]
        public void GetIndexer_String1Fret25_D()
        {
            var returnedNote = testFretboard[1, 25];

            Assert.AreEqual(new Note(Sound.D), returnedNote);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexer_StringNegative_ArgumentException()
        {
            testFretboard[-1] = testSingleNote;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexer_String3_ArgumentException()
        {
            testFretboard[-1] = testSingleNote;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetIndexer_NullValue_ArgumentNullException()
        {
            testFretboard[0] = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexer_AbsolutelyNewNote_ArgumentException()
        {
            testFretboard[0] = new Note(Sound.F);
        }

        [TestMethod]
        public void SetIndexer_String0NoteE_NoException()
        {
            testFretboard[0] = new Note(Sound.E);
        }

        [TestMethod]
        public void SetIndexer_String0NoteE_GetFret0_E()
        {
            testFretboard[0] = new Note(Sound.E);
            var returnedNote = testFretboard[0,0];

            Assert.AreEqual(new Note(Sound.E), returnedNote);
        }

        [TestMethod]
        public void SetIndexer_String0NoteE_GetFret25_C()
        {
            testFretboard[0] = new Note(Sound.E);
            var returnedNote = testFretboard[0, 25];

            Assert.AreEqual(new Note(Sound.C), returnedNote);
        }
    }
}
