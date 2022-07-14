using System;

namespace DivideTwoIntegers
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/divide-two-integers/
            // For example, truncate(8.345) = 8 and truncate(-2.7335) = -2
            // Both dividend and divisor will be 32-bit signed integers.
            // The divisor will never be 0.

            // Assume we are dealing with an environment 
            // which could only store integers within the 32-bit signed integer range: [−2^31,  2^31 − 1]. 
            // For the purpose of this problem, assume that your function returns 2^31 − 1 when the division result overflows.

            int dividend = 2147483647,
                divisor = 1;
            //long a = Convert.ToInt32(2147483648);
            // long a = (long)dividend;

            int result = Divide(dividend, divisor);
            Console.WriteLine("result: {0}", result);
            //Console.WriteLine(Int32.TryParse("-2147483648", out a));
            // int32 range: -2147483648~2147483647
            //Convert.ToInt32(2147483648);

            // Console.WriteLine(Math.Abs(-2147483648));
        }
        private static int Divide(int dividend, int divisor)
        {
                long quotient = 0;
                bool containNegative = false;
                if( (dividend > 0 && divisor < 0) || 
                    ( dividend < 0 && divisor > 0) )
                {
                    containNegative = true;
                }
                long longDividend = dividend,
                     longDivisor = divisor;

                longDividend = Math.Abs(longDividend);
                longDivisor = Math.Abs(longDivisor);
                while(longDividend >= longDivisor)
                {
                    longDividend = longDividend - longDivisor;
                    quotient++;
                }
                if(containNegative)
                {
                    quotient *= -1;
                }
                try
                {
                    return Convert.ToInt32(quotient);
                }
                catch(Exception)
                {
                    return Int32.MaxValue;
                }

            // try
            // {
            //     int result = dividend / divisor;
            //     return result;
            // }
            // catch(Exception e)
            // {
            //     return Int32.MaxValue;
            // }
        }
    }
}
