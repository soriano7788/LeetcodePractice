using System;
using System.Collections;
using System.Collections.Generic;

namespace Subsets
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/subsets/
            int[] nums = new int[] { 1, 2, 3 };
            // nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 10, 0 };

            // IList<IList<int>> results = Subsets(nums);
            IList<IList<int>> results = Approach1(nums);
            foreach (IList<int> result in results)
            {
                Console.WriteLine("[ {0} ]", string.Join(", ", result));
            }
        }
        private static IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> results = new List<IList<int>>();
            Solve(nums, 0, results, new List<int>());
            return results;
        }

        private static void Solve(int[] nums, int start, IList<IList<int>> results, List<int> combination)
        {
            if (start > nums.Length)
            {
                return;
            }

            var result = new List<int>(combination);
            results.Add(result);

            for (int i = start; i < nums.Length; i++)
            {
                if (combination.Contains(nums[i]))
                {
                    continue;
                }
                combination.Add(nums[i]);
                Solve(nums, i + 1, results, combination);
                combination.RemoveRange(combination.Count - 1, 1);
            }
        }

        private static IList<IList<int>> Approach1(int[] nums)
        {
            IList<IList<int>> results = new List<IList<int>>();
            results.Add(new List<int>());

            foreach (int num in nums)
            {
                // 型態跟 results 一樣
                IList<IList<int>> newSubsets = new List<IList<int>>();
                // loop results，results 最開始是空的
                // 等於是從 result 撈已有資料出來，
                // 已有資料加一個數字，變成新的一筆資料加回 result
                foreach (IList<int> curr in results)
                {
                    // 從 results 撈一筆資料，
                    List<int> tmp = new List<int>(curr);
                    // 加數字進去
                    tmp.Add(num);
                    // 變成一個新的子集合
                    newSubsets.Add(tmp);
                }
                foreach (List<int> curr in newSubsets)
                {
                    results.Add(curr);
                }
            }

            return results;
        }

        private static IList<IList<int>> Approach2(int[] nums)
        {}


    }
}
