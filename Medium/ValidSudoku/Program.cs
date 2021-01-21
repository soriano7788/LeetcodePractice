using System;
using System.Collections.Generic;

namespace ValidSudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/valid-sudoku/
            // 字元必需是 0~9 或 .


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

            board = new char[][]
            {
                new char[] {'8', '3', '.', '.', '7', '.', '.', '.', '.'},
                new char[] {'6', '.', '.', '1', '9', '5', '.', '.', '.'},
                new char[] {'.', '9', '8', '.', '.', '.', '.', '6', '.'},
                new char[] {'8', '.', '.', '.', '6', '.', '.', '.', '3'},
                new char[] {'4', '.', '.', '8', '.', '3', '.', '.', '1'},
                new char[] {'7', '.', '.', '.', '2', '.', '.', '.', '6'},
                new char[] {'.', '6', '.', '.', '.', '.', '2', '8', '.'},
                new char[] {'.', '.', '.', '4', '1', '9', '.', '.', '5'},
                new char[] {'.', '.', '.', '.', '8', '.', '.', '7', '9'}
            };

            Console.WriteLine("IsValidSudoku: {0}", IsValidSudoku(board));


        }
        private static bool IsValidSudoku(char[][] board)
        {
            // check subboxes，同時也檢查過是否有 1~9 以外的數字，和.以外的字元
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int startCol = k * 3,
                        endCol = k * 3 + 2,
                        startRow = i * 3,
                        endRow = i * 3 + 2;

                    if (!IsValidSubBox(board, startCol, endCol, startRow, endRow))
                    {
                        return false;
                    }
                }
            }

            // check rows
            for (int i = 0; i < 9; i++)
            {
                HashSet<char> hs = new HashSet<char>();
                for (int k = 0; k < 9; k++)
                {
                    char c = board[i][k];
                    if (IsDotCharacter(c))
                    {
                        continue;
                    }
                    if (hs.Contains(c))
                    {
                        return false;
                    }
                    hs.Add(c);
                }
                hs.Clear();
            }
            // check cols
            for (int i = 0; i < 9; i++)
            {
                HashSet<char> hs = new HashSet<char>();
                for (int k = 0; k < 9; k++)
                {
                    char c = board[k][i];
                    if (IsDotCharacter(c))
                    {
                        continue;
                    }
                    if (hs.Contains(c))
                    {
                        return false;
                    }
                    hs.Add(c);
                }
                hs.Clear();
            }

            return true;
        }
        private static bool IsValidSubBox(char[][] board, int startCol, int endCol, int startRow, int endRow)
        {
            HashSet<char> hs = new HashSet<char>();
            for (int i = startRow; i <= endRow; i++)
            {
                for (int k = startCol; k <= endCol; k++)
                {
                    char c = board[i][k];
                    if (IsDotCharacter(c))
                    {
                        continue;
                    }

                    if (IsValidDigit(c))
                    {
                        if (hs.Contains(c))
                        {
                            return false;
                        }
                        else
                        {
                            hs.Add(c);
                        }
                    }
                }
            }
            return true;
        }
        private static bool IsValidCharacter(char c)
        {
            char[] validCharacters = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            if (IsValidCharacter(c) || IsDotCharacter(c))
            {
                return true;
            }
            return false;
        }
        private static bool IsValidDigit(char c)
        {
            int result;
            if (int.TryParse(c.ToString(), out result) && (result > 0 && result < 10))
            {
                return true;
            }
            return false;
        }
        private static bool IsDotCharacter(char c)
        {
            return c == '.';
        }

    }
}
