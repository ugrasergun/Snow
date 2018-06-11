using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnowLibrary;
using System.Drawing;

namespace SnowLibraryTest
{
    [TestClass]
    public class SnowLibraryTest
    {
        [TestMethod]
        public void ShouldParseTheData()
        {
            string barLine = "#A:RED:5";
            Bar expected = new Bar { Name = "A", Colour = Color.Red, Value = 5 };
            IParser parser = new SnowParser();

            var result = parser.Parse(barLine);

            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void ShouldParseWithNegativeValue()
        {
            string barLine = "#A:RED:-5";
            Bar expected = new Bar { Name = "A", Colour = Color.Red, Value = -5 };
            IParser parser = new SnowParser();

            var result = parser.Parse(barLine);

            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void ShouldFailWithNull()
        {
            string barLine = null;
            IParser parser = new SnowParser();
            Exception expectedEx = null;

            try
            {
                var result = parser.Parse(barLine);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentNullException));
            Assert.AreEqual(expectedEx.Message, "Value cannot be null.\r\nParameter name: barLine");
        }

        [TestMethod]
        public void ShouldFailWithNoHash()
        {
            string barLine = "A:RED:5";
            IParser parser = new SnowParser();
            Exception expectedEx = null;

            try
            {
                var result = parser.Parse(barLine);
            }
            catch(Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
            Assert.AreEqual(expectedEx.Message, "BarLine should start with '#'\r\nParameter name: barLine");
        }

        [TestMethod]
        public void ShouldFailWhenNameIsNotAlphaNumeric()
        {
            string barLine = "#A/:RED:5";
            IParser parser = new SnowParser();
            Exception expectedEx = null;

            try
            {
                var result = parser.Parse(barLine);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
            Assert.AreEqual(expectedEx.Message, "Name Should Be Alphanumeric\r\nParameter name: name");
        }

        [TestMethod]
        public void ShouldFailWhenColourIsInvalid()
        {
            string barLine = "#A:NotAColour:5";
            IParser parser = new SnowParser();
            Exception expectedEx = null;

            try
            {
                var result = parser.Parse(barLine);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
            Assert.AreEqual(expectedEx.Message, "NotAColour is not a valid colour");
        }

        [TestMethod]
        public void ShouldFailWhenValueIsNotInteger()
        {
            string barLine = "#A:RED:five";
            IParser parser = new SnowParser();
            Exception expectedEx = null;

            try
            {
                var result = parser.Parse(barLine);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
            Assert.AreEqual(expectedEx.Message, "Bar Value should be a integer\r\nParameter name: value");
        }

        [TestMethod]
        public void ShouldParseAll()
        {
            string[] barLines = { "#A:RED:5", "#B:Yellow:10" };
            Bar expected0 = new Bar { Name = "A", Colour = Color.Red, Value = 5 };
            Bar expected1 = new Bar { Name = "B", Colour = Color.Yellow, Value = 10 };
            IParser parser = new SnowParser();

            var result = parser.ParseAll(barLines);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(expected0.Equals(result[0]));
            Assert.IsTrue(expected1.Equals(result[1]));
        }

        [TestMethod]
        public void ShouldFailWhenBarLinesNull()
        {
            string[] barLines = null;
            IParser parser = new SnowParser();
            Exception expectedEx = null;

            try
            {
                var result = parser.ParseAll(barLines);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentNullException));
            Assert.AreEqual(expectedEx.Message, "Value cannot be null.\r\nParameter name: barLines");
        }

        [TestMethod]
        public void ShouldFailWhenBarLinesEmpty()
        {
            string[] barLines = new string[0];
            IParser parser = new SnowParser();
            Exception expectedEx = null;

            try
            {
                var result = parser.ParseAll(barLines);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
            Assert.AreEqual(expectedEx.Message, "barLines collection can not be empty\r\nParameter name: barLines");
        }


    }
}
