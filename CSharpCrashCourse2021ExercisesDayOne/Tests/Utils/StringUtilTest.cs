using System;
using CrashCourse2021ExercisesDayOne.Utils;
using Xunit;

namespace CrashCourse2021ExercisesDayOne.Tests.Utils
{
    public class StringUtilTest
    {
        private StringUtil util;

        public StringUtilTest()
        {
            util = new StringUtil();
        }

        #region Length of string

        //Test if Null throws exception (Just a demo, dont worry about it yet)
        [Fact]
        public void LengthOfNullStringThrowsNullReferenceException()
        {
            var exception = Assert.Throws<NullReferenceException>(() => util.LengthOfString(null));
            Assert.Equal("String cannot be null!", exception.Message);
        }

        //Test if length of HelloWorld! is 12 and LengthOfString method is working
        [Fact]
        public void LengthOfHelloWorldIs12()
        {
            Assert.Equal(12, util.LengthOfString("Hello World!"));
        }

        //Multiple tests if LengthOfString method is working
        [Theory]
        [InlineData("Sing", 4)]
        [InlineData("The", 3)]
        [InlineData("Longest", 7)]
        [InlineData("Song", 4)]
        [InlineData("For", 3)]
        [InlineData("Me", 2)]
        [InlineData("", 0)]
        [InlineData("NOOOOOOOOOOOOOWWWWWWWWWWWWWWWWWWWWWWWWwwwwWWWWWWwwwWWWWwwWWWWWwwwwwWwwWWWwww", 76)]
        public void LengthOfStringsMatchFacts(string text, int length)
        {
            Assert.Equal(length, util.LengthOfString(text));
        }

        #endregion

        #region Sum of strings as string
        // Create a method that returns the sum of two numbers.
        [Fact]
        public void ReturnsSumAsStringFromTwoString()
        {
            Assert.Equal("11", util.SumStrings("6","5"));
        }

        // Previous method must pass the following Multiple tests.
        [Theory]
        [InlineData("1", "1", "0")]
        [InlineData("1", "0", "1")]
        [InlineData("-1", "0", "-1")]
        [InlineData("1000", "500", "500")]
        public void TheoriesReturnsSumAsStringFromTwoString(string length, string value1, string value2)
        {
            Assert.Equal(length, util.SumStrings(value1, value2));
        }

        #endregion

        #region Divide two strings return as string
        // Create a method that returns the division of two numbers.
        [Fact]
        public void ReturnsDivideAsStringFromTwoString()
        {
            Assert.Equal("2", util.DivideString("6", "3"));
        }

        // Previous method must pass the following Multiple tests.
        [Theory]
        [InlineData("1", "1", "1")]
        [InlineData("100", "1000", "10")]
        [InlineData("200000", "100000000", "500")]
        public void TheoriesReturnsDivideAsStringFromTwoString(string length, string value1, string value2)
        {
            Assert.Equal(length, util.DivideString(value1, value2));
        }

        #endregion

        #region String Value1 Contains String Value2
        // Create a method that returns "YES" if a string (value1) contains the string (value2) send into the method.
        [Fact]
        public void ReturnsYesIfStringValue1ContainsStringValue2()
        {
            Assert.Equal(Constants.AnswerYes, util.StringContains("Hello", "Hell"));
        }

        // Create a method that returns "No" if a string (value1) contains the string (value2) send into the method.
        [Fact]
        public void ReturnsNoIfStringValue1ContainsStringValue2()
        {
            Assert.Equal(Constants.AnswerNo, util.StringContains("Hello", "Helly"));
        }

        // Previous method must pass the following Multiple tests.
        [Theory]
        [InlineData("YES", "Hi All", "i Al")]
        [InlineData("NO", "How U Doin?", "doin")]
        [InlineData("YES", "Snurfs Gotcha Troublesies! ", " Troublesies! ")]
        public void TheoriesReturnsYesOrNoDependingOnValue1AndValue2(string answer, string value1, string value2)
        {
            Assert.Equal(answer, util.StringContains(value1, value2));
        }
        #endregion

        #region Make String Uppercase
        //Test if Null throws exception (Just a demo, dont worry about it yet)
        [Fact]
        public void StringToUpperCase_WithValidString_ReturnsFullUpperCaseString()
        {
            Assert.Equal("OST", util.StringToUpperCase("ost"));
        }

        //Multiple tests if StringToUpperCase method is working
        [Theory]
        [InlineData("SING", "Sing")]
        [InlineData("SNOW", "sNOw")]
        [InlineData("THE/|ØF", "thE/|Øf")]
        public void StringToUpperCase_WithANYValidString_ReturnsFullUpperCaseString(string valueOut, string valueIn)
        {
            Assert.Equal(valueOut, util.StringToUpperCase(valueIn));
        }

        #endregion

        #region Addition list of values in String split on "+"
        //Test if Null throws exception (Just a demo, dont worry about it yet)
        [Fact]
        public void AdditionFromPlusString_WithValidString_ReturnsAdditionedValueAsString()
        {
            Assert.Equal("76", util.AdditionFromPlusString("4+55+4+3+10"));
        }

        [Theory]
        [InlineData("176", "100+4+55+4+3+10")]
        [InlineData("2", "3+-1")]
        [InlineData("500", "599+-99+15+-15")]
        public void MultipleAdditionFromPlusString_WithValidString_ReturnsAdditionedValueAsString(string valueOut, string valueIn)
        {
            Assert.Equal(valueOut, util.AdditionFromPlusString(valueIn));
        }

        #endregion

        #region Even/Odd method.. Number Returns "EVEN" Odd Number Returns "ODD"
        //Test if Null throws exception (Just a demo, dont worry about it yet)
        [Fact]
        public void EvenNumber_WithValidIntOf2_ReturnsEvenAsString()
        {
            Assert.Equal(Constants.Even, util.EvenNumber(2));
        }

        [Theory]
        [InlineData("EVEN", 2)]
        [InlineData("ODD", 2345)]
        [InlineData("ODD", 1)]
        [InlineData("ODD", -11)]
        [InlineData("EVEN", -12)]
        [InlineData("EVEN", -0)]
        public void MultipleEvenNumber_WithValidIntOf2_ReturnsEvenAsString(string valueOut, int valueIn)
        {
            Assert.Equal(valueOut, util.EvenNumber(valueIn));
        }

        #endregion
    }
}
