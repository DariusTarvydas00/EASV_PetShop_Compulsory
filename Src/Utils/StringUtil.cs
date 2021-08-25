using System;
using System.Reflection.Metadata;
using Xunit.Sdk;

namespace CrashCourse2021ExercisesDayOne.Utils
{
    internal class StringUtil
    {
        internal int LengthOfString(string stringToMeasure)
        {
            try
            {
                return stringToMeasure.Length;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(Constants.StringCannotBeNull);
            }
        }

        internal string SumStrings(string value1, string value2)
        {
            return (Int32.Parse(value1) + Int32.Parse(value2)).ToString();
        }

        internal string DivideString(string value1, string value2)
        {
            return (Int32.Parse(value1) / Int32.Parse(value2)).ToString();
        }

        internal string StringContains(string value1, string value2)
        {
            return value1.Contains(value2) ? "YES" : "NO";
        }

        internal string StringToUpperCase(string value1)
        {
            return value1.ToUpper();
        }

        internal string AdditionFromPlusString(string inputString)
        {
            var sum = 0;
            string[] numbers = inputString.Split('+');
            foreach (var number in numbers)
            {
                sum += Int32.Parse(number);
            }

            return sum.ToString();
            throw new NotImplementedException();
        }

        internal string EvenNumber(int numbertoTest)
        {
            return numbertoTest % 2 == 0 ? "EVEN" : "ODD";
        }
    }
}