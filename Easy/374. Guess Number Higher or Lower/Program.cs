using System;

namespace GuessNumberHigherOrLower
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/guess-number-higher-or-lower/
            // 題目假設有一個 guess method，會回傳 0 -1 1
            // n 是上限數字，pick 範圍為 1~n
            int n = 10;

            n = 2;
            n = 2126753390;

            int num = GuessNumber2(n);
            Console.WriteLine("num: {0}", num);
        }
        // brute force
        private static int GuessNumber(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                if (guess(i) == 0)
                {
                    return i;
                }
            }
            return n;
        }

        // 二分搜尋
        private static int GuessNumber2(int n)
        {
            int start = 1,
                 end = n;

            while (start <= end)
            {
                int interval = (end - start + 1) / 2;
                int mid = start + interval;
                int ans = (int)guess(mid);
                if (ans == 0)
                {
                    return mid;
                }
                else if (ans == 1)
                {
                    start = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }
            }
            return -1;
        }
        // 三分搜尋
        private static int GuessNumber3(int n)
        {
            int start = 1,
                end = n;

            while (start <= end)
            {
                int interval = (end - start + 1) / 3;
                int m1 = start + interval,
                    m2 = end - interval;

                int a1 = guess(m1);
                int a2 = guess(m2);

                if (a1 == 0)
                {
                    return m1;
                }
                if (a2 == 0)
                {
                    return m2;
                }

                // 在左邊區段
                if (a1 == -1)
                {
                    end = m1 - 1;
                }
                else if (a1 == 1 && a2 == -1)
                {
                    // 在中間區段
                    start = m1 + 1;
                    end = m2 - 1;
                }
                else if (a2 == 1)
                {
                    // 在右邊區段
                    start = m2 + 1;
                }
            }
            return -1;
        }

        private static int guess(int n)
        {
            int pick = 6;
            pick = 2;
            pick = 1702766719;

            if (pick < n)
            {
                return -1;
            }
            else if (pick == n)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
