using System;
using System.Collections;
using System.Collections.Generic;

namespace PerfectSquares
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/perfect-squares/
            // 找出需要數字數量最少的集合，數字可重複
            // 數字是 perfect square
            int n = 12;
            //n = 13;
            //n = 40;
            int num = NumSquares(n);
            Console.WriteLine("num: {0}", num);
        }

        private static int NumSquares(int n)
        {


            // 先蒐集可用的 perfect square
            //List<int> candidates = new List<int>();
            //int i = 1;
            //while (i * i <= n)
            //{
            //    candidates.Add(i * i);
            //    i++;
            //}

            //return Solve(n, candidates.ToArray(), Int32.MaxValue, new List<int>());
            //return Solve3(n, candidates.ToArray(), Int32.MaxValue, new List<int>());



            // dp[i] 表示 i 的 perfect square count，總之 dp[n] 表示 input 為 n 的解
            // dp[1] = 1，組合為 [1]
            // dp[2] = 1，組合為 [4]
            // dp[3] = 3，組合[1,1,1]
            // dp[4] = 1，組合[4]
            // dp[5] = 2，組合[4, 1]

            int[] dp = new int[n + 1];
            // 把陣列的每個元素都先填入 Int32.MaxValue，(這行應該是不需要)
            //Array.Fill(dp, Int32.MaxValue);
            // 0 沒有任何 perefect square 數字的加總組合為 0
            dp[0] = 0;

            // 找出 1 ~ n 所有數字的 perfect square 加總組合 最少數量的數字
            // O(n)
            for (int i = 1; i <= n; i++)
            {
                // 初始先設一個最大值為 min，其實最大值也就 i 數字本身而已(組成全部都是 1 相加的 case)
                int min = Int32.MaxValue;
                int j = 1;
                // O(更號 i)
                while (i - j * j >= 0)
                {
                    // 最小值是某個前面數字的的解加 1，這裡的 1 是代表某個 dp[j * j]，也就是平方數，
                    // 所以假如把 1 換成 dp[j * j] 是不是會比較容易理解?
                    // 承上行，答案是不可，一開始要加上 dp[1*1] 就會出事，因為陣列元素預設值會影響，不是 0 就是 Int32.MaxValue
                    min = Math.Min(min, dp[i - j * j] + 1);
                    j++;
                }
                dp[i] = min;
            }
            return dp[n];
            // 上面的解其實也像是暴力破解，
            // 只是前面先處理好的較小數字的解，
            // 可以用來加快後面未處理的較大數字的解
            // 但是前幾個數字是如何思考出這個解的???


            // 如何切成小問題???
            // 1 ~ n 各個的 perfect square 數字數量???
            // 1 ~ n 之間的數字 i，最小的 perefect square 加總數量為 1，
            // e.g. 1 4 9 16 25 36 49 依此類推
            // 使用 貪婪法，從最大的數字先試，可以馬上找出最小的數字嗎???
            //Solve2(n, candidates.ToArray(), Int32.MaxValue, new List<int>(), dp);


        }

        /// <summary>
        /// 這是 brute force 方法
        /// </summary>
        /// <param name="n"></param>
        /// <param name="candidates"></param>
        /// <param name="minCount"></param>
        /// <param name="currentCombination"></param>
        /// <returns></returns>
        private static int Solve(int n, int[] candidates, int minCount, List<int> currentCombination)
        {
            if (Sum(currentCombination) > n)
            {
                return minCount;
            }
            if (Sum(currentCombination) == n)
            {
                //Console.WriteLine("match: {0}", string.Join(",", currentCombination));
                return Math.Min(currentCombination.Count, minCount);
            }

            for (int i = 0; i < candidates.Length; i++)
            {
                currentCombination.Add(candidates[i]);
                int localMin = Solve(n, candidates, minCount, currentCombination);
                minCount = Math.Min(minCount, localMin);
                currentCombination.RemoveRange(currentCombination.Count - 1, 1);
            }
            return minCount;
        }
        private static int Sum(List<int> combination)
        {
            int sum = 0;
            foreach (int num in combination)
            {
                sum += num;
            }
            return sum;
        }

        // dp 解
        private static int Solve2(int n, int[] candidates, int minCount, List<int> currentCombination, int[] dp)
        {
            return 0;
        }
        // greedy method，實作失敗
        private static int Solve3(int n, int[] candidates, int minCount, List<int> currentCombination)
        {
            if(Sum(currentCombination) > n)
            {
                return minCount;
            }
            if(Sum(currentCombination) == n)
            {
                Console.WriteLine("zzz: {0}", string.Join(", ", currentCombination));
                return Math.Min(minCount, currentCombination.Count);
            }

            for (int i = candidates.Length - 1; i >= 0; i--)
            {
                currentCombination.Add(candidates[i]);
                int localMin = Solve3(n, candidates, minCount, currentCombination);
                if(localMin != Int32.MaxValue)
                {
                    return localMin;
                }
                minCount = Math.Max(minCount, localMin);
                currentCombination.RemoveRange(currentCombination.Count - 1, 1);
            }
            return minCount;
        }

        // 自行練習撰寫
        private static int NumSquares2(int n)
        {
            int[] dp = new int[n + 1];
            for(int i = 1; i <= n; i++)
            {
                int min = i;
                int j = 1;
                while (i - j * j >= 0)
                {
                    min = Math.Min(min, dp[i - j * j] + 1);
                    j++;
                }
                dp[i] = min;
            }
            return dp[n];
        }


    }
}
