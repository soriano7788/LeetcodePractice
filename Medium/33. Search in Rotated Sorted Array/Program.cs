using System;

namespace SearchInRotatedSortedArray
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/search-in-rotated-sorted-array/
            int[] nums = new int[] { 4, 5, 6, 7, 0, 1, 2 };
            int target = 0;

            int index = Search(nums, target);
            Console.WriteLine("index: {0}", index);
        }
        private static int Search(int[] nums, int target)
        {
            int leftIndex = 0,
                rightIndex = nums.Length - 1;

            while (leftIndex <= rightIndex)
            {
                int middleIndex = (leftIndex + rightIndex) / 2;
                if (nums[middleIndex] == target)
                {
                    return middleIndex;
                }

                // nums[left to mid] is sorted
                if (nums[leftIndex] <= nums[middleIndex])
                {
                    if (target >= nums[leftIndex] && target < nums[middleIndex])
                    {
                        rightIndex = middleIndex - 1;
                    }
                    else
                    {
                        leftIndex = middleIndex + 1;
                    }
                }
                else  // nums[mid to right] is sorted
                {
                    if (target > nums[middleIndex] && target <= nums[rightIndex])
                    {
                        leftIndex = middleIndex + 1;
                    }
                    else
                    {
                        rightIndex = middleIndex - 1;
                    }
                }
            }
            return -1;
        }
    }
}
