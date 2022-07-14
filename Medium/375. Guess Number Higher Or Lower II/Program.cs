using System;

namespace GuessNumberHigherOrLowerII
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            // int pay = GetMoneyAmount(n);
            // int pay = GetMoneyAmount2(n);
            int pay = GetMoneyAmount3(n);
            Console.WriteLine("pay: {0}", pay);
        }
        private static int GetMoneyAmount(int n)
        {
            int[,] dp = new int[n + 1, n + 1];

            return Helper(1, n, dp);
        }

        // dp 非遞迴解
        private static int Helper(int start, int end, int[,] dp)
        {
            // 表示此位置已被填值??，可直接回傳
            if (dp[start, end] != 0)
            {
                return dp[start, end];
            }

            // start 等於 end 的 case，只有一個數字可選，一定猜對
            if (start >= end)
            {
                return 0;
            }

            // 數列數量 小於於等於 3 的 case，也就是數列數量為 2 或 3 的 case
            if (start >= end - 2)
            {
                // 3 個數字， end-1 等於取中間數字
                // 2 個數字， end-1 等於取第一個數字
                dp[start, end] = end - 1;
                return dp[start, end];
            }
            // 為何這邊定義的 mid 要先減 1?
            int mid = ((start + end) / 2) - 1,
                min = Int32.MaxValue;

            // loop mid 逐漸往右移動
            while (mid < end)
            {
                int left = Helper(start, mid - 1, dp);
                int right = Helper(mid + 1, end, dp);
                min = Math.Min(min, mid + Math.Max(left, right));

                // 為何?
                if (right <= left)
                {
                    break;
                }

                mid++;
            }
            dp[start, end] = min;
            return dp[start, end];
        }

        // https://leetcode.com/problems/guess-number-higher-or-lower-ii/discuss/84764/Simple-DP-solution-with-explanation~~
        private static int GetMoneyAmount2(int n)
        {
            int[,] table = new int[n + 1, n + 1];
            return DP(table, 1, n);
        }
        private static int DP(int[,] t, int s, int e)
        {
            if(s >= e)
            {
                return 0;
            }

            if(t[s, e] != 0)
            {
                return t[s, e];
            }

            int res = Int32.MaxValue;
            for(int i = s; i <= e; i++)
            {
                int tmp = i + Math.Max(DP(t, s, i-1),DP(t, i + 1, e));
                // 猜錯數字時，所有最差 case 裡的最佳值
                res = Math.Min(res,tmp);
            }
            t[s, e] = res;
            return res;
        }

        // https://leetcode.com/problems/guess-number-higher-or-lower-ii/discuss/84764/Simple-DP-solution-with-explanation~~
        // Here is a bottom up solution.
        // 跟 GetMoneyAmount 的概念似乎一樣
        private static int GetMoneyAmount3(int n)
        {
            int[,] table = new int[n + 1, n + 1];
            for(int j = 2; j <= n; j++)
            {
                for(int i = j - 1; i > 0; i--)
                {
                    int globalMin = Int32.MaxValue;
                    for(int k = i + 1; k < j; k++)
                    {
                        int localMax = k + Math.Max(table[i, k - 1], table[k + 1, j]);
                        globalMin = Math.Min(globalMin, localMax);
                    }
                    table[i, j] = (i + 1 == j) ? i : globalMin;
                }
            }
            return table[1, n];
        }
    }
}
