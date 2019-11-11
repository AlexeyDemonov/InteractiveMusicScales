using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class ScalesSorter_Tests
    {
        private static ScalesSorter test;
        private static Sound _;

        [ClassInitialize]
        public static void InitializeFields(TestContext arg)
        {
            test = new ScalesSorter();
            _ = Sound.C;
        }

        [TestMethod]
        public void Compare_1stArgNull_Exception()
        {
            string expectedMesssage = "ScalesSorter.Compare: argument 'left' was null";
            string recievedMessage = string.Empty;

            try
            {
                test.Compare(null, new Scale("smth", _, _));
            }
            catch (ArgumentException ex)
            {
                recievedMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, recievedMessage);
        }

        [TestMethod]
        public void Compare_2ndArgNull_Exception()
        {
            string expectedMesssage = "ScalesSorter.Compare: argument 'right' was null";
            string recievedMessage = string.Empty;

            try
            {
                test.Compare(new Scale("smth", _, _), null);
            }
            catch (ArgumentException ex)
            {
                recievedMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, recievedMessage);
        }

        [TestMethod]
        public void Compare_1stArgNameNull_Exception()
        {
            string expectedMesssage = "ScalesSorter.Compare: 'left' scale Name was null";
            string recievedMessage = string.Empty;

            try
            {
                test.Compare(new Scale(null, _, _), new Scale("smth", _, _));
            }
            catch (ArgumentException ex)
            {
                recievedMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, recievedMessage);
        }

        [TestMethod]
        public void Compare_2ndArgNameNull_Exception()
        {
            string expectedMesssage = "ScalesSorter.Compare: 'right' scale Name was null";
            string recievedMessage = string.Empty;

            try
            {
                test.Compare(new Scale("smth", _, _), new Scale(null, _, _));
            }
            catch (ArgumentException ex)
            {
                recievedMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, recievedMessage);
        }

        [TestMethod]
        public void Compare_EqualCorrectValues_0()
        {
            int result = test.Compare(new Scale("C major", _, _), new Scale("C major", _, _));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Compare_EqualIncorrectValues_0()
        {
            int result = test.Compare(new Scale("Marvel", _, _), new Scale("Marvel", _, _));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Compare_CorrectIncorrect_minus1()
        {
            int result = test.Compare(new Scale("C major", _, _), new Scale("Marvel", _, _));

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Compare_IncorrectCorrect_plus1()
        {
            int result = test.Compare(new Scale("Marvel", _, _), new Scale("C major", _, _));

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Compare_DifferentBeginnings_plus1()
        {
            int result = test.Compare(new Scale("C# major", _, _), new Scale("C major", _, _));

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Compare_DifferentBeginnings_minus1()
        {
            int result = test.Compare(new Scale("C# major", _, _), new Scale("E major", _, _));

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Compare_EqualBeginnings_plus1()
        {
            int result = test.Compare(new Scale("C minor", _, _), new Scale("C major", _, _));

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Compare_IncorrectBeginnings_plus1()
        {
            int result = test.Compare(new Scale("Cminor", _, _), new Scale("Cmajor", _, _));

            Assert.AreEqual(1, result);
        }
    }
}