using System;
using System.Collections.Generic;

namespace FirstMissingPositive
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/first-missing-positive/
            int[] nums = new int[] { 7, 8, 9, 11, 12 }; // output 1
            // nums = new int[] { 1, 2, 0 }; // output 3
            // nums = new int[] { 3, 4, -1, 1 }; // output 2
            // nums = new int[] { -1, 4, 2, 1, 9, 10 }; // output 3
            nums = new int[] { 1, 1 }; // output 2

            int ans = FirstMissingPositive2(nums);
            Console.WriteLine("ans: {0}", ans);
        }
        private static int FirstMissingPositive(int[] nums)
        {
            int minPositiveNum = int.MinValue,
                maxPositiveNum = int.MinValue;
            HashSet<int> hs = new HashSet<int>();
            // O(n)
            for (int i = 0; i < nums.Length; i++)
            {
                hs.Add(nums[i]);

                if (nums[i] > 0)
                {
                    if (nums[i] < minPositiveNum)
                    {
                        minPositiveNum = nums[i];
                    }
                    if (nums[i] > maxPositiveNum)
                    {
                        maxPositiveNum = nums[i];
                    }
                }
            }

            // 最大正數 小於等於 0
            if (maxPositiveNum <= 0)
            {
                return 1;
            }
            // 最小正數 大於 1
            if (minPositiveNum > 1)
            {
                return 1;
            }

            // check between min and max positive
            int startNum;
            // 最小正數小於 0 的話，就直接從 1 開始，因為要找"正"數
            if (minPositiveNum <= 0)
            {
                startNum = 1;
            }
            else
            {
                startNum = minPositiveNum;
            }

            int num = startNum;
            // O(??)
            // 可能是 1 ~ maxPositiveNum
            // 或是 minPositiveNum ~ maxPositiveNum
            // 是否有 big O 超過 n 的情形???
            while (num <= maxPositiveNum)
            {
                if (!hs.Contains(num))
                {
                    return num;
                }
                num++;
            }

            return num;
        }

        private static int FirstMissingPositive2(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                // 為了避免 nums[i] 換到正確位置，
                // 但是被換過來的 element 不在正確位置，
                // 這時候 for loop 就一直線到底了，
                // 沒機會再把被換過來的 element 換到正確的位置
                // 所以用 while loop 來處理
                while (nums[i] > 0 &&  // 只處理正數
                        nums[i] < nums.Length &&  // 確定不會超出 Array 邊界
                        /*nums[i] != (i + 1)*/ nums[i] != nums[nums[i]-1])  // 確定目前數字不在正確位置
                {
                    Swap(nums, i, nums[i] - 1);
                    //Console.WriteLine("After swap: {0}", string.Join(", ", nums));
                }
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != (i + 1))
                {
                    return i + 1;
                }
            }
            return nums.Length + 1;
        }
        private static void Swap(int[] nums, int x, int y)
        {
            int tmp = nums[x];
            nums[x] = nums[y];
            nums[y] = tmp;
        }
    }
}
