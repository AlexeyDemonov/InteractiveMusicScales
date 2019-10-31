using System;
using InteractiveMusicScales.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class NoteButton_Tests
    {
        [TestMethod]
        public void Ctor_UnassignedNoteProperty_trueAndNoException()
        {
            var test = new NoteButton();
            
            Assert.AreEqual(true, (test.Note == null) && !test.IsChecked && !test.IsKeyNote);
        }

        [TestMethod]
        public void NoteProperty_Null_trueAndNoException()
        {
            var test = new NoteButton();
            test.Note = null;

            Assert.AreEqual(true, (test.Note == null) && !test.IsChecked && !test.IsKeyNote);
        }

        [TestMethod]
        public void NoteProperty_Note_trueAndNoException()
        {
            var test = new NoteButton();
            test.Note = new Note(Sound.C);

            Assert.AreEqual(true, (test.Note != null) && !test.IsChecked && !test.IsKeyNote);
        }

        [TestMethod]
        public void NoteProperty_NoteWithTrueProperties_trueAndNoException()
        {
            var test = new NoteButton();
            var note = new Note(Sound.C) { IsKeynote=true, IsChecked=true };
            test.Note = note;

            Assert.AreEqual(true, (test.Note != null) && test.IsChecked && test.IsKeyNote);
        }

    }
}
