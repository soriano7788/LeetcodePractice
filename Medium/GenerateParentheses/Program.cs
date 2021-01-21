using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateParentheses
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/generate-parentheses/
            // Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.

            // n=2 組合含 (), (())
            // ()()
            // (())
            // e.g. 2 等於 2+0, 1+1

            // n=3 組合含 (), (()), ((()))
            // ()()()
            // ((()))
            // (())()
            // ()(())
            // (()())
            // e.g. 3 等於 3+0, 2+1, 1+1+1

            // n=4 組合含 (), (()), ((())), (((())))
            // ()()()()
            // (((())))
            // (())(())
            // ()((()))
            // (())()()
            // ()(())()
            // ()()(())
            // ((()))()
            // ()((()))
            // e.g. 4 等於 4+0, 3+1, 2+2, 2+1+1, 1+1+1+1
            //Console.WriteLine("test: {0}", GenerateContainedParenthesisByCount(2));

            List<String> result = new List<string>();
            int n = 3;
            // GenerateAll(2 * n, 0, string.Empty, result);
            // Backtrack(result, string.Empty, 0, 0, n);
            result = MethodThree(n);
            foreach(string r in result)
            {
                Console.WriteLine(r);
            }

        }
        // todo: combination 改用 char[] 感覺上比較直覺?? 不會有 string 會去思考以為是 pass by reference 的問題
        // 用 array 直接指定 index 設定值蓋掉原本位置的值，不會有像 string 以為會一直串接的問題
        // 方法一: 暴力破解，直接產生 () 的所有排列組合 不論是否符合規定格式，之後再檢查格式是否正確來篩選出來
        private static void GenerateAll(int length, int currentIndex, string combination, List<string> result)
        {
            if(currentIndex == length)
            {
                if(IsValid(combination))
                {
                    result.Add(combination);
                }
                return;
            }
            GenerateAll(length, currentIndex + 1, combination + "(", result);
            GenerateAll(length, currentIndex + 1, combination + ")", result);
        }
        private static bool IsValid(string result)
        {
            int balance = 0;
            foreach (char c in result) {
                if (c == '(') 
                {
                    balance++;
                }
                else
                {
                    balance--;
                }

                // 因為從字串頭開始算，正常封閉的 括號 pair，balance 不會小於 0
                if (balance < 0)
                {
                    return false;
                }
            }
            return (balance == 0);

            // int open = 0,
            //     close = 0;
            // foreach(char c in result)
            // {
            //     if(c == '(')
            //     {
            //         open++;
            //     }
            //     if(c == ')')
            //     {
            //         close++;
            //     }
            // }
            // return (open == close);
        }

        // 方法二:
        private static void Backtrack(List<string> result, string combination, int openCount, int closeCount, int n)
        {
            if(combination.Length == (n * 2))
            {
                result.Add(combination);
                return;
            }
            // condition 思考模式 與方法一的 IsValid 的 balance 一樣?

            // open括弧的總數不可超過 n，一旦超過就一定是 invalid
            if(openCount < n)
            {
                Backtrack(result, combination + "(", openCount + 1, closeCount, n);
            }
            // 加 close 括弧的條件， close括弧的數量小於 open括弧的數量
            if(closeCount < openCount)
            {
                Backtrack(result, combination + ")", openCount, closeCount + 1, n);
            }
        }

        // 方法三:
        private static List<string> MethodThree(int n)
        {
            List<string> result = new List<string>();
            if(n == 0)
            {
                result.Add(string.Empty);
            }
            else
            {
                for(int i = 0; i < n; i++)
                {
                    foreach(string left in MethodThree(i))
                    {
                        foreach(string right in MethodThree(n - 1- i))
                        {
                            result.Add("(" + left + ")" + right);
                        }
                    }
                }
            }
            return result;
        }


        private static IList<string> GenerateParenthesis(int n) {
            List<string> result = new List<string>();
            for(int i = n; i <= 1; i--)
            {
                int k = i;
                while(k >= 0)
                {
                    // 不然就是要有 method，功能是算出 n 有幾個數字加總的排列組合
                }
            }

            return result;
        }
        private static string GenerateContainedParenthesisByCount(int n)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < n; i++)
            {
                sb.Append("(");
            }
            for(int i = 0; i < n; i++)
            {
                sb.Append(")");
            }
            return sb.ToString();
        }
        private static void RecurssiveMethod(int n)
        {}
        private static List<int> aaa(int n)
        {
            if(n == 1)
            {
                return new List<int> {1};
            }
            var z = aaa(n-1);
            var y = aaa(1);
            var x = new List<int>();
            x.AddRange(z);
            x.AddRange(y);
            return x;
        }
    }
}
