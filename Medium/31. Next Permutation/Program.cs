using System;

namespace NextPermutation
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/next-permutation/

            // 方法一: 暴力破解法
            // 先找出所有可能的排列組合 O(n!)
            // 再找出剛好比 input 大一點的那個排列組合 O(n)

            int[] nums = new int[] {1,2,3};
            nums = new int[] {1, 1};
            Console.WriteLine("before: {0}", string.Join(",", nums));
            NextPermutation(nums);
            Console.WriteLine("after: {0}", string.Join(",", nums));
        }
        private static void NextPermutation(int[] nums)
        {
            if(nums.Length <= 1)
            {
                return;
            }

            int i = nums.Length - 1;

            while(i > 0 && (nums[i] <= nums[i - 1]))
            {
                i--;
            }

            // 表示 nums 目前是完全大到小排序
            if(i == 0)
            {
                Reverse(nums, 0);
                return;
            }

            int k = nums.Length -1;
            while(k >= i && (nums[k] <= nums[i -1]))
            {
                k--;
            }
            Swap(nums,i-1,k);
            Reverse(nums,i);


            // compare current element with previous element from last
            // for(int i = nums.Length - 1 ; i > 0; i--)
            // {
            //     if(nums[i] > nums[i - 1])
            //     {
            //         // 方向錯了，要從後方開始找
            //         for(int k = i + 1; k < nums.Length - 1; k++)
            //         {
            //             if(nums[k] > nums[i - 1])
            //             {
            //                 Swap(nums,k , i - 1);
            //                 Reverse(nums, i);
            //                 break;
            //             }
            //         }
            //         break;
            //     }
            // }
        }
        private static void Reverse(int[] nums, int start)
        {
            int i = start,
                j = nums.Length - 1;

            while(i < j)
            {
                Swap(nums, i, j);
                i++;
                j--;
            }

        }
        private static void Swap(int[] nums, int x, int y)
        {
            int temp = nums[x];
            nums[x] = nums[y];
            nums[y] = temp;
        }


    }
}
