using System;

namespace UniquePathsII
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] obstacleGrid = new int[][]
            {
                new int[] { 0, 0, 0 },
                new int[] { 0, 1, 0 },
                new int[] { 0, 0, 0 }
            };

            obstacleGrid = new int[][]
            {
                new int[] { 0, 1 },
                new int[] { 0, 0 }
            };

            // 障礙物在終點的 case，無法到達終點，答案為 0
            obstacleGrid = new int[][]
            {
                new int[] { 0, 0 },
                new int[] { 0, 1 }
            };

            // 大 size 的 test case，答案為 718991952，暴力解法跑超久
            obstacleGrid = new int[][]
            {
                new int[] {0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,0,0,1,0,1,1,0,1,0,0,1,0,0,0,1,0,0},
                new int[] {0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0},
                new int[] {1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0,0,1,0,0,0},
                new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,1,0,0,0,1,0,0,0,0,0,0,1,0,0,1,0,1},
                new int[] {0,0,0,1,0,0,0,0,0,0,0,1,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                new int[] {0,0,1,0,0,0,1,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,0,0,1,0,0,1,0,0},
                new int[] {0,0,0,0,0,0,1,0,0,0,1,1,0,1,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0},
                new int[] {1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,1,1,0,0,0,1},
                new int[] {0,0,0,0,1,0,0,1,0,1,1,1,0,0,0,1,0,0,1,0,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0},
                new int[] {0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,1,0,1,0,0,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0},
                new int[] {1,0,1,0,1,1,0,1,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,1,1,0,0,0,0,1,0,0,0,1,0},
                new int[] {0,0,0,0,0,0,1,0,0,1,1,0,0,1,0,0,0,0,1,0,0,1,1,0,0,0,0,0,1,0,0,1,0,0,0,1},
                new int[] {0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1,0,0,1,0,0,0},
                new int[] {1,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                new int[] {0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                new int[] {0,1,0,0,1,0,0,0,0,0,1,0,1,1,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0},
                new int[] {0,0,0,0,1,0,0,0,1,0,0,0,0,1,0,0,1,1,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0},
                new int[] {0,0,0,0,0,1,0,0,1,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                new int[] {0,0,1,0,1,0,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                new int[] {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0},
                new int[] {0,0,0,1,0,1,0,0,1,0,0,0,0,0,1,1,1,0,1,1,1,0,0,1,0,1,0,1,1,1,0,0,0,0,0,0},
                new int[] {0,0,1,0,0,0,0,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0},
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0},
                new int[] {0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                new int[] {1,1,0,0,0,0,1,0,0,1,1,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                new int[] {0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,1,0,0},
                new int[] {0,0,0,0,1,0,0,1,0,0,0,0,0,0,1,0,0,1,0,1,1,1,0,0,0,0,0,0,1,0,0,0,1,1,0,0},
                new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0}
            };

            // int count = UniquePathsWithObstacles(obstacleGrid);
            int count = UniquePathsWithObstacles2(obstacleGrid);
            Console.WriteLine("count: {0}", count);

        }
        private static int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int rowCount = obstacleGrid.Length;
            int colCount = obstacleGrid[0].Length;

            // return Solve(0, 0, rowCount, colCount, obstacleGrid);

            int[,] memo = new int[rowCount, colCount];
            return Solve2(0, 0, rowCount, colCount, obstacleGrid, memo);


        }

        // brute force
        private static int Solve(int curRow, int curCol, int rowCount, int colCount, int[][] obstacleGrid)
        {
            // 先判斷是否超出邊界
            if (curRow == rowCount || curCol == colCount)
            {
                return 0;
            }

            // 此位置是障礙物
            if (obstacleGrid[curRow][curCol] == 1)
            {
                return 0;
            }

            // 抵達終點
            if (curRow == rowCount - 1 && curCol == colCount - 1)
            {
                return 1;
            }

            // 到最底部
            if (curRow == rowCount - 1)
            {
                return Solve(curRow, curCol + 1, rowCount, colCount, obstacleGrid);
            }
            // 到最右邊
            if (curCol == colCount - 1)
            {
                return Solve(curRow + 1, curCol, rowCount, colCount, obstacleGrid);
            }

            return Solve(curRow + 1, curCol, rowCount, colCount, obstacleGrid) + Solve(curRow, curCol + 1, rowCount, colCount, obstacleGrid);
        }

        // 加上 memory 輔助
        private static int Solve2(int curRow, int curCol, int rowCount, int colCount, int[][] obstacleGrid, int[,] memo)
        {
            // 先判斷是否超出邊界
            if (curRow == rowCount || curCol == colCount)
            {
                return 0;
            }

            // 此位置是障礙物
            if (obstacleGrid[curRow][curCol] == 1)
            {
                return 0;
            }

            // 抵達終點
            if (curRow == rowCount - 1 && curCol == colCount - 1)
            {
                return 1;
            }

            // 到最底部
            if (curRow == rowCount - 1)
            {
                return Solve2(curRow, curCol + 1, rowCount, colCount, obstacleGrid, memo);
            }
            // 到最右邊
            if (curCol == colCount - 1)
            {
                return Solve2(curRow + 1, curCol, rowCount, colCount, obstacleGrid, memo);
            }

            if (memo[curRow, curCol] > 0)
            {
                return memo[curRow, curCol];
            }

            memo[curRow, curCol] = Solve2(curRow + 1, curCol, rowCount, colCount, obstacleGrid, memo) + Solve2(curRow, curCol + 1, rowCount, colCount, obstacleGrid, memo);

            return memo[curRow, curCol];
        }


        private static int UniquePathsWithObstacles2(int[][] obstacleGrid)
        {
            // 起點就是障礙物
            if (obstacleGrid[0][0] == 1)
            {
                return 0;
            }

            int rowCount = obstacleGrid.Length;
            int colCount = obstacleGrid[0].Length;

            // 能到達最左上角起點位置的路徑只有自己本身，先初始化為 1
            obstacleGrid[0][0] = 1;
            // 先 loop 第一行
            for (int i = 1; i < colCount; i++)
            {
                // 目前位置是障礙物
                if (obstacleGrid[0][i] == 1)
                {
                    // 改為 0，表示沒有任何路徑可到達此處
                    obstacleGrid[0][i] = 0;
                }
                else
                {
                    // 由於只能往右移動，目前又在最上面，所以能到達目前位置的路徑只有左邊的位置
                    obstacleGrid[0][i] = obstacleGrid[0][i - 1];
                }
            }

            // 先 loop 第一列
            for (int k = 1; k < rowCount; k++)
            {
                // 目前位置是障礙物
                if (obstacleGrid[k][0] == 1)
                {
                    // 改為 0，表示沒有任何路徑可到達此處
                    obstacleGrid[k][0] = 0;
                }
                else
                {
                    // 由於只能往下移動，目前又在最左邊面，所以能到達目前位置的路徑只有上面的位置
                    obstacleGrid[k][0] = obstacleGrid[k - 1][0];
                }
            }

            for (int i = 1; i < rowCount; i++)
            {
                for (int k = 1; k < colCount; k++)
                {
                    // 目前位置是障礙物
                    if (obstacleGrid[i][k] == 1)
                    {
                        // 改為 0，表示沒有任何路徑可到達此處
                        obstacleGrid[i][k] = 0;
                    }
                    else
                    {
                        // 能到達目前位置的只有 相鄰的左邊 和 相鄰的上面
                        obstacleGrid[i][k] = obstacleGrid[i - 1][k] + obstacleGrid[i][k - 1];
                    }
                }
            }

            return obstacleGrid[rowCount - 1][colCount - 1];
        }
    }
}
