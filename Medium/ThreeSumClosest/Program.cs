using System;
using System.Linq;

namespace ThreeSumClosest
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] {-1, 2, 1, -4};
            int target = 1;

            nums = new int[] {1, 1, 1, 0};
            target = -100;

            nums = new int[] {1, 1, -1, -1, 3};
            target = -1;

            int closestSum = ThreeSumClosest(nums, target);
            int closestSum2 = ThreeSumClosest2(nums, target);
            Console.WriteLine("closestSum: {0}", closestSum);
            Console.WriteLine("closestSum2: {0}", closestSum2);
        }
        private static int ThreeSumClosest(int[] nums, int target)
        {
            int closestSumNum = int.MaxValue;
            long closestSumDistance = Math.Abs((long)(closestSumNum - target));
            for(int i = 0; i < nums.Length - 2; i++)
            {
                for(int j = i + 1; j < nums.Length; j++)
                {
                    for(int k = j + 1; k < nums.Length; k++)
                    {
                        int sum = nums[i] + nums[j] + nums[k];
                        int distance = Math.Abs(sum - target);

                        if(distance < closestSumDistance)
                        {
                            closestSumNum = sum;
                            closestSumDistance = distance;
                        }
                    }
                }
            }
            return closestSumNum;
        }
        
        // todo: 仍有問題
        private static int ThreeSumClosest2(int[] nums, int target)
        {
            nums = nums.OrderBy(v => v).ToArray();
            int minDifference = int.MaxValue,
                closestSum = int.MaxValue;
            for(int i = 0; i < nums.Length - 2; i++)
            {
                int start = i + 1,
                    end = nums.Length - 1;
                while(start < end)
                {
                    int sum = nums[i] + nums[start] + nums[end];
                    int difference = Math.Abs(sum - target);
                    if(difference < minDifference)
                    {
                        minDifference = difference;
                        closestSum = sum;
                        start++;
                    }
                    else
                    {
                        end--;
                    }
                }
            }
            return closestSum;
        }

        private static int ThreeSumClosest3(int[] nums, int target)
        {
            // todo:
            // 先固定兩個數字，第三個數字用 binary search 決定，
            // 雖然速度比不上 two pointer 方法，
            // 但思考上比較直覺
        }
    }
}
