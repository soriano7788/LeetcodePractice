using System;

namespace FindFirstAndLastPositionOfElementInSortedArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] { 5, 7, 7, 8, 8, 10 };
            int target = 8;

            nums = new int[] { 1 };
            target = 1;

            int[] range = SearchRange(nums, target);
            Console.WriteLine("start: {0}, end: {1}", range[0], range[1]);
        }
        private static int[] SearchRange(int[] nums, int target)
        {
            int left = 0,
                right = nums.Length - 1;

            int targetIndex = -1;
            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (target == nums[mid])
                {
                    targetIndex = mid;
                }

                if (target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            if (targetIndex == -1)
            {
                return new int[] { -1, -1 };
            }
            // find left boundary
            int startIndex = targetIndex;
            while (startIndex > 0 && nums[startIndex] == target)
            {
                if (nums[startIndex - 1] == target)
                {
                    startIndex--;
                }
                else
                {
                    break;
                }
            }
            // find right boundary
            int endIndex = targetIndex;
            while (endIndex < nums.Length - 1 && nums[endIndex] == target)
            {
                if (nums[endIndex + 1] == target)
                {
                    endIndex++;
                }
                else
                {
                    break;
                }
            }
            return new int[] { startIndex, endIndex };
        }

        private static int[] SearchRangeAdvanced(int[] nums, int target)
        {
            int[] targetRange = new int[] { -1, -1 };
            int leftIdx = extremeInsertionIndex(nums, target, true);
            if (leftIdx >= nums.Length || nums[leftIdx] != target)
            {
                return targetRange;
            }

            targetRange[0] = leftIdx;
            targetRange[1] = extremeInsertionIndex(nums, target, false) - 1;
            return targetRange;
        }
        private static int extremeInsertionIndex(int[] nums, int target, bool left)
        {
            int leftIdx = 0,
                rightIdx = nums.Length - 1;

            while (leftIdx < rightIdx)
            {
                int midIdx = (leftIdx + rightIdx) / 2;
                if (target < nums[midIdx] || (left && target == nums[midIdx]))
                {
                    rightIdx = midIdx;
                }
                else
                {
                    leftIdx = midIdx + 1;
                }
            }
            return leftIdx;
        }
    }
}
