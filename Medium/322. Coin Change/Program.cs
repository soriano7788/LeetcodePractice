using System;

namespace CoinChange
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/coin-change/

            // 這題跟 使用完全平方數加總的組合解那題很類似，只是從完全平方數換成 硬幣面額
            // 使用最少數量的硬幣

            int[] coins = new int[] { 1, 2, 5 };
            int amount = 11;

            coins = new int[] { 2 };
            amount = 3;

            coins = new int[] { 1 };
            amount = 0;

            coins = new int[] { 1 };
            amount = 1;

            coins = new int[] { 1 };
            amount = 2;

            // input coins array 不一定有排序
            coins = new int[] { 2, 5, 10, 1 };
            amount = 27;

            // expected output: 20
            coins = new int[] { 186, 419, 83, 408 };
            amount = 6249;

            //coins = new int[] { 1 };
            //amount = 0;

            // 並非由大面額依序減到小面額就能正確解出
            //int count = CoinChange(coins, amount);
            //int count = CoinChange2(coins, amount);
            int count = LeetCodeApproach1(coins, amount);
            Console.WriteLine("count: {0}", count);
        }

        private static int CoinChange(int[] coins, int amount)
        {
            Array.Sort(coins);

            int index = coins.Length - 1;
            int totalCount = 0;

            // 外層 loop 處理面額
            // 內層 loop 處理 amount 扣除
            while (index >= 0)
            {
                // 目前的 amount 不會被面額減成負數
                while (coins[index] <= amount)
                {
                    amount -= coins[index];
                    Console.WriteLine("minus: {0},  remain: {1}", coins[index], amount);
                    totalCount++;
                }
                if (amount == 0)
                {
                    break;
                }
                index--;
            }

            if (amount != 0)
            {
                return -1;
            }
            return totalCount;
        }

        /// <summary>
        /// 嘗試 dp 解
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static int CoinChange2(int[] coins, int amount)
        {
            // todo: 假如我先把 coins array 排序，下面的內層迴圈 loop 由大到小測試貨幣
            // todo: 只要較大的貨幣找到解了，就可以直接終止嘗試貨幣，因為較大的貨幣等於使用較少的數量

            // dp[i] 表示 amount 為 i 時，最少數量的硬幣
            int[] dp = new int[amount + 1];
            dp[0] = 0;

            // 運算過程中，扣除後剩下的餘額，有成功兌換完畢的話會是 0
            int[] surplus = new int[amount + 1];
            Array.Fill(surplus, -1);
            surplus[0] = 0;

            // int backupOriginalAmount = amount;
            for (int i = 1; i <= amount; i++)
            {
                int subAmount = i;
                int minCount = Int32.MaxValue;

                for (int k = 0; k < coins.Length; k++)
                {
                    // 需考慮目前的一枚 coins[k] 就能扣光 amount 的 case
                    if (coins[k] == subAmount)
                    {
                        minCount = 1;
                        surplus[i] = 0;
                        break;
                    }
                    // 先從一枚硬幣開始
                    int curCoinCount = 1;
                    // 不能減到負數
                    while (coins[k] * curCoinCount <= subAmount)
                    {
                        // 確定這個前面的問題有成功解， dp[subAmount - coins[k] * curCoinCount] 有成功解
                        if (surplus[subAmount - coins[k] * curCoinCount] == 0)
                        {
                            minCount = Math.Min(minCount, curCoinCount + dp[subAmount - coins[k] * curCoinCount]);
                            surplus[i] = 0;
                        }
                        curCoinCount++;
                    }
                }
                dp[i] = minCount;
            }

            if (surplus[amount] != 0)
            {
                return -1;
            }
            return dp[amount];
        }


        /// <summary>
        /// brute force [Time Limit Exceeded]，用 backtracking 解
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static int LeetCodeApproach1(int[] coins, int amount)
        {
            return Solve(0, coins, amount);
        }
        private static int Solve(int idxCoin, int[] coins, int amount)
        {
            if (amount == 0)
            {
                return 0;
            }

            if (idxCoin < coins.Length && amount > 0)
            {
                // 目前這枚硬幣的面額，在不超過 amount 的情況下，最多可用幾枚
                int maxVal = amount / coins[idxCoin];
                // minCost 表示最少硬幣的數量
                int minCost = Int32.MaxValue;
                // loop 範圍為 1 ~ 總額不超過 amount 的硬幣數
                for (int x = 0; x <= maxVal; x++)
                {
                    // 目前硬幣面額 乘上 x枚，小於等於 amount
                    if (amount >= x * coins[idxCoin])
                    {
                        int res = Solve(idxCoin + 1, coins, amount - x * coins[idxCoin]);

                        // res 為 -1 的話，表示上面那一條 遞迴搭配的結果是失敗的，無法扣完 amount
                        if (res != -1)
                        {
                            minCost = Math.Min(minCost, res + x);
                        }
                    }
                }
                return (minCost == Int32.MaxValue) ? -1 : minCost;
            }
            // 超出 coins array index 範圍 也直接 return -1
            return -1;
        }

        /// <summary>
        /// dp 解，top - down 方法
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static int LeetCodeApproach2(int[] coins, int amount)
        {
            if (amount < 1)
            {
                return 0;
            }
            return Solve2(coins, amount, new int[amount]);
        }
        private static int Solve2(int[] coins, int rem, int[] count)
        {
            if (rem < 0)
            {
                return -1;
            }
            if (rem == 0)
            {
                return 0;
            }
            // 為何這邊 rem 還要再 -1 ?
            // 幹，因為陣列長度剛好是 amount，沒有設為 + 1
            // 導致這邊索引還要調一下常能對到相對應的....
            if (count[rem - 1] != 0)
            {
                return count[rem - 1];
            }
            int min = Int32.MaxValue;
            foreach (int coin in coins)
            {
                // rem 為目前餘額，扣掉當前硬幣面額後，再傳入下一回遞迴呼叫
                int res = Solve2(coins, rem - coin, count);
                if (res >= 0 && res < min)
                {
                    // 因為上面只會扣一枚硬幣，所以這邊也只會加 1
                    min = res + 1;
                }
            }
            count[rem - 1] = (min == Int32.MaxValue) ? -1 : min;
            return count[rem - 1];
        }

        /// <summary>
        /// dp 解，bottom - up 方法
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static int LeetCodeApproach3(int[] coins, int amount)
        {
            // max 假設前提，硬幣最小面額是 1，不會有比 1 更小的面額了，所以 max 設此值(面額小於 1 的 case)
            int max = amount + 1;
            int[] dp = new int[amount + 1];
            Array.Fill(dp, max);
            for (int i = 1; i <= amount; i++)
            {
                for (int j = 0; j < coins.Length; j++)
                {
                    // 跟我的解差異在於，他每次當枚硬幣只會用一次，
                    // 我的話則是從一枚，漸漸增加到上限去試
                    // 是因為當枚硬幣只用一枚，理論上就可以算是最少數量了嗎?
                    // 假如當枚硬幣用到兩枚(含)以上，就已經沒嘗試價值了嗎???
                    if (coins[j] <= i)
                    {
                        dp[i] = Math.Min(dp[i], dp[i - coins[j]] + 1);
                    }
                }
            }
            return (dp[amount] > amount) ? -1 : dp[amount];
        }


    }
}
