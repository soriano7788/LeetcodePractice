using System;
using System.Collections;
using System.Collections.Generic;

namespace CombinationSumII
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] candidates = new int[] { 10, 1, 2, 7, 6, 1, 5 };
            int target = 8;

            candidates = new int[] { 2,5,2,1,2 };
            target = 5;

            IList<IList<int>> results = CombinationSum2(candidates, target);
            foreach (var result in results)
            {
                Console.WriteLine(string.Join(", ", result));
            }
        }
        private static IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            // 最終結果
            IList<IList<int>> results = new List<IList<int>>();
            // 記錄目前的組合數字
            List<int> combination = new List<int>();
            // 記錄目前的組合數字在 array 中的 index
            List<int> combinationIndices = new List<int>();
            // 記錄已確定的排列組合
            HashSet<string> records = new HashSet<string>();

            Solve(results, candidates, target, combination, combinationIndices, records);
            return results;
        }

        private static void Solve(
            IList<IList<int>> results,
            int[] candidates,
            int target,
            List<int> combination,
            List<int> combinationIndices,
            HashSet<string> records)
        {
            for (int i = 0; i < candidates.Length; i++)
            {
                if (combinationIndices.Contains(i))
                {
                    continue;
                }

                int nowTotal = Sum(combination) + candidates[i];
                if (nowTotal == target)
                {
                    // 這邊不能直接用 combination，要 new 一個
                    List<int> newCombination = new List<int>();
                    newCombination.AddRange(combination);
                    newCombination.Add(candidates[i]);
                    List<int> newCombinationIndices = new List<int>();
                    newCombinationIndices.AddRange(combinationIndices);
                    newCombinationIndices.Add(i);

                    string key = GetKey(newCombination);
                    if (!records.Contains(key))
                    {
                        results.Add(newCombination);
                        records.Add(key);
                    }
                }
                if (nowTotal < target)
                {
                    List<int> newCombination = new List<int>();
                    newCombination.AddRange(combination);
                    newCombination.Add(candidates[i]);
                    List<int> newCombinationIndices = new List<int>();
                    newCombinationIndices.AddRange(combinationIndices);
                    newCombinationIndices.Add(i);

                    Solve(results, candidates, target, newCombination, newCombinationIndices, records);
                }
            }
        }

        private static int Sum(List<int> combination)
        {
            int sum = 0;
            foreach (int n in combination)
            {
                sum += n;
            }
            return sum;
        }
        private static string GetKey(List<int> combination)
        {
            combination.Sort();
            return string.Join(",", combination);
        }

    }
}
