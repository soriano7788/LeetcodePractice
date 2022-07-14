using System;
using System.Collections;
using System.Collections.Generic;

namespace SpiralMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] matrix = new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 9},
            };
            // output: 1, 2, 3, 6, 9, 8, 7, 4, 5

            // matrix = new int[][]
            // {
            //     new int[] {1, 2, 3, 4},
            //     new int[] {5, 6, 7, 8},
            //     new int[] {9, 10, 11, 12}
            // };
            // output: 1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7

            // matrix = new int[][]
            // {
            //     new int[] {1, 2, 3, 4},
            //     new int[] {5, 6, 7, 8},
            //     new int[] {9, 10, 11, 12},
            //     new int[] {13, 14, 15, 16},
            // };
            // output: 1, 2, 3, 4, 8, 12, 16, 15, 14, 13, 9, 5, 6, 7, 11, 10


            IList<int> result = SpiralOrder(matrix);
            Console.WriteLine(string.Join(", ", result));
        }
        private static IList<int> SpiralOrder(int[][] matrix)
        {
            IList<int> result = new List<int>();

            // 取得行列數
            int rowCount = matrix.Length;
            int colCount = 0;
            if (rowCount > 0)
            {
                colCount = matrix[0].Length;
            }

            // 紀錄目前已塞進幾個數字
            int currentCount = 0;
            // matrix 元素總數
            int totalCount = rowCount * colCount;

            // 紀錄 上下左右邊界，隨著移動會改變
            int topBound = 0,
                rightBound = colCount - 1,
                bottomBound = rowCount - 1,
                leftBound = 0;

            // 目前的移動方向
            string direction = "right";
            // 目前的行列索引
            int rowIndex = 0,
                colIndex = 0;
            // 還沒取完全部元素就繼續
            while (currentCount < totalCount)
            {
                int el = matrix[rowIndex][colIndex];
                result.Add(el);
                currentCount++;
                switch (direction)
                {
                    case "right":
                        if (colIndex < rightBound)
                        {
                            colIndex++;
                        }
                        else
                        {
                            //向下
                            direction = "down";
                            rowIndex++;
                            // 邊界縮小
                            topBound++;
                        }
                        break;
                    case "down":
                        if (rowIndex < bottomBound)
                        {
                            rowIndex++;
                        }
                        else
                        {
                            direction = "left";
                            colIndex--;
                            // 邊界縮小
                            rightBound
                            --;
                        }
                        break;
                    case "left":
                        if (colIndex > leftBound)
                        {
                            colIndex--;
                        }
                        else
                        {
                            direction = "up";
                            rowIndex--;
                            bottomBound--;
                        }
                        break;
                    case "up":
                        if (rowIndex > topBound)
                        {
                            rowIndex--;
                        }
                        else
                        {
                            direction = "right";
                            colIndex++;
                            leftBound++;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
