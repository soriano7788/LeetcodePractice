using System;

namespace ContainerWithMostWater
{
    class Program
    {
        static void Main(string[] args)
        {
            // 高度以較短的長條柱為標準
            // 有可能用 sliding window 的解法??? (應該不可，因為沒判斷 i 是否要往前的條件)
            // array [1,8,6,2,5,4,8,3,7]
            int[] input = new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
            Console.WriteLine(MaxArea(input));
            Console.WriteLine(TwoPointerApproach(input));
        }
        private static int MaxArea(int[] height)
        {
            int maxArea = 0;
            for (int i = 0; i < height.Length - 1; i++)
            {
                for (int j = i + 1; j < height.Length; j++)
                {
                    int h = Math.Min(height[i], height[j]);
                    int currentArea = h * (j - i);
                    maxArea = Math.Max(maxArea, currentArea);
                }
            }
            return maxArea;
        }

        private static int TwoPointerApproach(int[] height)
        {
            int maxArea = 0;

            return maxArea;
        }

    }
}
