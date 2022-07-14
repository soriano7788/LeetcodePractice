using System;
using System.Text;

namespace MultiplyStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            string num1 = "123";
            string num2 = "456";
            // 56088

            num1 = "123456789";
            num2 = "987654321";
            // 121932631112635269


            string result = Multiply2(num1, num2);
            Console.WriteLine("result: {0}", result);
        }

        // (x) 跟LEETCODE 的顯示不同，但我覺得應該要算ok才對
        private static string Multiply(string num1, string num2)
        {
            if (/*num1 == "0" || num2 == "0"*/num1.Equals("0") || num2.Equals("0"))
            {
                return "0";
            }

            double product = 0;
            for (int i = num1.Length - 1; i >= 0; i--)
            {
                for (int k = num2.Length - 1; k >= 0; k--)
                {
                    int num1TenToPower = num1.Length - i - 1;
                    int num2TenToPower = num2.Length - k - 1;

                    int num1digit = Convert.ToInt32(num1[i].ToString());
                    int num2digit = Convert.ToInt32(num2[k].ToString());

                    double t = num1digit * num2digit * Math.Pow((double)10, (double)num1TenToPower) * Math.Pow((double)10, (double)num2TenToPower);
                    product += t;
                }
            }
            return product.ToString();
        }

        private static string Multiply2(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0")
            {
                return "0";
            }
            // 把 result 用 int new Array[num1.Length + num2.Length] 來儲存
            // 最後再轉回字串
            int num1Length = num1.Length;
            int num2Length = num2.Length;
            int[] result = new int[num1Length + num2.Length];


            for (int i = num1Length - 1; i >= 0; i--)
            {
                int digit1 = Convert.ToInt32(num1[i].ToString());
                for (int k = num2Length - 1; k >= 0; k--)
                {
                    int digit2 = Convert.ToInt32(num2[k].ToString());
                    int product = digit1 * digit2;

                    // 要注意進位!!!

                    result[i + k + 1] += product % 10;
                    result[i + k] += product / 10;
                }
            }
            // 處理進位
            for (int i = result.Length - 1; i > 0; i--)
            {
                int carry = result[i] / 10;
                result[i] = result[i] % 10;
                result[i - 1] += carry;
            }


            int z = 0;
            while (result[z] == 0)
            {
                z++;
            }
            StringBuilder sb = new StringBuilder();
            while (z < result.Length)
            {
                sb.Append(result[z].ToString());
                z++;
            }

            return sb.ToString();
        }
    }
}
