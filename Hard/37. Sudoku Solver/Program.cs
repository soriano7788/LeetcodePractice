using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/sudoku-solver/

            char[][] board = new char[][]
            {
                new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };

            ShowBoard(board);
            SolveSudoku(board);

            Console.WriteLine();
            ShowBoard(board);


        }
        private static void SolveSudoku(char[][] board)
        {
            // 蒐集 . 所在的 col 的數字
            // 蒐集 . 所在的 row 的數字
            // 蒐集 . 所在的 subbox 的數字
            // 看看剩下未出現的數字有哪些，假如只有一個的話就直接填入了
            Solve(board);
        }

        private static bool Solve(char[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int k = 0; k < board[i].Length; k++)
                {
                    char c = board[i][k];
                    // 發現空格了
                    if (c == '.')
                    {
                        List<int> possibleDigits = GetPossibleDigits(board, i, k);
                        foreach (int possibleDigit in possibleDigits)
                        {
                            board[i][k] = Convert.ToChar(possibleDigit.ToString());
                            if (Solve(board))
                            {
                                return true;
                            }
                            else
                            {
                                board[i][k] = '.';
                            }
                        }
                        return false;
                    }
                }
            }
            // 跑到這邊 表示沒發現任何 '.' ????
            return true;
        }

        private static int[] CollectRowDigits(char[][] board, int rowIndex)
        {
            List<int> digits = new List<int>();
            for (int i = 0; i < board[rowIndex].Length; i++)
            {
                char c = board[rowIndex][i];
                if (c != '.')
                {
                    digits.Add(Convert.ToInt32(c.ToString()));
                }
            }
            return digits.ToArray();
        }
        private static int[] CollectColDigits(char[][] board, int colIndex)
        {
            List<int> digits = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                char c = board[i][colIndex];
                if (c != '.')
                {
                    digits.Add(Convert.ToInt32(c.ToString()));
                }
            }
            return digits.ToArray();
        }
        private static int[] CollectSubboxDigits(char[][] board, int rowIndex, int colIndex)
        {
            List<int> digits = new List<int>();

            // 0~2
            // 3~5
            // 6~8

            // 將九個 subbox 用 0 ~ 2 表示

            // (2, 2) 除以 3, (0, 0)
            // (2, 3) 除以 3, (0, 1)
            // (5, 5) 除以 3, (1, 1)

            int boxRowIndex = rowIndex / 3;
            int boxColIndex = colIndex / 3;

            int startRow = boxRowIndex * 3,
                endRow = startRow + 2,
                startCol = boxColIndex * 3,
                endCol = startCol + 2; ;

            for (int i = startRow; i <= endRow; i++)
            {
                for (int k = startCol; k <= endCol; k++)
                {
                    char c = board[i][k];
                    if (c != '.')
                    {
                        digits.Add(Convert.ToInt32(c.ToString()));
                    }
                }
            }

            return digits.ToArray();
        }

        private static List<int> GetPossibleDigits(char[][] board, int rowIndex, int colIndex)
        {
            // 蒐集不可能可以填的數字
            int[] rowDigits = CollectRowDigits(board, rowIndex);
            int[] colDigits = CollectColDigits(board, colIndex);
            int[] subboxDigits = CollectSubboxDigits(board, rowIndex, colIndex);
            List<int> digits = new List<int>();
            digits.AddRange(rowDigits);
            digits.AddRange(colDigits);
            digits.AddRange(subboxDigits);

            digits = digits.Distinct().ToList();
            if (digits.Count == 9)
            {
                return new List<int>();
            }

            List<int> possibleDigits = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                if (!digits.Contains(i))
                {
                    possibleDigits.Add(i);
                }
            }

            return possibleDigits;
        }


        private static void ShowBoard(char[][] board)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board.Length; i++)
            {
                Console.WriteLine(string.Join(" ", board[i]));
                // for (int k = 0; k < board[i].Length; k++)
                // {
                //     sb.Append(board[i][k]);
                // }
                // sb.Append("\n");
            }
            // Console.WriteLine(sb.ToString());
        }
    }
}
