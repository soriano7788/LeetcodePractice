using System;
using System.Collections;
using System.Collections.Generic;

namespace TwoSum
{
    class Program
    {
        static void Main(string[] args)
        {
            var now = DateTime.UtcNow;
            var nowAdd = now.AddSeconds(1800);
            var s = (nowAdd - now).TotalSeconds;
            Console.WriteLine("s: {0}", (int)s);
            return;

            // 某方法: 先用 merge sort 排序，再用 binary search 找出互補樹是否存在


            int[] nums = new[] { 2, 7, 11, 15 };
            nums = new[] { 3, 2, 4 };

            int target = 9;
            target = 6;

            int[] ans = TwoSum2(nums, target);
            if (ans != null)
            {
                Console.WriteLine(ans[0]);
                Console.WriteLine(ans[1]);
            }
        }
        private static int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int k = i + 1; k < nums.Length; k++)
                {
                    if (nums[i] + nums[k] == target)
                    {
                        return new int[] { i, k };
                    }
                }
            }
            return null;
        }
        private static int[] TwoSum2(int[] nums, int target)
        {
            Hashtable ht = new Hashtable();
            ht.Add(9, 9);
            ht.Add(9, 10);
            


            for (int i = 0; i < nums.Length; i++)
            {
                ht.Add(nums[i], i);
            }

            for (int i = 0; i < nums.Length; i++)
            {
                int num2 = target - nums[i];
                if (ht.Contains(num2) && i != Convert.ToInt32(ht[num2]))
                {
                    return new int[] { i, Convert.ToInt32(ht[num2]) };
                }
            }
            return null;
        }
    }
}
