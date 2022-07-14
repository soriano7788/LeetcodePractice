using System;
using System.Collections;
using System.Collections.Generic;

namespace PermutationsII
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] { 1, 1, 2 };
            IList<IList<int>> results = PermuteUnique(nums);
            foreach (IList<int> result in results)
            {
                Console.WriteLine(string.Join(",", result));
            }
        }
        private static IList<IList<int>> PermuteUnique(int[] nums)
        {
            IList<IList<int>> results = new List<IList<int>>();
            Solve(results, new List<int>(), new List<int>(), new HashSet<string>(), nums);
            return results;
        }
        private static void Solve(
            IList<IList<int>> results,
            List<int> currentCombination,
            List<int> currentCombinationIndex,
            HashSet<string> records,
            int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                // 用 index 來區別出是否為同一個 element 數字
                if (currentCombinationIndex.Contains(i))
                {
                    continue;
                }
                currentCombination.Add(nums[i]);
                currentCombinationIndex.Add(i);

                // 這裡就先記錄，因為可能會有重複數字，之後會無法以數字準確判斷出 index
                int removedIndex = currentCombination.Count - 1;

                if (currentCombination.Count == nums.Length)
                {
                    string key = GenerateKey(currentCombination);
                    if (!records.Contains(key))
                    {
                        records.Add(key);
                        results.Add(new List<int>(currentCombination));
                    }
                }
                else
                {
                    Solve(results, currentCombination, currentCombinationIndex, records, nums);
                }
                currentCombination.RemoveRange(removedIndex, currentCombination.Count - removedIndex);
                currentCombinationIndex.RemoveRange(removedIndex, currentCombinationIndex.Count - removedIndex);
            }
        }
        private static string GenerateKey(List<int> combination)
        {
            return string.Join(",", combination);
        }
    }
}
