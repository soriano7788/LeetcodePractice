using System;

namespace SpiralMatrixII
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/spiral-matrix-ii/

            int n = 3;
            // output:
            // 1 2 3
            // 8 9 4
            // 7 6 5

            // int[][] matrix = GenerateMatrix(n);
            // int[][] matrix = LeetCodeApproach1(n);
            int[][] matrix = LeetCodeApproach2(n);
            foreach (int[] row in matrix)
            {
                Console.WriteLine(string.Join(", ", row));
            }

        }
        enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
        private static int[][] GenerateMatrix(int n)
        {
            int count = n * n;

            int[][] matrix = new int[n][];
            for (int k = 0; k < n; k++)
            {
                matrix[k] = new int[n];
            }

            int topBound = 0,
                rightBound = n - 1,
                bottomBound = n - 1,
                leftBound = 0;

            int i = 1;
            int row = 0,
                col = 0;
            Direction direction = Direction.Right;
            while (i <= count)
            {
                matrix[row][col] = i;
                switch (direction)
                {
                    case Direction.Right:
                        if (col < rightBound)
                        {
                            col++;
                        }
                        else
                        {
                            row++;
                            topBound++;
                            direction = Direction.Down;
                        }
                        break;
                    case Direction.Down:
                        if (row < bottomBound)
                        {
                            row++;
                        }
                        else
                        {
                            col--;
                            rightBound--;
                            direction = Direction.Left;
                        }
                        break;
                    case Direction.Left:
                        if (col > leftBound)
                        {
                            col--;
                        }
                        else
                        {
                            row--;
                            bottomBound--;
                            direction = Direction.Up;
                        }
                        break;
                    case Direction.Up:
                        if (row > topBound)
                        {
                            row--;
                        }
                        else
                        {
                            col++;
                            leftBound++;
                            direction = Direction.Right;
                        }
                        break;
                }
                i++;
            }

            return matrix;
        }

        private static int[][] LeetCodeApproach1(int n)
        {
            // init matrix
            int[][] result = new int[n][];
            for (int k = 0; k < n; k++)
            {
                result[k] = new int[n];
            }

            int count = 1;
            for (int layer = 0; layer < (n + 1) / 2; layer++)
            {
                // left to right
                for (int ptr = layer; ptr < n - layer; ptr++)
                {
                    result[layer][ptr] = count++;
                }
                // top to bottom
                for (int ptr = layer + 1; ptr < n - layer; ptr++)
                {
                    result[ptr][n - layer - 1] = count++;
                }
                // right to left
                for (int ptr = layer + 1; ptr < n - layer; ptr++)
                {
                    result[n - layer - 1][n - ptr - 1] = count++;
                }
                // bottom to top
                for (int ptr = layer + 1; ptr < n - layer - 1; ptr++)
                {
                    result[n - ptr - 1][layer] = count++;
                }
            }

            return result;
        }

        private static int[][] LeetCodeApproach2(int n)
        {
            // init matrix
            int[][] result = new int[n][];
            for (int k = 0; k < n; k++)
            {
                result[k] = new int[n];
            }

            int count = 1;
            int d = 0; // 表示目前的方向 index，get by dir[d]
            int[][] dir = new int[][]
            {
                new int[] { 0, 1 }, // to right
                new int[] { 1, 0 }, // to bottom
                new int[] { 0, -1 }, // to left
                new int[] { -1, 0 } // to top
            };

            int row = 0,
                col = 0;
            while (count <= n * n)
            {
                result[row][col] = count++;
                int r = FloorMod(row + dir[d][0], n);
                int c = FloorMod(col + dir[d][1], n);

                // change direction if next cell is not zero
                if (result[r][c] != 0)
                {
                    d = (d + 1) % 4;
                }

                row += dir[d][0];
                col += dir[d][1];
            }
            return result;
        }
        private static int FloorMod(int x, int y)
        {
            return ((x % y) + y) % y;
        }

    }
}
