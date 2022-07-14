using System;

namespace RotateImage
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/rotate-image/

            int[][] matrix = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 },
                new int[] { 7, 8, 9 }
            };
            /*
            {7, 4, 1}
            {8, 5, 2}
            {9, 6, 3}
            */
            matrix = new int[][]
            {
                new int[] { 5, 1, 9, 11 },
                new int[] { 2, 4, 8, 10 },
                new int[] { 13, 3, 6, 7 },
                new int[] { 15, 14, 12, 16 }
            };
            /*
            {15, 13, 2, 5},
            {14, 3, 4, 1},
            {12, 6, 8, 9},
            {16, 7, 10, 11}
            */

            Rotate3(matrix);
            foreach (int[] row in matrix)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }
        private static void Test(out int[] nums)
        {
            int[] a = new int[] { 9, 8, 7, 6 };
            nums = a;
        }

        private static void Rotate(int[][] matrix)
        {
            // space O(n^2)
            // time O(n^2)

            // init new matrix
            int n = matrix.Length;
            int[][] newMatrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                newMatrix[i] = new int[n];
            }
            // 旋轉結果先放到新矩陣
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    newMatrix[k][n - 1 - i] = matrix[i][k];
                }
            }
            // 蓋回原矩陣
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    matrix[i][k] = newMatrix[i][k];
                }
            }
        }

        private static void Rotate2(int[][] matrix)
        {
            // 先處理最外圈旋轉結果，
            // 在依序處理較內圈旋轉的結果
            int n = matrix.Length;
            // Console.WriteLine("matrix.Length: {0}", matrix.Length);

            // if n=9, 依序為 9 7 5 3
            // 最外層 loop 控制內外圈
            for (int i = matrix.Length; i > 1; i -= 2)
            {
                // (n-i)/2
                // if n=9
                // i=9  , 0
                // i=7  , 1
                // i=5  , 2 .......
                int startIndex = (n - i) / 2;
                // endIndex is the end index of matrix we are processing
                int endIndex = startIndex + i - 1;
                // 這個 loop j 的 range 是每一條地變量 
                for (int j = 0; j < i - 1; j++)
                {
                    // move number on one side to the other side clockwise
                    // 先保留 左邊，由下往上那一條的 value
                    int temp = matrix[endIndex - j][startIndex];
                    // 左邊，由下到上那條 = 下面，由右到左那條
                    matrix[endIndex - j][startIndex] = matrix[endIndex][endIndex - j];
                    // 下面，由右到左那條 = 右邊，由上到下那條
                    matrix[endIndex][endIndex - j] = matrix[startIndex + j][endIndex];
                    // 右邊，由上到下那條 = 上面，由左到右那條
                    matrix[startIndex + j][endIndex] = matrix[startIndex][startIndex + j];
                    // 上面，由左到右那條 = 左邊，由下到上那條
                    matrix[startIndex][startIndex + j] = temp;
                }
            }
        }
        private static void Rotate3(int[][] matrix)
        {
            // 以左上右下對角線為軸，翻轉矩陣
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int k = 0; k < i; k++)
                {
                    int temp = matrix[i][k];
                    matrix[i][k] = matrix[k][i];
                    matrix[k][i] = temp;
                }
            }
            // 以垂直中線為軸，翻轉矩陣
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int k = 0; k < matrix.Length / 2; k++)
                {
                    int temp = matrix[i][k];
                    matrix[i][k] = matrix[i][matrix.Length - k - 1];
                    matrix[i][matrix.Length - k - 1] = temp;
                }
            }


        }
    }
}
