using System;
using System.Collections;
using System.Collections.Generic;

namespace Permutations
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/permutations/
            
            int[] nums = new int[] { 1, 2, 3 };
            IList<IList<int>> results = Permute(nums);
            foreach (IList<int> result in results)
            {
                Console.WriteLine(string.Join(", ", result));
            }

        }
        private static IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> results = new List<IList<int>>();
            Solve(results, new List<int>(), nums);
            return results;
        }
        private static void Solve(IList<IList<int>> results, List<int> combination, int[] nums)
        {
            foreach (int num in nums)
            {
                if (combination.Contains(num))
                {
                    continue;
                }
                // 用 new 的，或是下一行再減回來
                combination.Add(num);
                if (combination.Count != nums.Length)
                {
                    Solve(results, combination, nums);
                }
                else
                {
                    List<int> resultCombination = new List<int>();
                    resultCombination.AddRange(combination);
                    results.Add(/*resultCombination*/new List<int>(combination));
                }
                int removedIndex = combination.IndexOf(num);
                combination.RemoveRange(removedIndex, combination.Count - removedIndex);
            }
        }
    }
}
