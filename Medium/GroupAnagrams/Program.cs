using System;
using System.Collections;
using System.Collections.Generic;

namespace GroupAnagrams
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/group-anagrams/

            // 找出 同字母異序詞
            // 各群 同字母異序詞 包進同一個 string list

            string[] strs = new string[] { "eat", "tea", "tan", "ate", "nat", "bat" };
            // output: [["bat"],["nat","tan"],["ate","eat","tea"]]

            strs = new string[] { "" };
            // output: [[""]]

            strs = new string[] { "a" };
            // output: [["a"]]

            IList<IList<string>> results = GroupAnagrams(strs);
            foreach (IList<string> result in results)
            {
                Console.WriteLine(string.Join(", ", result));
            }

        }
        private static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            IList<IList<string>> results = new List<IList<string>>();
            Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
            // 先整理進 dictionary
            foreach (string str in strs)
            {
                string key = GenerateKey(str);
                if (temp.ContainsKey(key))
                {
                    temp[key].Add(str);
                }
                else
                {
                    temp.Add(key, new List<string> { str });
                }
            }
            // 從 dictionary 取出，放進 lists
            foreach (var kvp in temp)
            {
                results.Add(kvp.Value);
            }
            return results;
        }
        private static string GenerateKey(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Sort(chars);
            string key = string.Join("", chars);
            return key;
        }
    }
}
