using System;

namespace ClimbingStairs
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 4;

            int wayCount = ClimbStairs(n);
            Console.WriteLine("wayCount: {0}", wayCount);
        }
        private static int ClimbStairs(int n)
        {
            // int wayCount = 0;
            // Solve(n, 0, ref wayCount);
            // return wayCount;

            // return Solve2(n, 0);

            int[] memo = new int[n + 1];
            int ans = Solve3(0, n, memo);
            for (int i = 0; i < memo.Length; i++)
            {
                Console.WriteLine("memo[{0}]: {1}", i, memo[i]);
            }
            return ans;

            // return Solve4(n);

        }

        // 暴力解法
        private static void Solve(int n, int curStepCount, ref int wayCount)
        {
            if (curStepCount == n)
            {
                wayCount++;
                return;
            }
            if (curStepCount > n)
            {
                return;
            }

            Solve(n, curStepCount + 1, ref wayCount);
            Solve(n, curStepCount + 2, ref wayCount);
        }

        // 暴力解法
        private static int Solve2(int n, int curStepCount)
        {
            if (curStepCount == n)
            {
                return 1;
            }
            if (curStepCount > n)
            {
                return 0;
            }

            return Solve2(n, curStepCount + 1) + Solve2(n, curStepCount + 2);
        }

        // 加上 memory 輔助加速，先用小樣本幫助思考看看
        private static int Solve3(int curStepCount, int n, int[] memo)
        {
            // 執行完後，印出 memo 看看...

            // n = 3 時
            // 111
            // 12
            // 21

            // n = 4 時
            // 1111
            // 112
            // 121
            // 211
            // 22


            if (curStepCount > n)
            {
                return 0;
            }
            if (curStepCount == n)
            {
                return 1;
            }
            if (memo[curStepCount] > 0)
            {
                return memo[curStepCount];
            }

            // 紀錄走 curStepCount stair 的步法有幾種
            // 索引為 stair 數量，value 為方法數
            memo[curStepCount] = Solve3(curStepCount + 1, n, memo) + Solve3(curStepCount + 2, n, memo);
            // if n = 4，recursive stack 由下往上解
            // memo[0] = solve(1) + solve(2) = ? + ? = ?     3+2=5
            // memo[1] = solve(2) + solve(3) = ? + ? = ?     2+1=3
            // memo[2] = solve(3) + solve(4) = ? + ? = ?     1+1=2
            // memo[3] = solve(4) + solve(5) = 1 + 0 = 1
            // (X 不會執行到這裡???)memo[4] = solve(5) + solve(6) = 
            return memo[curStepCount];
        }

        // dynamic programming，看起來就直接用費伯納西數列算阿??
        private static int Solve4(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            int[] dp = new int[n + 1];
            dp[1] = 1;
            dp[2] = 2;
            for (int i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
            return dp[n];
        }


    }
}
