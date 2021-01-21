using System;
using System.Collections.Generic;

namespace JumpGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/jump-game/

            // array 裡，每個 element 的數字代表的的是，可 jump 的最大距離
            // 例如，nums[0] 的數字為 2，
            // 表示可 jump 1~2 之間的距離，
            // 而不是一定要 jump 2 的意思

            int[] nums = new int[] { 2, 3, 1, 1, 4 };
            // true

            nums = new int[] { 3, 2, 1, 0, 4 };
            // false;

            Console.WriteLine(CanJump(nums));
        }






        private static bool CanJump(int[] nums)
        {
            // return Solve(nums, 0);

            // Index[] memo = GetMemorizationTable(nums);
            // return Solve2(nums, 0, memo);

            // // Approach 3: Dynamic Programming Bottom-up
            Index[] memo = GetMemorizationTable(nums);
            return memo[0] == Index.GOOD;
        }


        // 使用 backtrack 檢查每一種可能
        private static bool Solve(int[] nums, int index)
        {
            if (index == nums.Length - 1)
            {
                return true;
            }

            // 最大可 jump 的距離
            // todo: 注意，要是某個數字是超大數字，超過 array 的大小，
            // 那麼，會有很大部分的嘗試是多於浪費的，
            // 應該避免掉這種多餘的嘗試
            int maxJumpRange = nums[index];
            // jump 範圍不超過 array 範圍
            maxJumpRange = (index + maxJumpRange > nums.Length) ? (nums.Length - 1 - index) : maxJumpRange;

            for (int i = 1; i <= maxJumpRange; i++)
            {
                bool result = Solve(nums, index + i);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }


        // Approach 2: Dynamic Programming Top-down
        private static bool Solve2(int[] nums, int index, Index[] memo)
        {
            if (index == nums.Length - 1)
            {
                return true;
            }

            int maxJumpRange = nums[index];
            maxJumpRange = (index + maxJumpRange > nums.Length) ? (nums.Length - 1 - index) : maxJumpRange;
            for (int i = 1; i <= maxJumpRange; i++)
            {
                int nextIndex = index + i;
                if (memo[nextIndex] == Index.GOOD)
                {
                    return true;

                    // 只要知道現在可以到達 Good index，
                    // 就確定一定可以到達最後一個 index
                    // 下面這些不需要再跑一次

                    // bool result = Solve2(nums, index + i, memo);
                    // if (result)
                    // {
                    //     return true;
                    // }
                }
            }
            return false;
        }
        private static Index[] GetMemorizationTable(int[] nums)
        {
            // init memorization table
            Index[] memo = new Index[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                memo[i] = Index.UNKNOWN;
            }
            memo[memo.Length - 1] = Index.GOOD;

            List<int> goodIndices = new List<int>();
            goodIndices.Add(memo.Length - 1);

            for (int i = nums.Length - 2; i >= 0; i--)
            {
                int maxJumpRange = nums[i];
                if (IsGoodIndex(i, maxJumpRange, goodIndices))
                {
                    memo[i] = Index.GOOD;
                    goodIndices.Add(i);
                }
                else
                {
                    memo[i] = Index.BAD;
                }
            }
            return memo;
        }
        private static bool IsGoodIndex(int index, int jump, List<int> goodIndices)
        {
            // 檢查是否可到達目前已知的 good index
            foreach (int goodIndex in goodIndices)
            {
                if (index + jump >= goodIndex)
                {
                    return true;
                }
            }
            return false;
        }
        enum Index
        {
            GOOD,
            BAD,
            UNKNOWN
        }

        // Approach 3: Dynamic Programming Bottom-up
        // 只要 memo[0] 是 true，這題的結果就是 true




    }
}
