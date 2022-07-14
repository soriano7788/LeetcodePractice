using System;
using System.Collections.Generic;

namespace CombinationSumIV
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/combination-sum-iv/

            // 7
            int[] nums = new int[] { 1, 2, 3 };
            int target = 4;

            // 181997601
            nums = new int[] { 1, 2, 3 };
            target = 32;

            nums = new int[] { 3, 33, 3333 };
            target = 10000;

            nums = new int[] { 4, 5 };
            target = 11;

            nums = new int[] { 3, 33, 3333 };
            target = 1000;

            //int count = CombinationSum4(nums, target);
            //int count = CombinationSum4DpBottomUpSolution(nums, target);
            int count = CombinationSum4DpTopDownSolution(nums, target);
            //int count = LeetCodeDiscuss1(nums, target);
            //int count = LeetCodeDiscuss2(nums, target);

            Console.WriteLine("count: {0}", count);
        }

        #region 自行撰寫的 brute force，用 backtrack 嘗試各種組合，leetcode 上會超時
        private static int CombinationSum4(int[] nums, int target)
        {
            return Solve(nums, target, new List<int>(), 0, 0);
        }
        private static int Solve(int[] nums, int target, List<int> combination, int currentSum, int totalCount)
        {
            if (currentSum == target)
            {
                return totalCount + 1;
            }
            if (currentSum > target)
            {
                return totalCount;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                combination.Add(nums[i]);
                currentSum += nums[i];

                totalCount = Solve(nums, target, combination, currentSum, totalCount);

                combination.RemoveRange(combination.Count - 1, 1);
                currentSum -= nums[i];
            }

            return totalCount;
        }
        #endregion

        #region 自行撰寫的 DP 解，使用 Bottom-up
        private static int CombinationSum4DpBottomUpSolution(int[] nums, int target)
        {
            if (target == 0)
            {
                return 0;
            }
            // 初始化 dp table，預設值為 0
            int[] dp = new int[target + 1];
            Array.Fill(dp, 0);

            // 找出從 1 ~ target 的每個解
            // 也就是從子問題 target 為 1 的 case 開始，
            // 循序解到原問題 target
            for (int i = 1; i <= target; i++)
            {
                for (int k = 0; k < nums.Length; k++)
                {
                    // 當目前數字 nums[k] 剛好等於子問題 target 為 i 時的數字，
                    // 相當於加總排列組合只有一個數字 nums[k]，
                    // 正好是一組解，所以這邊 dp[i] 就直接加一
                    if (nums[k] == i)
                    {
                        dp[i]++;
                    }
                    // 確保當前數字 nums[k] 沒有比子問題的 target i 還大，造成索引變 0 或負數
                    if ((i - nums[k] > 0) )
                    {
                        // 組合的其中一個數字固定為 nums[k]，
                        // 剩餘的數字 i - nums[k]，
                        // 有幾種排列組合? 查 dp table
                        dp[i] += dp[i - nums[k]];
                    }
                }
            }
            return dp[target];
        }
        #endregion

        #region 自行撰寫的 DP 解，使用 Top-down (遇到 單一 num 大於 target 的 case 就壞了????)
        private static int CombinationSum4DpTopDownSolution(int[] nums, int target)
        {
            int[] dp = new int[target + 1];
            Array.Fill(dp, -1);
            dp[0] = 1;
            return SolveTopDown(nums, target, dp);
        }
        private static int SolveTopDown(int[] nums, int target, int[] dp)
        {
            //Console.WriteLine("extract from dp array. dp[{0}] = {1}", target, dp[target]);

            if (dp[target] != -1)
            {
                //Console.WriteLine("extract from dp array. dp[{0}] = {1}", target, dp[target]);
                return dp[target];
            }

            int res = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                // 確定子問題不要減到變負數
                if(target - nums[i] >= 0)
                {
                    // target 加總組合，剛好只有一個數字 nums[i] 的 case，所以 res 數加 1
                    if (target == nums[i])
                    {
                        res++;
                    }
                    else
                    {
                        res += SolveTopDown(nums, target - nums[i], dp);
                    }
                }
            }
            Console.WriteLine("set dp[{0}] = {1}", target, res);

            dp[target] = res;
            return dp[target];
        }
        #endregion

        #region leetcode 討論區 dp top-down
        private static int LeetCodeDiscuss1(int[] nums, int target)
        {
            int[] dp = new int[target + 1];
            // -1 用來表示 還沒處理過這個子問題
            Array.Fill(dp, -1);
            // dp[0] = 1 在這邊視為特殊存在，
            // 當下面的 target-nums[i] 為 0 時，
            // 剛好是加總組合只有一個數字的 case，
            // 所以刻意把 dp[0] 設為 1，
            // 確保回傳有一種組合的結果
            dp[0] = 1;
            return Helper(nums, target, dp);
        }
        private static int Helper(int[] nums, int target, int[] dp)
        {
            if(dp[target] != -1)
            {
                return dp[target];
            }
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if(nums[i] <= target)
                {
                    res += Helper(nums, target - nums[i], dp);
                }

            }

            dp[target] = res;
            return dp[target];
        }
        #endregion

        #region leetcode 討論區 dp bottom-up
        private static int LeetCodeDiscuss2(int[] nums, int target)
        {
            int[] dp = new int[target + 1];
            dp[0] = 1;
            for (int i = 1; i <= target; i++)
            {
                for (int j = 0; j < nums.Length; j++)
                {
                    if (i - nums[j] >= 0)
                    {
                        dp[i] += dp[i - nums[j]];
                    }
                }
            }
            return dp[target];
        }
        #endregion
    }
}
