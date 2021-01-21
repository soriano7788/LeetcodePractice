using System;
using System.Collections.Generic;
using System.Text;

namespace ZigZagConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Input: s = "PAYPALISHIRING", numRows = 3
            Output: "PAHNAPLSIIGYIR"

            P   A   H   N
            A P L S I I G
            Y   I   R

            Input: s = "PAYPALISHIRING", numRows = 4
            Output: "PINALSIGYAHRPI"
            Explanation:

            P     I    N
            A   L S  I G
            Y A   H R
            P     I

            */

            // 直列數量為 n 的話， 斜列數量為 n-2
            // 以 1 直列 加上 1 斜列(斜列最右上方與 下一直列交接的元素空間不計算) 來計算所需的矩陣空間
            // 直列數量為 n 的話，每一直列加一斜列的空間 column 數量為 n-1，
            // 存放的元素數量為 直列(n) + 斜列(n-2) = 2n-2

            string str = "PAYPALISHIRING";   // 長度 14
            int numRows = 3;

            // str = "PAYPALISHIRING";
            // numRows = 4;


            // str = "ABCDE";
            // numRows = 4;

            Console.WriteLine(Convert2(str, numRows));

        }

        private static string Convert2(string s, int numRows)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            if (numRows == 1)
                return s;

            List<StringBuilder> lists = new List<StringBuilder>();

            // init
            for (int i = 0; i < numRows; i++)
            {
                lists.Add(new StringBuilder());
            }

            int baseNum = numRows - 1;
            int currentRowIndex = 0;
            string direction = "down";

            for (int i = 0; i < s.Length; i++)
            {
                int flag = i / baseNum;
                lists[currentRowIndex].Append(s[i]);

                // if (currentRowIndex == 0)
                // {
                //     direction = "down";
                // }
                // if (currentRowIndex == numRows - 1)
                // {
                //     direction = "up";
                // }

                // 用來判斷 目前是上升還是下降的方向
                // todo: 或許可以想得更單純
                // if (direction == "down")
                // {
                //     currentRowIndex++;
                // }
                // if (direction == "up")
                // {
                //     currentRowIndex--;
                // }


                if (flag % 2 == 0)
                {
                    currentRowIndex++;
                }
                else
                {
                    currentRowIndex--;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (StringBuilder list in lists)
            {
                Console.WriteLine("ANS: {0}", list.ToString());
                sb.Append(list.ToString());
            }

            return sb.ToString();
        }


        private static string Convert(string s, int numRows)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            if (numRows == 1)
                return s;

            CharNode head = new CharNode
            {
                Character = '\0',
                RowIndex = -1,
                ColIndex = -1,
                Previous = null,
                Next = null
            };

            // row 邊界檢查變數 0 ~ numRows-1
            int nowRow = 0,
                nowCol = 0;
            for (int i = 0; i < s.Length; i++)
            {
                // Console.WriteLine("head row: {0}, col: {1}", head.RowIndex, head.ColIndex);

                CharNode node = new CharNode
                {
                    Character = s[i],
                    RowIndex = nowRow,
                    ColIndex = nowCol
                };
                //Console.WriteLine("[ {0}, {1} ]", nowRow, nowCol);
                head.AddNode(node);
                //Console.WriteLine("nowStr: {0}", head.GetString());

                // matrix[nowRow, nowCol] = s[i];

                // 目前在直行
                if (nowCol % (numRows - 1) == 0)
                {
                    // 已觸底
                    if (nowRow == numRows - 1)
                    {
                        // 往右斜上移動
                        nowRow--;
                        nowCol++;
                    }
                    else
                    {
                        nowRow++;
                    }
                }
                else
                {
                    // 目前在斜行
                    nowRow--;
                    nowCol++;
                }
            }
            return head.GetString();
        }

        class CharNode
        {
            public int RowIndex;
            public int ColIndex;
            public char Character;
            public CharNode Previous;
            public CharNode Next;
            public void AddNode(CharNode node)
            {
                // 是也可以不管順序直接 add 到尾端就好，反正 rol col 的索引都已經有了
                // 假如要轉成二維陣列存的話，也可以找出需要的rold col 數，
                // 填入陣列後，再掃一次陣列，把字元挑出來就好
                // 只是目前是希望可以省空間複雜度，從 n^2 變成 n

                //Console.WriteLine("Add [ {0}, {1} ] = {2}", node.RowIndex, node.ColIndex, node.Character);

                CharNode current = this;

                // 假如是 head，先 next(一開始 head 是空的情況，第一次 add 跑 while 迴圈就會失敗)
                // if (current.Character == '\0')
                // {
                //     current = current.Next;
                // }

                while (current.Next != null)
                {
                    // 遇到同 row
                    if (node.RowIndex == current.RowIndex)
                    {
                        // 找出同 row 中，正確的 col 位置
                        while (node.ColIndex > current.ColIndex &&
                            node.RowIndex == current.RowIndex &&
                            current.Next != null)
                        {
                            current = current.Next;
                        }

                        if (node.RowIndex != current.RowIndex)
                        {
                            current.Previous.Next = node;
                            node.Previous = current.Previous;
                            node.Next = current;
                            current.Previous = node;

                            return;
                        }
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
                // 執行到這邊，表示 此 node 就是要接在最尾端
                current.Next = node;
                node.Previous = current;
            }

            public string GetString()
            {
                CharNode current = this;
                // 先回到 head
                while (current.Previous != null)
                {
                    current = current.Previous;
                }

                current = current.Next;

                string result = string.Empty;
                while (current != null)
                {
                    result += current.Character;
                    current = current.Next;
                }

                return result;
            }
        }
    }
}
