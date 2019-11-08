using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InteractiveMusicScales.Tests
{
    [TestClass]
    public class ScalesCirclesHolder_Tests
    {
        static Scale[] testScales;

        [ClassInitialize]
        public static void InitializeFields(TestContext arg)
        {
            testScales = new Scale[]
            {
                new Scale("C major", keynoteSound:Sound.C, scaleSound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B),
                new Scale("G major", keynoteSound:Sound.G, scaleSound:Sound.C | Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("D major", keynoteSound:Sound.D, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("A major", keynoteSound:Sound.A, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("E major", keynoteSound:Sound.E, scaleSound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("B major", keynoteSound:Sound.B, scaleSound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_ScalesNull_Exception()
        {
            new ScalesCirclesHolder(null);
        }

        [TestMethod]
        public void Ctor_ScalesEmpty_Exception()
        {
            string expectedMesssage = "ScalesCirclesHolder.Ctor: 'scales' array can not be empty (its length was zero)";
            string actualMessage = default(string);

            try
            {
                new ScalesCirclesHolder(new Scale[0]);
            }
            catch (ArgumentException ex)
            {
                actualMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, actualMessage);
        }

        [TestMethod]
        public void Ctor_CirclesZero_Exception()
        {
            string expectedMesssage = "ScalesCirclesHolder.Ctor: 'divideToNumberOfCircles' argument can not be zero or negative (its value was 0 )";
            string actualMessage = default(string);

            try
            {
                new ScalesCirclesHolder(testScales, 0);
            }
            catch (ArgumentException ex)
            {
                actualMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, actualMessage);
        }

        [TestMethod]
        public void Ctor_CirclesNegative_Exception()
        {
            string expectedMesssage = "ScalesCirclesHolder.Ctor: 'divideToNumberOfCircles' argument can not be zero or negative (its value was -5 )";
            string actualMessage = default(string);

            try
            {
                new ScalesCirclesHolder(testScales, -5);
            }
            catch (ArgumentException ex)
            {
                actualMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, actualMessage);
        }

        [TestMethod]
        public void Ctor_CorrectArgs_NoException()
        {
            new ScalesCirclesHolder(testScales);
            new ScalesCirclesHolder(testScales, 2);
            new ScalesCirclesHolder(testScales, 10);
        }

        [TestMethod]
        public void Indexer_CircleNegative_Exception()
        {
            string expectedMesssage = "ScalesCirclesHolder.Indexer: 'circle' argument cannot be negative (its value was -1 )";
            string actualMessage = default(string);

            var test = new ScalesCirclesHolder(testScales);

            try
            {
                var scale = test[-1,0];
            }
            catch (ArgumentException ex)
            {
                actualMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, actualMessage);
        }

        [TestMethod]
        public void Indexer_CircleBorderHigh_Exception()
        {
            string expectedMesssage = "ScalesCirclesHolder.Indexer: 'circle' argument cannot be greater or equal to number of circles (its value was 1 )";
            string actualMessage = default(string);

            var test = new ScalesCirclesHolder(testScales);

            try
            {
                var scale = test[1, 0];
            }
            catch (ArgumentException ex)
            {
                actualMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, actualMessage);
        }

        [TestMethod]
        public void Indexer_CircleHigh_Exception()
        {
            string expectedMesssage = "ScalesCirclesHolder.Indexer: 'circle' argument cannot be greater or equal to number of circles (its value was 2 )";
            string actualMessage = default(string);

            var test = new ScalesCirclesHolder(testScales);

            try
            {
                var scale = test[2, 0];
            }
            catch (ArgumentException ex)
            {
                actualMessage = ex.Message;
            }

            Assert.AreEqual(expectedMesssage, actualMessage);
        }

        [TestMethod]
        public void Indexer_TwoCircles_0_0_Cmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 2);
            var recievedScale = test[0,0];

            Assert.AreEqual(testScales[0], recievedScale);
        }

        [TestMethod]
        public void Indexer_TwoCircles_1_0_Amajor()
        {
            var test = new ScalesCirclesHolder(testScales, 2);
            var recievedScale = test[1, 0];

            Assert.AreEqual(testScales[3], recievedScale);
        }

        [TestMethod]
        public void Indexer_TwoCircles_0_5_Dmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 2);
            var recievedScale = test[0, 5];

            Assert.AreEqual(testScales[2], recievedScale);
        }

        [TestMethod]
        public void Indexer_TwoCircles_0_minus5_Gmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 2);
            var recievedScale = test[0, -5];

            Assert.AreEqual(testScales[1], recievedScale);
        }

        [TestMethod]
        public void Indexer_TwoCircles_ShiftR_0_0_Gmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 2);
            test.ShiftRight();
            var recievedScale = test[0, 0];

            Assert.AreEqual(testScales[1], recievedScale);
        }

        [TestMethod]
        public void Indexer_TwoCircles_ShiftL_1_10_Amajor()
        {
            var test = new ScalesCirclesHolder(testScales, 2);
            test.ShiftLeft();
            var recievedScale = test[1, 10];

            Assert.AreEqual(testScales[3], recievedScale);
        }

        [TestMethod]
        public void Indexer_ThreeCircles_0_0_Cmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 3);
            var recievedScale = test[0, 0];

            Assert.AreEqual(testScales[0], recievedScale);
        }

        [TestMethod]
        public void Indexer_ThreeCircles_1_0_Dmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 3);
            var recievedScale = test[1, 0];

            Assert.AreEqual(testScales[2], recievedScale);
        }

        [TestMethod]
        public void Indexer_ThreeCircles_0_5_Gmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 3);
            var recievedScale = test[0, 5];

            Assert.AreEqual(testScales[1], recievedScale);
        }

        [TestMethod]
        public void Indexer_ThreeCircles_2_minus5_Bmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 3);
            var recievedScale = test[2, -5];

            Assert.AreEqual(testScales[5], recievedScale);
        }

        [TestMethod]
        public void Indexer_ThreeCircles_ShiftR_2_0_Bmajor()
        {
            var test = new ScalesCirclesHolder(testScales, 3);
            test.ShiftRight();
            var recievedScale = test[2, 0];

            Assert.AreEqual(testScales[5], recievedScale);
        }

        [TestMethod]
        public void Indexer_ThreeCircles_ShiftL_1_10_Amajor()
        {
            var test = new ScalesCirclesHolder(testScales, 3);
            test.ShiftLeft();
            var recievedScale = test[1, 10];

            Assert.AreEqual(testScales[3], recievedScale);
        }
    }
}
