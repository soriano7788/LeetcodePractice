using System;

namespace _198._House_Robber
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/house-robber/

            // 扮演一名小偷，從一排房子裡面偷錢，每個房子裡面有藏錢，用 int array 表示
            // 需注意防盜系統，
            // 當兩個相鄰的房子都被偷的話，防盜系統就會啟動，
            // 總之就是不可偷連續的，中間至少要空一個

            // 目標: 不可觸發防盜系統，然後可偷到最多錢的結果
            // 限制: 第 (i) 和 (i+1) 的房子被偷竊後，房子的警報系統就會觸發

            // 0 <= nums length <= 100
            // 0 <= nums[i] <= 400

            // 起始店不一定要 從頭 或 從尾 開始，也可以從中間開始，切兩半
            // 就像是 divide and conquer


            // output: 4 ( rob 1st house then get 1 money, rob 3rd house then get 3, final get total 4)
            int[] nums = new int[] { 1, 2, 3, 1 };

            // output: 12 (index，0 2 4)
            nums = new int[] { 2, 7, 9, 3, 1 };

            // output: 2 (index，1)
            nums = new int[] { 1, 2 };

            // output: 3365
            nums = new int[] { 183, 219, 57, 193, 94, 233, 202, 154, 65, 240, 97, 234, 100, 249, 186, 66, 90, 238, 168, 128, 177, 235, 50, 81, 185, 165, 217, 207, 88, 80, 112, 78, 135, 62, 228, 247, 211 };

            // output: 4173
            nums = new int[] { 114, 117, 207, 117, 235, 82, 90, 67, 143, 146, 53, 108, 200, 91, 80, 223, 58, 170, 110, 236, 81, 90, 222, 160, 165, 195, 187, 199, 114, 235, 197, 187, 69, 129, 64, 214, 228, 78, 188, 67, 205, 94, 205, 169, 241, 202, 144, 240 };

            //int result = Rob(nums);
            //int result = Rob2(nums);
            //int result = Rob3(nums);
            //int result = Rob4(nums);
            //int result = Rob5(nums);
            int result = Rob6(nums);

            Console.WriteLine("result: {0}", result);
        }

        // todo: output 為 3365 的 input 跑不完，不知道有沒有時間複雜度以外的原因?
        private static int Rob(int[] nums)
        {
            // 注意: ans 有可能會不包括 index 0..
            return Solve(nums, 0, 0);
        }
        private static int Solve(int[] nums, int curIndex, int total)
        {
            // 檢查目前 index 是否已超界
            if(curIndex >= nums.Length)
            {
                // 超界了，直接回傳 total
                return total;
            }

            #region rob 當前 house
            int curHouseMoney = nums[curIndex];
            total += curHouseMoney;
            curIndex += 2;

            int max = total;
            for (int i = curIndex; i < nums.Length; i++)
            {
                int tmp = Solve(nums, i, total);
                max = Math.Max(max, tmp);
            }
            #endregion

            #region 不 rob 當前 house
            total -= curHouseMoney;
            curIndex -= 2;
            curIndex += 1;

            for (int i = curIndex; i < nums.Length; i++)
            {
                int tmp = Solve(nums, i, total);
                max = Math.Max(max, tmp);
            }
            #endregion

            return max;
        }


        private static int Rob2(int[] nums)
        {
            return Solve2(nums, 0);
        }
        private static int Solve2(int[] nums, int index)
        {
            if(index >= nums.Length)
            {
                return 0;
            }

            // Max 的第一個參數是，目前房子的錢 加上格一格空格後的房子的錢
            // 第二個參數是，下一個房子的錢

            // 第二個參數是考量到目前 index 的房子，不在最佳解的範圍內
            // 所以 Solve2 的其中一條遞迴分支，假如有連續跑第二個參數的分支，那就會是中間空好幾個房子不偷的 case
            int max = Math.Max(nums[index] + Solve2(nums, index + 2), Solve2(nums, index + 1));

            return max;
        }

        #region recursive divide and conquer (top-down)
        // 其實就是 Rob2 從 array 最後面往前處理的版本而已
        private static int Rob3(int[] nums)
        {
            return Solve3(nums, nums.Length - 1);
        }
        private static int Solve3(int[] nums, int index)
        {
            if(index < 0)
            {
                return 0;
            }
            return Math.Max(nums[index] + Solve3(nums, index - 2), Solve3(nums, index - 1));
        }
        #endregion

        #region recursive divide and conquer (top-down + memory table)

        private static int Rob4(int[] nums)
        {
            // memory array 有需要多一個元素空間嗎??
            int[] dp = new int[nums.Length + 1];
            Array.Fill(dp, -1);
            return Solve4(nums, nums.Length - 1, dp);

        }
        private static int Solve4(int[] nums, int index, int[] dp)
        {
            if(index < 0)
            {
                return 0;
            }

            if(dp[index] >= 0)
            {
                return dp[index];
            }

            int result = Math.Max(nums[index] + Solve4(nums, index - 2, dp), Solve4(nums, index - 1, dp));
            dp[index] = result;

            return result;
        }

        #endregion


        #region iterative memory array (bottom-up)
        private static int Rob5(int[] nums)
        {
            if(nums.Length == 0)
            {
                return 0;
            }

            // dp 的 index 代表的是數量吧?
            int[] dp = new int[nums.Length + 1];
            dp[0] = 0;
            dp[1] = nums[0];

            for(int i = 1; i < nums.Length; i++)
            {
                int val = nums[i];
                // 第一個參數，相當於 nums[i+1] 有偷，所以 nums[i] 不能偷(會觸發警報)，因此就不加上 val
                // 第二個參數，相當原 nums[i+1] 不偷，所以 nums[i] 可偷，因此加上 val
                // 思考上有點不太直覺
                dp[i + 1] = Math.Max(dp[i], dp[i - 1] + val);
            }
            return dp[nums.Length];
        }
        #endregion

        #region iterative (bottom-up) (replace memory array with 2 variable)
        private static int Rob6(int[] nums)
        {
            if(nums.Length == 0)
            {
                return 0;
            }

            int prev1 = 0, 
                prev2 = 0;

            int result = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                result = Math.Max(nums[i] + prev2, prev1);

                prev2 = prev1;
                prev1 = result;
            }
            return result;
        }

        #endregion


    }
}
