﻿using System;

namespace CrashCourse2021ExercisesDayOne.Utils
{
    internal class StringUtil
    {
        internal int LengthOfString(string stringToMeasure)
        {
            return stringToMeasure.Length;
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
            throw new NotImplementedException();
        }

        internal string EvenNumber(int numbertoTest)
        {
            throw new NotImplementedException();
        }
    }
}