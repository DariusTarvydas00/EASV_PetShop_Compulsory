using Xunit;

namespace CrashCourse2021ExercisesDayThree.Tests
{
    public class ConstantsTest
    {
        [Fact]
        public void ConstantStringEvenTest()
        {
            Assert.Equal("FirstName Must Be 2 Or More Characters", Constants.FirstNameException);
        }
    }
}
