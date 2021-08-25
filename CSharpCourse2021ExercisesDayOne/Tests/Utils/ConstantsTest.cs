using CrashCourse2021ExercisesDayOne.Utils;
using Xunit;

namespace CrashCourse2021ExercisesDayOne.Tests.Utils
{
    public class ConstantsTest
    {
        [Fact]
        public void ConstantAnswerYesIsYes()
        {
            Assert.Equal("YES", Constants.AnswerYes);
        }

        [Fact]
        public void ConstantAnswerNoIsNo()
        {
            Assert.Equal("NO", Constants.AnswerNo);
        }

        [Fact]
        public void ConstantStringCannotBeNullTest()
        {
            Assert.Equal("String cannot be null!", Constants.StringCannotBeNull);
        }

        [Fact]
        public void ConstantStringEvenTest()
        {
            Assert.Equal("EVEN", Constants.Even);
        }

        [Fact]
        public void ConstantStringOddest()
        {
            Assert.Equal("ODD", Constants.Odd);
        }


    }
}
