using System;
using System.Collections.Generic;

namespace _52._N_Queens_II
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/n-queens-ii/
            // 算出有幾種解

            int n = 4;
            int count = SolveNQueens(n);
            Console.WriteLine("count: {0}", count);
        }

        private static int SolveNQueens(int n)
        {
            // 初始化 棋盤
            int[][] grid = new int[n][];
            for (int i = 0; i < n; i++)
            {
                grid[i] = new int[n];
            }
            int count = solveNQueens(0, grid, 0);
            return count;
        }


        private static int solveNQueens(int row, int[][] grid, int count)
        {
            if (row == grid.Length)
            {
                return count + 1;
                // 產出問題要求的格式

            }
            else
            {
                // 嘗試每一個 colIndex
                for (int col = 0; col < grid[0].Length; col++)
                {
                    if (canPlace(row, col, grid))
                    {
                        grid[row][col] = 1;
                        count = solveNQueens(row + 1, grid, count);
                        grid[row][col] = 0;

                        // 其實不用管 grid 實際內容有沒有塞滿，只要某一個遞迴成功走到第 n row 就得到解了!!!
                    }
                }
                return count;
            }
        }

        // 檢查現在要放置的 row, col 是否可以放(就是不會讓 queen 之間可以互相攻擊)
        private static bool canPlace(int row, int col, int[][] grid)
        {
            // 這裡的判斷有沒有辦法寫得好讀一點?

            // i 是 row，j 是 col
            int i = 0, j = 0;
            // loop 從最上面的 row 往下，檢查有沒有在同一條直線正上方
            while (i != row)
            {
                if (grid[i++][col] == 1)
                    return false;
            }

            // 初始化為 新位置的45度左上方
            i = row - 1; j = col - 1;
            // 不斷往左上移動，看會不會撞到之前的 queen，當然出界的話就不用繼續 loop 了
            while (i >= 0 && j >= 0)
            {
                if (grid[i--][j--] == 1)
                    return false;
            }

            // 初始化為 新位置的45度右上方
            i = row - 1; j = col + 1;
            while (i >= 0 && j < grid[0].Length)
            {
                if (grid[i--][j++] == 1)
                    return false;
            }
            return true;
        }
    }
}
