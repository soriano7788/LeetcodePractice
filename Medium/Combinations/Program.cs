using System;
using System.Collections;
using System.Collections.Generic;

namespace Combinations
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/combinations/
            // k numbers from 1 ~ n
            // 就是 C n 取 k 的意思
            int n = 4,
                k = 2;

            // n = 1;
            // k = 1;

            IList<IList<int>> results = Combine(n, k);
            foreach (IList<int> result in results)
            {
                Console.WriteLine("[ {0} ]", string.Join(", ", result));
            }
        }
        private static IList<IList<int>> Combine(int n, int k)
        {
            IList<IList<int>> results = new List<IList<int>>();
            Solve(n, k, 1, results, new List<int>());
            return results;
        }
        private static void Solve(int n, int k, int start, IList<IList<int>> results, List<int> combination)
        {
            if (combination.Count == k)
            {
                results.Add(new List<int>(combination));
                return;
            }
            for (int i = start; i <= n; i++)
            {
                combination.Add(i);
                Solve(n, k, i + 1, results, combination);
                combination.RemoveRange(combination.Count - 1, 1);
            }
        }
    }
}
