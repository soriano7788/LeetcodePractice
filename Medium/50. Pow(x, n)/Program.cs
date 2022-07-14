using System;

namespace Pow
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/powx-n/
            double x = 2;
            int n = 10;
            // ouput: 1024

            // x = 2.1;
            // n = 3;
            // ouput: 9.261

            // x = 2;
            // n = -2;
            // ouput: 1/4 = 0.25

            x = 0.00001;
            n = 2147483647;

            double result = MyPow(x, n);
            Console.WriteLine("result: {0}", result);
        }
        private static double MyPow(double x, int n)
        {
            // double result = 1;
            // for (int i = 0; i < Math.Abs(n); i++)
            // {
            //     result *= x;
            // }
            // if (n > 0)
            // {
            //     return result;
            // }
            // return 1 / result;
            
            /////////////////////////
            // 以 2 的 3 次方帶入思考看看
            // 以 2 的 6 次方帶入思考看看
            // 以 2 的 8 次方帶入思考看看

            if (n == 0)
                return 1;

            double temp = x;
            // 把一半的次方運算交給遞迴
            temp = MyPow(x, n / 2);
            
            if (n % 2 == 0)
                return temp * temp;
            else
            {
                if (n > 0)
                    return x * temp * temp;
                else
                    return (temp * temp) / x;
            }
        }
    }
}
