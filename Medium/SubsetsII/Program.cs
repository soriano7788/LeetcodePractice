using System;
using System.Collections;
using System.Collections.Generic;

namespace SubsetsII
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/subsets-ii/
            int[] nums = new int[] { 1, 2, 2 };
            IList<IList<int>> results = SubsetsWithDup(nums);
            foreach (IList<int> result in results)
            {
                Console.WriteLine("[ {0} ]", string.Join(", ", result));
            }

        }
        private static IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            IList<IList<int>> results = new List<IList<int>>();
            Array.Sort(nums);
            Solve(nums, 0, results, new List<int>());
            return results;
        }
        private static void Solve(int[] nums, int start, IList<IList<int>> results, List<int> combination)
        {
            if (start > nums.Length)
            {
                return;
            }

            List<int> result = new List<int>(combination);
            results.Add(result);

            for (int i = start; i < nums.Length; i++)
            {
                // 主要避免在同一順位，出現重複的數字
                if (i > start && nums[i] == nums[i - 1])
                {
                    continue;
                }

                combination.Add(nums[i]);
                Solve(nums, i + 1, results, combination);
                combination.RemoveRange(combination.Count - 1, 1);
            }
        }
    }
}
