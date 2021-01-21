using System;
using System.Collections.Generic;

namespace _312._Burst_Balloons
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/burst-balloons/

            // 給予 n 個氣球
            // 每個氣球上面有數字，以 nums array 表示
            // 當你戳破第 i 個氣球，你可以獲得 nums[i-1] * nums[i] * nums[i+1] 枚硬幣
            // 假如 i-1 或 i+1 超出 array 邊界的話，就視為 1，
            // 假如 n-1 超界，就變成 1 * nums[i] * nums[i+1]

            // 條件設定
            // 0 <= nums[i] <= 100     // 有 0 存在!
            // 1 <= n <= 500     // 最少有 1 個氣球

            // output: 167
            int[] nums = new int[] { 3, 1, 5, 8 };

            // output: 10
            //nums = new int[] { 1, 5 };

            // output: 1654
            //nums = new int[] { 7, 9, 8, 0, 7, 1, 3, 5, 5, 2, 3 };

            //int result = MaxCoins(nums);
            //int result = MaxCoins2(nums);
            //int result = MaxCoins3(nums);
            int result = MaxCoinsMyVersion(nums);
            Console.WriteLine("result: {0}", result);
        }

        #region backtracking 暴力解，leetcode 會 timeout
        private static int MaxCoins(int[] nums)
        {
            // todo: 面試注意 ref out 修飾詞的差異
            // out 不需要初始值，但是傳入後，裡面一定要給他一個值，不然會爆
            // ref 一定要初始值

            // array pass by reference

            return Solve(new List<int>(nums), 0, Int32.MinValue);
        }
        private static int Solve(List<int> curNums, int curCoins, int maxCoins)
        {
            if(curNums.Count == 0)
            {
                return curCoins;
            }

            for(int i = 0; i < curNums.Count; i++)
            {

                int leftCoin = (i - 1 < 0) ? 1 : curNums[i - 1];
                int rightCoin = (i + 1 >= curNums.Count) ? 1 : curNums[i + 1];

                int tmpCoins = leftCoin * curNums[i] * rightCoin;

                List<int> tmp = new List<int>(curNums);
                tmp.RemoveAt(i);

                int result = Solve(tmp, curCoins + tmpCoins, maxCoins);
                maxCoins = Math.Max(maxCoins, result);
            }
            return maxCoins;
        }
        #endregion

        #region Divide & Conquer with Memoization
        private static int MaxCoins2(int[] nums)
        {
            // 需記錄在每個步驟狀態下的 最佳解，
            // 當紀錄不存在時，就要先解一遍，要保證這一次取得的解答就是最佳解

            // 最後一個戳破的氣球，獲得的 coin 一定是自己本身的數字，因為左右都沒氣球了

            // 把左右邊界多預留位置，這樣 之後 戳氣球 然後跟左右數字互乘時，
            // 就不用考慮左右邊是否已經出界
            int[] numsEX = new int[nums.Length + 2];
            int n = 1;
            foreach(int num in nums)
            {
                // 先排除掉 0，0 是最廢的，會讓一切化為烏有
                if(num > 0)
                {
                    numsEX[n++] = num;
                }
            }

            // 設定頭尾都是 1
            numsEX[0] = numsEX[n++] = 1;

            int[,] memo = new int[n, n];
            return Burst(memo, numsEX, 0, n - 1);

        }
        private static int Burst(int[,] memo, int[] nums, int left, int right)
        {
            // 假定 left right 實際上是空的位置，只是輔助用，
            // 當 left + 1 == right 成立
            // 表示實際上中間已經沒汽球了
            if (left + 1 == right)
            {
                return 0;
            }
            if(memo[left,right] > 0)
            {
                return memo[left, right];
            }
            int ans = 0;
            // i 相當於要戳爆的那個汽球的位置，
            // 所以一開始就先不考慮 最左 跟 最右??
            // 一開始就保留最左跟最右，一方面是為了讓一開始設定的公式成立 (爆破最後一個汽球，左右兩區塊已經先爆破了 這樣的公式設計)
            for (int i = left + 1; i < right; i++)
            {
                // 為啥 nums[i] 的左右會是 nums[left] 和 nums[right]?
                // 想到了!! 一開始就把最開頭和最結尾設為 1 了，這兩個位置實際上是超出邊界的位置，
                // 只不過是用來幫助讓計算簡化
                int whatTheFuck = 
                    nums[left] * nums[i] * nums[right] 
                    + Burst(memo, nums, left, i) 
                    + Burst(memo, nums, i, right);

                ans = Math.Max(ans, whatTheFuck);
            }

            memo[left, right] = ans;
            return ans;
        }
        #endregion

        private static int MaxCoins3(int[] nums)
        {
            int[] numsEX = new int[nums.Length+2];
            int n = 1;
            // 就這樣直接拿掉 0 也很奇怪欸
            // 0 還是要留著吧，為何這樣答案還會正確?
            foreach(int num in nums)
            {
                if (num > 0)
                {
                    numsEX[n++] = num;
                }
            }
            numsEX[0] = numsEX[n++] = 1;
            int[,] dp = new int[n, n];



            // K 應該是戳爆的氣球位置
            for (int k = 2; k < n; ++k)
            {
                // 這邊 left 是處理 被戳爆的氣球左半邊??
                for (int left = 0; left < n - k; ++left)
                {
                    int right = left + k;
                    for (int i = left + 1; i < right; ++i)
                    {
                        dp[left, right] = 
                            Math.Max(
                                dp[left, right], 
                                numsEX[left] * nums[i] * nums[right] + dp[left, i] + dp[i, right]);
                    }
                }
            }

            return dp[0, n - 1];
        }

        private static int MaxCoinsMyVersion(int[] nums)
        {
            // 先把 左右加 1
            // 先不把 0 排除掉
            int[] numsEX = new int[nums.Length + 2];
            for (int i = 1; i <= nums.Length; i++)
            {
                numsEX[i] = nums[i-1];
            }
            numsEX[0] = 1;
            numsEX[numsEX.Length - 1] = 1;

            //Console.WriteLine(string.Join(", ", numsEX));

            int n = numsEX.Length;
            int[,] dp = new int[n, n];

            int left = 1;
            int right = n - 2;
            return FuckingSolve(dp, numsEX, left, right);
        }
        private static int FuckingSolve(int[,] dp, int[] nums, int left, int right)
        {
            if (left == right)
            {
                return 0;
            }

            if (dp[left, right] != 0)
            {
                return dp[left, right];
            }

            int maxCoins = 0;
            for (int i = left; i <= right; i++)
            {

                int midBalloon = left * nums[i] * right;
                int leftNums = FuckingSolve(dp, nums, left, i);
                int rightNums = FuckingSolve(dp, nums, i, right);

                maxCoins = Math.Max(maxCoins, midBalloon + leftNums + rightNums);

            }
            return dp[left, right];
        }



    }
}
