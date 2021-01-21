using System;
using System.Text;

namespace StringToInteger_atoi_
{
    class Program
    {
        static void Main(string[] args)
        {
            // 去掉字串中的空白
            // 接著，開頭只能是 + - 號或數字，假如是其他字元的話就是無效，回傳 0
            // 只接受 32 bit 的有號整數，超過上下限的話，就回傳上下限就好
            string input = "A B C D E";
            input = "-2147483647";

            Console.WriteLine(MyAtoi(input));

        }
        private static int MyAtoi(string str)
        {
            // remove space
            str = str.Trim();

            if (str.Length > 0)
            {
                // 開頭非數字或 + -
                if (!IsValidStartedCharacter(str[0]))
                {
                    return 0;
                }

                bool startWithMinus = false;
                // 開頭是 + - 符號
                if (IsSignedCharacter(str[0]))
                {
                    // 開頭是 - 符號
                    if (str[0] == '-')
                    {
                        startWithMinus = true;
                    }
                    // 去掉開頭 + 或 - 符號
                    str = str.Substring(1);
                    if (str.Length == 0)
                    {
                        return 0;
                    }
                }

                // remove leading zero
                int leadingZeroCount = 0;
                foreach (char c in str)
                {
                    if (c == '0')
                    {
                        leadingZeroCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                str = str.Substring(leadingZeroCount);
                if (str.Length == 0)
                {
                    return 0;
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < str.Length; i++)
                {
                    if (!IsNumbericalCharacter(str[i]))
                    {
                        break;
                    }
                    sb.Append(str[i]);
                }

                str = sb.ToString();
                if (string.IsNullOrEmpty(str))
                {
                    return 0;
                }

                if (startWithMinus)
                {
                    //負數
                    str = "-" + str;
                    return FilterNegativeNumber(str);
                }
                else
                {
                    // 正數
                    return FilterPositiveNumber(str);
                }
            }

            return 0;
        }

        private static bool IsValidStartedCharacter(char c)
        {
            return IsSignedCharacter(c) || IsNumbericalCharacter(c);
        }
        private static bool IsSignedCharacter(char c)
        {
            // 字串 + 為 43
            // 字串 - 為 45
            int num = (int)c;
            return (num == 43) || (num == 45);
        }
        private static bool IsNumbericalCharacter(char c)
        {
            // 字串 0~9 轉成編碼為數字 48~57
            int num = (int)c;
            return num >= 48 && num <= 57;
        }

        private static int FilterPositiveNumber(string str)
        {
            string maxNumStr = Int32.MaxValue.ToString();
            if (str.Length > maxNumStr.Length)
            {
                return Int32.MaxValue;
            }
            if (str.Length < maxNumStr.Length)
            {
                return Convert.ToInt32(str);
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert.ToInt32(str[i]) < Convert.ToInt32(maxNumStr[i]))
                {
                    return Convert.ToInt32(str);
                }
                if (Convert.ToInt32(str[i]) > Convert.ToInt32(maxNumStr[i]))
                {
                    return Int32.MaxValue;

                }
                if (Convert.ToInt32(str[i]) == Convert.ToInt32(maxNumStr[i]))
                {
                    continue;
                }
            }
            return Int32.MaxValue;
        }
        private static int FilterNegativeNumber(string str)
        {
            string minNumStr = Int32.MinValue.ToString();
            minNumStr = minNumStr.Substring(1);
            str = str.Substring(1);

            if (str.Length > minNumStr.Length)
            {
                return Int32.MinValue;
            }
            if (str.Length < minNumStr.Length)
            {
                return Convert.ToInt32("-" + str);
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert.ToInt32(str[i]) < Convert.ToInt32(minNumStr[i]))
                {
                    return Convert.ToInt32("-" + str);
                }
                if (Convert.ToInt32(str[i]) > Convert.ToInt32(minNumStr[i]))
                {
                    return Int32.MinValue;

                }
                if (Convert.ToInt32(str[i]) == Convert.ToInt32(minNumStr[i]))
                {
                    continue;
                }
            }
            return Int32.MinValue;
        }
    }
}
