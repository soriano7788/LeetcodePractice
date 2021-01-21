using System;
using System.Text;

namespace IntegerToRoman
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] { 3, 4, 9, 58, 1994 };
            foreach (int num in nums)
            {
                Console.WriteLine("{0} = {1}", num, IntToRoman(num));
            }
        }
        private static string IntToRoman(int num)
        {
            // Symbol       Value
            // I             1
            // V             5
            // X             10
            // L             50
            // C             100
            // D             500
            // M             1000
            // Given an integer, convert it to a roman numeral. 
            // Input is guaranteed to be within the range from 1 to 3999.

            // 覺得類似換鈔票零錢的題目?
            StringBuilder romanNum = new StringBuilder();

            int mSymbol = num / 1000;
            if (mSymbol > 0)
            {
                for (int i = 0; i < mSymbol; i++)
                {
                    romanNum.Append("M");
                }
            }
            num = num % 1000;

            int cdSymbol = num / 100;
            if (cdSymbol > 0)
            {
                if (cdSymbol < 4)
                {
                    for (int i = 0; i < cdSymbol; i++)
                    {
                        romanNum.Append("C");
                    }
                }
                if (cdSymbol == 4)
                {
                    romanNum.Append("CD");
                }
                if (cdSymbol >= 5 && cdSymbol < 9)
                {
                    romanNum.Append("D");
                    for (int i = 0; i < cdSymbol - 5; i++)
                    {
                        romanNum.Append("C");
                    }
                }
                if (cdSymbol == 9)
                { 
                    romanNum.Append("CM");
                }
            }
            num = num % 100;

            int xlSymbol = num / 10;
            if (xlSymbol > 0)
            {
                if (xlSymbol < 4)
                {
                    for (int i = 0; i < xlSymbol; i++)
                    {
                        romanNum.Append("X");
                    }
                }
                if (xlSymbol == 4)
                {
                    romanNum.Append("XL");
                }
                if (xlSymbol >= 5 && xlSymbol < 9)
                {
                    romanNum.Append("L");
                    for (int i = 0; i < xlSymbol - 5; i++)
                    {
                        romanNum.Append("X");
                    }
                }
                if (xlSymbol == 9)
                {
                    romanNum.Append("XC");
                }
            }
            num = num % 10;

            int ivSymbol = num;
            if (ivSymbol < 4)
            {
                for (int i = 0; i < ivSymbol; i++)
                {
                    romanNum.Append("I");
                }
            }
            if (ivSymbol == 4)
            {
                romanNum.Append("IV");
            }
            if (ivSymbol >= 5 && ivSymbol < 9)
            {
                romanNum.Append("V");
                for (int i = 0; i < ivSymbol - 5; i++)
                {
                    romanNum.Append("I");
                }
            }
            if (ivSymbol == 9)
            {
                romanNum.Append("IX");
            }

            return romanNum.ToString();
        }
    }
}
