using System;
using System.Collections;
using System.Collections.Generic;

namespace LetterCombinationsOfAPhoneNumber
{
    class Program
    {
        static Dictionary<string,string> mapping = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            mapping.Add("2", "abc");
            mapping.Add("3", "def");
            mapping.Add("4", "ghi");
            mapping.Add("5", "jkl");
            mapping.Add("6", "mno");
            mapping.Add("7", "pqrs");
            mapping.Add("8", "tuv");
            mapping.Add("9", "wxyz");
            // https://leetcode.com/problems/letter-combinations-of-a-phone-number/
            // 1: 沒字母
            // 2: abc
            // 3: def
            // 4: ghi
            // 5: jkl
            // 6: mno
            // 7: pqrs
            // 8: tuv
            // 9: wxyz

            string digits = "234";
            IList<string> combinationResult = LetterCombinations(digits);
            foreach(string cr in combinationResult)
            {
                Console.WriteLine(cr);
            }

        }

        private static IList<string> LetterCombinations(string digits)
        {
            string filterDigits = FilterDigits(digits);
            List<string> combinationResult = new List<string>();
            if(string.IsNullOrEmpty(filterDigits))
            {
                return combinationResult;
            }

            string combination = string.Empty;
            BackTrack(filterDigits, combination, combinationResult);

            return combinationResult;
        }
        private static string FilterDigits(string digits)
        {
            string filterDigits = string.Empty;
            foreach(char c in digits)
            {
                int result;
                if(int.TryParse(c.ToString(), out result))
                {
                    if(result == 1)
                    {
                        continue;
                    }
                    filterDigits += c;
                }
            }
            return filterDigits;
        }
        private static void BackTrack(string digits, string combination, List<string> combinationResult)
        {
            if(digits.Length == 0)
            {
                combinationResult.Add(combination);
                return;
            }
            string digit = digits.Substring(0, 1);
            string alphabets = mapping[digit];
            for(int i = 0; i < alphabets.Length; i++)
            {
                BackTrack(digits.Substring(1), combination + alphabets[i], combinationResult);
            }
        }

    }
}
