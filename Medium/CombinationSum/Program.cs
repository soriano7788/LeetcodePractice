using System;
using System.Collections;
using System.Collections.Generic;

namespace CombinationSum
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/combination-sum/
            int[] candidates = new int[] { 2, 3, 6, 7 };
            int target = 7;

            // candidates = new int[] { 2, 3, 5 };
            // target = 8;

            candidates = new int[] { 8, 7, 4, 3 };
            target = 11;

            IList<IList<int>> results = CombinationSum(candidates, target);
            foreach (var result in results)
            {
                Console.WriteLine(string.Join(", ", result));
            }
        }
        private static IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            // Array.Sort(candidates);
            IList<IList<int>> results = new List<IList<int>>();
            List<int> combination = new List<int>();
            HashSet<string> records = new HashSet<string>();

            Solve(results, candidates, target, combination, records);
            return results;
        }

        private static void Solve(IList<IList<int>> results, int[] candidates, int target, List<int> combination, HashSet<string> records)
        {
            foreach (int candidate in candidates)
            {
                int nowTotal = Sum(combination) + candidate;
                if (nowTotal == target)
                {
                    // 這邊不能直接用 combination，要 new 一個
                    List<int> newCombination = new List<int>();
                    newCombination.AddRange(combination);
                    newCombination.Add(candidate);

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
                    newCombination.Add(candidate);
                    Solve(results, candidates, target, newCombination, records);
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
