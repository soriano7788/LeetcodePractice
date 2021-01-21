using System;
using System.Collections.Generic;
namespace LengthOfLongestSubstring
{
    class Program
    {
        static void Main(string[] args)
        {
            // 有 time complexity 為 n 的解

            string str = "pwwkew";
            //Console.WriteLine(LengthOfLongestSubstring(str));
            Console.WriteLine(SlidingWindowMethods2(str));
        }
        private static int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            HashSet<char> h = new HashSet<char>();
            int longestNotRepeatedStrLength = 1;
            for (int i = 0; i < s.Length - 1; i++)
            {
                // 可以判斷目前最大的 length，和目前檢查所在位置，決定是否還有需要繼續檢查剩下的部分
                if (s.Length - i < longestNotRepeatedStrLength)
                {
                    break;
                }

                h.Add(s[i]);
                string subStr = s.Substring(i + 1);
                for (int k = 0; k < subStr.Length; k++)
                {
                    if (h.Contains(subStr[k]))
                    {
                        break;
                    }
                    h.Add(subStr[k]);
                }
                if (h.Count > longestNotRepeatedStrLength)
                {
                    longestNotRepeatedStrLength = h.Count;
                }
                h.Clear();
            }
            return longestNotRepeatedStrLength;
        }
        private static int LengthOfLongestSubstring2(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            HashSet<char> h = new HashSet<char>();
            int longestNotRepeatedStrLength = 1;
            for (int i = 0; i < s.Length - 1; i++)
            {
                // 可以判斷目前最大的 length，和目前檢查所在位置，決定是否還有需要繼續檢查剩下的部分
                if (s.Length - i < longestNotRepeatedStrLength)
                {
                    break;
                }

                h.Add(s[i]);
                for (int k = i + 1; k < s.Length; k++)
                {
                    if (h.Contains(s[k]))
                    {
                        break;
                    }
                    h.Add(s[k]);
                }
                if (h.Count > longestNotRepeatedStrLength)
                {
                    longestNotRepeatedStrLength = h.Count;
                }
                h.Clear();
            }
            return longestNotRepeatedStrLength;
        }
        private static int SlidingWindowMethods(string s)
        {
            int n = s.Length;
            HashSet<char> hs = new HashSet<char>();
            int i = 0, j = 0;
            int maxLength = 0;
            while (i < n && j < n)
            {
                if (hs.Contains(s[j]))
                {
                    hs.Remove(s[i]);
                    i++;
                }
                else
                {
                    hs.Add(s[j]);
                    int nowLength = j - i + 1;
                    if (maxLength < nowLength)
                    {
                        maxLength = nowLength;
                    }
                    j++;
                }
            }
            return maxLength;
        }
        private static int SlidingWindowMethods2(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            int maxLength = 0;
            for (int i = 0, j = 0; j < s.Length; j++)
            {
                if (dic.ContainsKey(s[j]))
                {
                    i = dic[s[j]] + 1;
                    dic.Remove(s[j]);
                }
                dic.Add(s[j], j);
                if (maxLength < j - i + 1)
                {
                    maxLength = j - i + 1;
                }
            }
            return maxLength;
        }


        private static bool allUnique(string substring)
        {
            HashSet<char> hs = new HashSet<char>();
            foreach (char c in substring)
            {
                if (hs.Contains(c))
                {
                    return false;
                }
                hs.Add(c);
            }
            return true;
        }
    }
}
