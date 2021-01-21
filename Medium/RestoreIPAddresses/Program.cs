#undef AAA
#define BBB
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace RestoreIPAddresses
{
    class Program
    {
        static void Main(string[] args)
        {
            #if AAA
            Console.WriteLine("AAA");
            #endif

            #if BBB
            Console.WriteLine("BBB");
            #endif

            // https://leetcode.com/problems/restore-ip-addresses/

            string s = "25525511135";
            // Output: ["255.255.11.135","255.255.111.35"]

            s = "0000";
            // Output: ["0.0.0.0"]

            s = "1111";
            // ["1.1.1.1"]

            s = "010010";
            // Output: ["0.10.0.10","0.100.1.0"]

            s = "101023";
            // Output: ["1.0.10.23","1.0.102.3","10.1.0.23","10.10.2.3","101.0.2.3"]


            IList<string> results = RestoreIpAddressesZ(s);
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
            // Debug.WriteLine("Debug Information");
            // Trace.WriteLine("Trace Information");

        }
        private static IList<string> RestoreIpAddressesZ(string s)
        {
            IList<string> results = new List<string>();
            Solve(s, results, new List<string>());
            return results;
        }

        private static void Solve(string s, IList<string> results, List<string> ip)
        {
            // 已蒐集 4 組數字
            if (ip.Count == 4)
            {
                // 字串仍有殘留，失敗
                if (s.Length > 0)
                {
                    return;
                }
                List<string> result = new List<string>(ip);
                // result.Reverse();
                results.Add(string.Join(".", result));
                return;
            }
            // 還沒集滿 4 組數字，但字串已經沒有殘留了，失敗
            if (s.Length == 0)
            {
                return;
            }

            // 嘗試切出 1 到 3 位數數字
            // 從字串最後面開始切
            for (int i = 1; i <= 3; i++)
            {
                if(s.Length < i)
                {
                    return;
                }
                string ipSector = s.Substring(0, i);
                if (IsValidIpNumber(ipSector))
                {
                    ip.Add(ipSector);
                    Solve(s.Substring(i), results, ip);
                    ip.RemoveRange(ip.Count - 1, 1);
                }
                else
                {
                    continue;
                }


                // if(s.Length - i < 0)
                // {
                //     return;
                // }
                // string ipSector = s.Substring(s.Length - i, i);
                // if (IsValidIpNumber(ipSector))
                // {
                //     ip.Add(ipSector);
                //     Solve(s.Substring(0, s.Length - i), results, ip);
                //     ip.RemoveRange(ip.Count - 1, 1);
                // }
                // else
                // {
                //     continue;
                // }
            }
        }
        private static bool IsValidIpNumber(string ip)
        {
            if (ip.Length > 3 || ip.Length == 0)
            {
                return false;
            }
            // not allow leading zero
            if (ip.Length > 1 && ip.StartsWith("0"))
            {
                return false;
            }
            int num;
            if (int.TryParse(ip, out num))
            {
                if (num > 255 || num < 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private static string ReverseString(string str)
        {
            char[] charArr = str.ToCharArray();
            Array.Reverse(charArr);
            return new string(charArr);
        }


    }
}
