using System;

namespace LongestPalindromicSubstring
{
    class Program
    {
        static void Main(string[] args)
        {
            // Given a string s, find the longest palindromic substring in s. 
            // You may assume that the maximum length of s is 1000.
            string str = "babad";
            //str = "cbbd";
            //str = "a";
            str = "bandana";

            Console.WriteLine(LongestPalindrome(str));
            Console.WriteLine(FindFromCenter(str));
        }
        private static string LongestPalindrome(string s)
        {
            // 檢查各種可能的回文長度，從最大長度開始遞減檢查
            for (int i = s.Length - 1; i >= 0; i--)    // O(n)
            {
                int leftIndex = 0,
                    rightIndex = i;

                while (rightIndex <= s.Length - 1)   // O(n)
                {
                    if (IsPalindrome(s, leftIndex, rightIndex))
                    {
                        return s.Substring(leftIndex, rightIndex - leftIndex + 1);
                    }
                    leftIndex++;
                    rightIndex++;
                }
            }
            return string.Empty;
        }

        private static string FindFromCenter(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            if(s.Length == 1)
            {
                return s;
            }

            int ansLeftIndex = 0,
                ansRightIndex = 0,
                maxLength = 0;

            // todo: 奇偶數處理法 可以直接合併，
            // 奇數的話，左右起始點會在同一位置
            // 偶數的話，左右起始點相鄰

            // 回文長度是奇數的 case
            for (int i = 0; i < s.Length; i++)
            {
                int nowLeftIndex = i,
                    nowRightIndex = i;
                while (nowLeftIndex >= 0 && nowRightIndex <= s.Length - 1)
                {
                    if (s[nowLeftIndex] == s[nowRightIndex])
                    {
                        int nowLength = (nowRightIndex) - (nowLeftIndex) + 1;
                        if (maxLength < nowLength)
                        {
                            maxLength = nowLength;
                            ansLeftIndex = nowLeftIndex;
                            ansRightIndex = nowRightIndex;
                        }
                        nowLeftIndex--;
                        nowRightIndex++;
                    }
                    else
                    {
                        break;
                    }

                }
            }

            // 回文長度是偶數的 case
            for (int i = 0, j = 1; j <= s.Length; i++, j++)
            {
                int nowLeftIndex = i,
                    nowRightIndex = j;
                while (nowLeftIndex >= 0 && nowRightIndex <= s.Length - 1)
                {
                    if (s[nowLeftIndex] == s[nowRightIndex])
                    {
                        int nowLength = (nowRightIndex) - (nowLeftIndex) + 1;
                        if (maxLength < nowLength)
                        {
                            maxLength = nowRightIndex - nowLeftIndex + 1;
                            ansLeftIndex = nowLeftIndex;
                            ansRightIndex = nowRightIndex;
                        }
                        nowLeftIndex--;
                        nowRightIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return s.Substring(ansLeftIndex, ansRightIndex - ansLeftIndex + 1);
        }


        private static bool IsPalindrome(string s, int leftIndex, int rightIndex)   // O(n)
        {
            while (leftIndex <= rightIndex)
            {
                if (s[leftIndex] != s[rightIndex])
                {
                    return false;
                }
                leftIndex++;
                rightIndex--;
            }
            return true;
        }
    }
}
