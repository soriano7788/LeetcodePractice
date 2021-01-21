using System;

namespace _64._Minimum_Path_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/minimum-path-sum/
            // 一個二維矩陣
            // 起點左上角，終點右下角
            // 只能往右或往下移動
            // 找出從起點到終點，路徑上數字總和最小的路徑

            // output: 7
            int[][] grid = new int[][]
            {
                new int[] { 1, 3, 1},
                new int[] { 1, 5, 1},
                new int[] { 4, 2, 1}
            };

            // output: 12
            grid = new int[][]
            {
                new int[] { 1, 2, 3},
                new int[] { 4, 5, 6}
            };

            // output: 1
            grid = new int[][]
            {
                new int[] { 1 }
            };

            //int result = MinPathSum(grid);
            int result = MinPathSum3(grid);
            Console.WriteLine("result: {0}", result);
        }

        private static int MinPathSum(int[][] grid)
        {
            if(grid.Length == 0)
            {
                return 0;
            }

            if(grid.Length == 1)
            {
                // 只有一維陣列，直接加總就好，因為也就只能從起點一直往右走
                int sum = 0;
                for (int i = 0; i < grid[0].Length; i++)
                {
                    sum += grid[0][i];
                }
                return sum;
            }

            int rowCount = grid.Length;
            int colCount = grid[0].Length;

            // dp[i,j] 用來記錄從起點 [0,0] 到 [i,j] 的最短路徑
            int[,] dp = new int[rowCount, colCount];

            // 
            // dp[i,j] = Min( dp[i - 1, j] , dp[i, j - 1] ) + grid[i, j]
            // if dp[x,y] == 0，就繼續往前找，不等於 0 的話，就可以直接回傳 dp[x,y]

            Solve(grid, dp, rowCount - 1, colCount - 1);

            return dp[rowCount - 1, colCount - 1];
        }

        // 這是從 結尾往前推的作法
        private static int Solve(int[][] grid, int[,] dp, int curRow, int curCol)
        {
            // 不可低於起點

            // 已追朔到起點需終止
            if(curRow == 0 && curCol == 0)
            {
                return grid[0][0];
            }

            if (curRow < 0)
            {
                return Int32.MaxValue;
            }
            if (curCol < 0)
            {
                return Int32.MaxValue;
            }


            // 已抵達終點
            //if (curRow == rowCount - 1 && curCol == colCount - 1)
            //{ }

            if (dp[curRow, curCol] != 0)
            {
                return dp[curRow, curCol];
            }

            int fromLeftPath = Solve(grid, dp, curRow, curCol-1);
            int fromTopPath = Solve(grid, dp, curRow-1, curCol);

            int minCost = Math.Min(fromLeftPath, fromTopPath) + grid[curRow][curCol];
            dp[curRow, curCol] = minCost;
            return minCost;
        }



        private static int MinPathSum2(int[][] grid)
        {
            if (grid.Length == 0)
            {
                return 0;
            }

            if (grid.Length == 1)
            {
                // 只有一維陣列，直接加總就好，因為也就只能從起點一直往右走
                int sum = 0;
                for (int i = 0; i < grid[0].Length; i++)
                {
                    sum += grid[0][i];
                }
                return sum;
            }

            int rowCount = grid.Length;
            int colCount = grid[0].Length;

            // dp[i,j] 用來記錄從起點 [0,0] 到 [i,j] 的最短路徑
            int[,] dp = new int[rowCount, colCount];


            Solve2(grid, dp, 0, 0, rowCount, colCount, 0);

            return dp[rowCount - 1, colCount - 1];
        }

        // 從開頭推到結尾用暴力解的流程其實可以，因為有 dp table 輔助
        // 其實 rowCount 和 colCount 這邊仍然可以省略，只要從 grid 或 dp 就可以判斷，
        // 不過就是要在這 recursive method 裡面抓就是了
        private static int Solve2(int[][] grid, int[,] dp, int curRow, int curCol, int rowCount, int colCount, int currentSum)
        {
            // todo: 因為是從起點出發，所以應該要累積加總???? 到終點時再跟目前 dp紀錄的最短路徑比較，假如是最佳值就取代之??

            // 抵達終點
            if (curRow == rowCount - 1 && curCol == colCount - 1)
            {
                return dp[curRow, curCol];
            }

            // 抵達最下面
            if (curRow == rowCount - 1)
            { }

            // 抵達最右邊
            if(curCol == colCount - 1)
            { }

            if(dp[curRow,curCol] != 0)
            {
                return dp[curRow, curCol];
            }


            currentSum += grid[curRow][curCol];

            // 往下走
            int toBottom = Solve2(grid, dp, curRow + 1, curCol, rowCount, colCount, currentSum);
            // 往右走
            int toRight = Solve2(grid, dp, curRow, curCol + 1, rowCount, colCount, currentSum);

            int minPath = Math.Min(toBottom, toRight);

            dp[curRow, curCol] = minPath;
            return minPath;

        }


        private static int MinPathSum3(int[][] grid)
        {
            int rowCount = grid.Length;
            int colCount = grid[0].Length;

            int[,] dp = new int[rowCount, colCount];
            dp[0, 0] = grid[0][0];

            // 先填滿最上列
            int sum = 0;
            for (int i = 0; i < colCount; i++)
            {
                sum += grid[0][i];
                dp[0, i] = sum;
            }
            // 先填滿最左欄
            sum = 0;
            for (int i = 0; i < rowCount; i++)
            {
                sum += grid[i][0];
                dp[i, 0] = sum;
            }

            for (int i = 1; i < rowCount; i++)
            {
                for (int k = 1; k < colCount; k++)
                {
                    int fromTop = grid[i][k] + dp[i - 1, k];
                    int fromLeft = grid[i][k] + dp[i, k - 1];
                    int min = Math.Min(fromTop, fromLeft);
                    dp[i, k] = min;
                }
            }
            return dp[rowCount - 1, colCount - 1];
        }
    }
}
