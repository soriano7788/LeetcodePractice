using System;

namespace UniquePaths
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 3,
                n = 7;

            m = 3;
            n = 2;

            m = 7;
            n = 3;

            m = 3;
            n = 3;

            m = 51;
            n = 9;

            // m = 3;
            // n = 4;

            // int wayCount = UniquePaths(m, n);
            int wayCount = UniquePaths2(m, n);
            Console.WriteLine("wayCount: {0}", wayCount);
        }

        private static int UniquePaths(int m, int n)
        {
            // return Solve(0, 0, m, n);
            int[,] memo = new int[m, n];
            // Console.WriteLine("memo.GetLength(0): {0}", memo.GetLength(0));
            // Console.WriteLine("memo.GetLength(1): {0}", memo.GetLength(1));

            // for (int i = 0; i < memo.GetLength(0); i++)
            // {
            //     for (int k = 0; k < memo.GetLength(1); k++)
            //     {
            //         Console.WriteLine("z:"+memo[i, k]);
            //     }
            // }

            return Solve2(0, 0, memo, m, n);
        }

        // brute force
        private static int Solve(int row, int col, int m, int n)
        {
            if (row == m - 1 && col == n - 1)
            {
                return 1;
            }

            if (row == m - 1)
            {
                return Solve(row, col + 1, m, n);
            }
            if (col == n - 1)
            {
                return Solve(row + 1, col, m, n);
            }

            return Solve(row, col + 1, m, n) + Solve(row + 1, col, m, n);
        }

        // 用 memory 輔助加速 brute force 方法
        // memo[x][y] 表示，在 (x, y) 這個位置，有 n 個路徑可以到終點
        // 從 (0, 0) 到 (x, y) 可能有 m 個路徑，
        // 第一次到達 (x, y) 後，知道有 n 個路徑可到終點
        // 之後第二或第三次以上從 (0, 0) 到 (x, y) 後就可以停止了，
        // 因為已經知道從 (x, y) 開始 到終點共有 n 個路徑，
        // 不需要再嘗試接下來的路徑了
        private static int Solve2(int row, int col, int[,] memo, int m, int n)
        {
            if (memo[row, col] > 0)
            {
                return memo[row, col];
            }

            if (row == m - 1 && col == n - 1)
            {
                return 1;
            }

            if (row == m - 1)
            {
                return Solve2(row, col + 1, memo, m, n);
            }
            if (col == n - 1)
            {
                return Solve2(row + 1, col, memo, m, n);
            }

            memo[row, col] = Solve2(row + 1, col, memo, m, n) + Solve2(row, col + 1, memo, m, n);

            return memo[row, col];
        }

        private static int UniquePaths2(int m, int n)
        {
            int[,] map = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                map[i, 0] = 1;
            }
            for (int j = 0; j < n; j++)
            {
                map[0, j] = 1;
            }
            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    // 等於 上面 和 左邊 的和
                    map[i, j] = map[i - 1, j] + map[i, j - 1];
                }
            }



            return map[m - 1, n - 1];
        }


    }
}
