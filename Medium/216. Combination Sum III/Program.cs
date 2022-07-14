using System;
using System.Collections;
using System.Collections.Generic;

namespace CombinationSumIII
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/combination-sum-iii/

            // 從 1 ~ 9 中，找出 k 個數字，加總等於 n
            // 每個數字在一個子集合中，最多只能用一次
            int k = 3,
                n = 7;

            IList<IList<int>> results = CombinationSum3(k, n);

            foreach (IList<int> result in results)
            {
                Console.WriteLine("[ {0} ]", string.Join(", ", result));
            }


        }
        private static IList<IList<int>> CombinationSum3(int k, int n)
        {
            IList<IList<int>> results = new List<IList<int>>();
            Solve(k, n, 1, results, new List<int>());
            return results;
        }

        private static void Solve(int k, int n, int start, IList<IList<int>> results, List<int> combination)
        {
            if (combination.Count == k && Sum(combination) == n)
            {
                results.Add(new List<int>(combination));
                return;
            }
            if (start > 9)
            {
                return;
            }

            for (int i = start; i <= 9; i++)
            {
                combination.Add(i);
                Solve(k, n, i + 1, results, combination);
                combination.RemoveRange(combination.Count - 1, 1);
            }
        }
        private static int Sum(List<int> combination)
        {
            int sum = 0;
            foreach (int c in combination)
            {
                sum += c;
            }
            return sum;
        }
    }
}
