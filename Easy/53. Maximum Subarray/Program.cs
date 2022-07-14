using System;
using System.Text.RegularExpressions;

namespace MaximumSubarray
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/maximum-subarray/
            // 給一個整數陣列，回傳陣列中，總和數字最大的子陣列的和
            int[] nums = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

            // ans: 1
            // nums = new int[] { 1 };

            // ans: 0
            // nums = new int[] { 0 };

            // ans: -1
            // nums = new int[] { -1 };

            // ans: -2147483647
            // nums = new int[] { -2147483647 };

            // ans: 21
            nums = new int[] { 8, -19, 5, -4, 20 };

            // int sum = MaxSubArray(nums);
            int sum = MaxSubArray4(nums);
            Console.WriteLine("sum: {0}", sum);
        }

        // brute force
        private static int MaxSubArray(int[] nums)
        {
            int max = nums[0];
            for (int i = 0; i < nums.Length; i++)
            {
                int sum = 0;
                for (int k = i; k < nums.Length; k++)
                {
                    sum += nums[k];
                    if (sum > max)
                    {
                        max = sum;
                    }
                }
            }
            return max;
        }

        // O(n) https://leetcode.com/problems/maximum-subarray/discuss/20210/O(n)-Java-solution
        // 其實就是 DP 解
        private static int MaxSubArray2(int[] nums)
        {
            int max = Int32.MinValue;
            int sum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                // 只要目前總和小於 0，就直接洗掉
                if (sum < 0)
                {
                    sum = nums[i];
                }
                else
                {
                    sum += nums[i];
                }

                if (sum > max)
                {
                    max = sum;
                }
            }

            return max;
        }

        // DP解，其實跟 MaxSubArray2 一樣
        private static int MaxSubArray3(int[] nums)
        {
            int sum = 0;
            int max = nums[0];
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                // 如果 sum 是負數，就直接歸 0
                sum = Math.Max(nums[i], sum);
                max = Math.Max(sum, max);
            }
            return max;
        }

        // Divide and Conquer，演算法課本有
        private static int MaxSubArray4(int[] nums)
        {
            return GetSubArraySum(0, nums.Length - 1, nums);
        }
        private static int GetSubArraySum(int start, int end, int[] nums)
        {
            if (start == end)
            {
                return nums[start];
            }

            int middle = (start + end) / 2;

            int leftSum = GetSubArraySum(start, middle, nums);
            int rightSum = GetSubArraySum(middle + 1, end, nums);
            int middleSum = GetCrossSubArraySum(nums, start, middle, end);

            // if (start == 0 && end == 1)
            // {
            //     Console.WriteLine("{0} {1} {2}", leftSum, middleSum, rightSum);
            // }
            if (leftSum == 8)
            {
                Console.WriteLine("{0} {1} {2}", leftSum, middleSum, rightSum);
            }

            int max = Math.Max(leftSum, rightSum);
            max = Math.Max(max, middleSum);
            return max;
        }
        private static int GetCrossSubArraySum(int[] nums, int start, int middle, int end)
        {
            int leftMax = nums[middle];
            int rightMax = nums[middle + 1];

            int leftSum = 0;
            int rightSum = 0;

            for (int i = middle; i >= start; i--)
            {
                leftSum += nums[i];
                leftMax = Math.Max(leftMax, leftSum);
            }

            for (int i = middle + 1; i <= end; i++)
            {
                rightSum += nums[i];
                rightMax = Math.Max(rightMax, rightSum);
            }

            int max = Math.Max(leftMax, rightMax);
            max = Math.Max(max, leftMax + rightMax);

            return max;
        }

    }
}
