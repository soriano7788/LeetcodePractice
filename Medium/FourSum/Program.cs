using System;
using System.Collections.Generic;

namespace FourSum
{
    class Program
    {
        static void Main(string[] args)
        {
            // 注意 組合不可重複
            int[] nums = new int[] {1, 0, -1, 0, -2, 2};
            int target = 0;

            nums = new int[] {-3,-2,-1,0,0,1,2,3};
            target = 0;

            nums = new int[] {-5,5,4,-3,0,0,4,-2};
            target = 4;

            IList<IList<int>> result = FourSum2(nums, target);
            foreach(var r in result)
            {
                Console.WriteLine(string.Join(", ", r));
            }
        }
        private static IList<IList<int>> FourSum(int[] nums, int target) 
        {
            List<IList<int>> result = new List<IList<int>>();
            HashSet<string> hs = new HashSet<string>();
            for(int a = 0; a < nums.Length - 3; a++)
            {
                for(int b = a + 1; b < nums.Length - 2; b++)
                {
                    for(int c = b + 1; c < nums.Length - 1; c++)
                    {
                        for(int d = c + 1; d < nums.Length; d++)
                        {
                            int aNum = nums[a],
                                bNum = nums[b],
                                cNum = nums[c],
                                dNum = nums[d];
                            if(aNum + bNum + cNum + dNum == target)
                            {
                                List<int> quadruplets = new List<int>() {aNum, bNum, cNum, dNum};
                                quadruplets.Sort();
                                string key = string.Join(",", quadruplets);
                                if(hs.Contains(key))
                                {
                                    continue;
                                }
                                result.Add(quadruplets);
                                hs.Add(key);
                            }
                        }
                    }
                }
            }
            return result;
        }

        // 第四個數字用 hash 查
        private static IList<IList<int>> FourSum2(int[] nums, int target) 
        {
            List<IList<int>> result = new List<IList<int>>();
            HashSet<string> hs = new HashSet<string>();

            HashSet<int> numsHs = new HashSet<int>();
            foreach(int num in nums)
            {
                numsHs.Add(num);
            }

            for(int a = 0; a < nums.Length - 3; a++)
            {
                for(int b = a + 1; b < nums.Length - 2; b++)
                {
                    for(int c = b + 1; c < nums.Length - 1; c++)
                    {
                        int aNum = nums[a],
                            bNum = nums[b],
                            cNum = nums[c];

                        // todo: 注意第四個數字，不要跟前三個重複到
                        int complement = target - aNum - bNum - cNum;
                        if(numsHs.Contains(complement))
                        {
                            List<int> quadruplets = new List<int>() {aNum, bNum, cNum, complement};
                            quadruplets.Sort();
                            string key = string.Join(",", quadruplets);
                            if(hs.Contains(key))
                            {
                                continue;
                            }
                            result.Add(quadruplets);
                            hs.Add(key);
                        }
                    }
                }
            }
            return result;
        }
    }
}
