using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExpressionAddOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/expression-add-operators/

            string num = "123";
            int target = 6;

            num = "232";
            target = 8;

            num = "105";
            target = 5;

            num = "00";
            target = 0;

            num = "3456237490";
            target = 9191;

            num = "123456789";
            target = 45;

            IList<string> results = AddOperators3(num, target);
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }

            // "123-4*5+6-7-89"
            // "12+3*4*5-6-78+9"
            // 連續兩個(含)以上的乘法運算會出錯，表示遇到乘法時，要找上一個運算，直到非乘法時，停下來.....
            // List<string> e = new List<string> { "12", "+", "3", "*", "4", "*", "5", "-", "6", "-", "78", "+", "9" };
            // // e = new List<string> { "123", "-", "4", "*", "5" };
            // bool b = IsCorrectExpression(e, 45);
            // Console.WriteLine("b: {0}", b);

        }
        private static IList<string> AddOperators(string num, int target)
        {
            List<string> results = new List<string>();

            // num 切分出所有可能的數字排列組合
            // 最少兩個數字，2 ~ num.length 個數字
            // no leading zero
            IList<IList<uint>> numberCombinations = GetPossibleNumberCombinations(num);
            foreach (IList<uint> numberCombination in numberCombinations)
            {
                IList<string> operatorCombination = GetOperatorCombinations(numberCombination);
                results.AddRange(operatorCombination);
            }

            IList<string> ans = new List<string>();
            foreach (string result in results)
            {
                List<string> expression = ConvertExpressionToArray(result);
                if (IsCorrectExpression(expression, target))
                {
                    ans.Add(result);
                }
            }

            return ans;
        }
        private static IList<IList<uint>> GetPossibleNumberCombinations(string num)
        {
            IList<IList<uint>> results = new List<IList<uint>>();
            SolveCombination(num, new List<uint>(), results);
            return results;
        }
        private static void SolveCombination(string num, IList<uint> combination, IList<IList<uint>> results)
        {
            // 數字切完了，且組合至少要有兩個數字
            if (num.Length == 0 && combination.Count > 1)
            {
                results.Add(new List<uint>(combination));
                return;
            }

            for (int i = 1; i <= num.Length; i++)
            {
                // 要小心切的範圍不要超過邊界
                if (i > num.Length)
                {
                    return;
                }

                string number = num.Substring(0, i);
                if (IsValidNumber(number))
                {
                    combination.Add(Convert.ToUInt32(number));
                    SolveCombination(num.Substring(i), combination, results);
                    combination.RemoveAt(combination.Count - 1);
                }
                else
                {
                    // 無效數字的可能性只有 leading zero，所以這邊應該可以直接 return
                    return;
                }


            }
        }

        private static IList<string> GetOperatorCombinations(IList<uint> numberCombination)
        {
            IList<string> results = new List<string>();
            SolveOperatorCombination(results, numberCombination, 0, string.Empty);
            return results;
        }
        private static void SolveOperatorCombination(
            IList<string> combinations,
            IList<uint> numberCombination,
            int start,
            string operatorCombinations)
        {
            // 先加數字
            operatorCombinations += numberCombination[start];
            // 假如已經加到最後一個數字
            if (start == numberCombination.Count - 1)
            {
                combinations.Add(operatorCombinations);
                return;
            }

            char[] operators = new char[] { '+', '-', '*' };
            for (int i = 0; i < operators.Length; i++)
            {
                operatorCombinations += operators[i];
                SolveOperatorCombination(combinations, numberCombination, start + 1, operatorCombinations);
                operatorCombinations = operatorCombinations.Substring(0, operatorCombinations.Length - 1);
            }
        }

        private static bool IsValidNumber(string num)
        {
            // 大於個位數時，確定開頭不是 0
            if (num.Length > 1)
            {
                if (num.StartsWith("0"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        private static List<string> ConvertExpressionToArray(string expression)
        {
            // 只取出數字
            string[] operands = expression.Split(new char[] { '+', '-', '*' });

            // 只取出 + - x
            List<string> operators = new List<string>();
            foreach (char c in expression)
            {
                if (c == '+' || c == '-' || c == '*')
                {
                    operators.Add(c.ToString());
                }
            }

            // 融合數字和 + - x
            List<string> expressions = new List<string>();
            int i = 0, j = 0;
            while (expressions.Count < operands.Length + operators.Count)
            {
                // Console.WriteLine("expressions.Count: {0}", expressions.Count);
                // Console.WriteLine("operands.Length: {0}", operands.Length);
                // Console.WriteLine("operators.Count: {0}", operators.Count);
                // 不要超界
                if (i < operands.Length)
                {
                    expressions.Add(operands[i++]);
                }
                if (j < operators.Count)
                {
                    expressions.Add(operators[j++]);
                }
            }
            return expressions;
        }

        private static bool IsCorrectExpression(List<string> expression, int target)
        {
            // todo: "123-4*5+6-7-89" 不等於 45，但被此 method 判別正確
            uint ans = 0;
            int i = 0;

            // 先塞第一個數字
            ans = Convert.ToUInt32(expression[0]);
            i = 1;
            // 目標是運算符號，數字就用 +-1 來拿
            while (i < expression.Count)
            {
                string s = expression[i];
                if (s == "*")
                {
                    uint prevNum = Convert.ToUInt32(expression[i - 1]);
                    uint nextNum = Convert.ToUInt32(expression[i + 1]);
                    // 這邊應該看還要判斷前面是 +-* 哪個
                    // ans -= prevNum;
                    // 這邊應該看還要判斷前面是 +-* 哪個
                    // 假如前面是 *，那就不需要先+-數字回來，往前找注意不要超界
                    // ans += (prevNum * nextNum);

                    // 注意不要超界
                    // 還在範圍內
                    if (i - 2 >= 0)
                    {
                        string prevOperator = expression[i - 2];
                        if (prevOperator == "*")
                        {
                            // 直接繼續乘就好
                            ans *= Convert.ToUInt32(expression[i + 1]);
                        }
                        else
                        {
                            // 先加減回來
                            if (prevOperator == "+")
                            {
                                ans -= prevNum;
                                ans += prevNum * nextNum;
                            }
                            if (prevOperator == "-")
                            {
                                ans += prevNum;
                                ans -= prevNum * nextNum;
                            }
                        }
                    }
                    else
                    {
                        // 代表是第一個運算符號，所以直接乘就好
                        ans *= Convert.ToUInt32(expression[i + 1]);
                    }
                }
                else
                {
                    // 最後一個絕對是數字，現在是 + 或 -，
                    // 所已確定至少一定還有下一個 i
                    // 所以這邊就放心 i + 1 吧
                    if (s == "+")
                    {
                        ans += Convert.ToUInt32(expression[i + 1]);
                    }
                    else
                    {
                        ans -= Convert.ToUInt32(expression[i + 1]);
                    }
                }
                i += 2;
            }
            Console.WriteLine("ans: {0}", ans);
            return ans == target;
        }
        private static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*';
        }
        private static bool IsOperator(string s)
        {
            return s == "+" || s == "-" || s == "*";
        }

        private static int gTarget;
        private static string gDigits;
        private static List<string> gAnswer;
        private static IList<string> AddOperators2(string num, int target)
        {
            if (num.Length == 0)
            {
                return new List<string>();
            }
            gTarget = target;
            gDigits = num;
            gAnswer = new List<string>();
            Recurse(0, 0, 0, 0, new List<string>());
            return gAnswer;
        }
        private static void Recurse(int index, long previousOperand, long currentOperand, long value, List<string> ops)
        {
            string nums = gDigits;
            // Done processing all the digits in num
            if (index == nums.Length)
            {
                // If the final value == target expected AND
                // no operand is left unprocessed
                if (value == gTarget && currentOperand == 0)
                {
                    StringBuilder sb = new StringBuilder();
                    List<string> subList = ops.GetRange(1, ops.Count - 1);
                    foreach (string s in subList)
                    {
                        sb.Append(s);
                    }
                    gAnswer.Add(sb.ToString());
                }
                return;
            }

            // Extending the current operand by one digit
            // currentOperand = currentOperand * 10 + Character.getNumericValue(nums.charAt(index));
            currentOperand = currentOperand * 10 + (int)nums[index];

            // String current_val_rep = Long.toString(currentOperand);
            string currentValRep = currentOperand.ToString();
            int length = nums.Length;

            // To avoid cases where we have 1 + 05 or 1 * 05 since 05 won't be a
            // valid operand. Hence this check
            if (currentOperand > 0)
            {
                // NO OP recursion
                Recurse(index + 1, previousOperand, currentOperand, value, ops);
            }

            // ADDITION
            // ops.remove(ops.size() - 1);
            // ops.remove(ops.size() - 1);

            ops.Add("+");
            ops.Add(currentValRep);
            Recurse(index + 1, currentOperand, 0, value + currentOperand, ops);
            ops.RemoveAt(ops.Count - 1);
            ops.RemoveAt(ops.Count - 1);

            if (ops.Count > 0)
            {
                // SUBTRACTION
                ops.Add("-");
                ops.Add(currentValRep);
                Recurse(index + 1, -currentOperand, 0, value - currentOperand, ops);
                // ops.remove(ops.size() - 1);
                // ops.remove(ops.size() - 1);
                ops.RemoveAt(ops.Count - 1);
                ops.RemoveAt(ops.Count - 1);

                // MULTIPLICATION
                ops.Add("*");
                ops.Add(currentValRep);
                Recurse(
                    index + 1,
                    currentOperand * previousOperand,
                    0,
                    value - previousOperand + (currentOperand * previousOperand),
                    ops);
                // ops.remove(ops.size() - 1);
                // ops.remove(ops.size() - 1);
                ops.RemoveAt(ops.Count - 1);
                ops.RemoveAt(ops.Count - 1);
            }
        }

        private static IList<string> AddOperators3(string num, int target)
        {
            IList<string> rst = new List<string>();
            if (num == null || num.Length == 0)
            {
                return rst;
            }
            Helper(rst, "", num, target, 0, 0, 0);
            return rst;
        }

        ///
        /// path: 目前的運算式
        /// pos: 目前開始處理 num 字串的 index 位置
        /// eval: 目前運算式計算的結果
        /// multed: 遇到乘法符號的處理數字，上一個 Helper stack 擷取的數字
        private static void Helper(IList<string> rst, string path, string num, int target, int pos, long eval, long multed)
        {
            // 已掃完 num 字串
            if (pos == num.Length)
            {
                // 運算式結果和 target 一樣
                if (target == eval)
                    rst.Add(path);
                return;
            }

            for (int i = pos; i < num.Length; i++)
            {
                // i != pos 表示目前要截取的數字大於等於兩位數
                // 因為數字大於等於兩位數，所以要檢查最前面是否為 0
                // 發現兩位數以上，數字最前面為 0 的數字，規則: no leading zero
                if (i != pos && num[pos] == '0')
                {
                    // no leading zero
                    break;
                }
                // cur: 現在截出的數字，範圍為 索引 pos 到 (i - pos - 1)
                long cur = Convert.ToInt64(num.Substring(pos, i - pos + 1));

                // 先取數字，再決定數字前面是 加 或 減 或 乘，再來運算
                if (pos == 0)
                {
                    // 這是開頭第一個數字，直接加上去就好
                    Helper(rst, path + cur, num, target, i + 1, cur, cur);
                }
                else
                {
                    Helper(rst, path + "+" + cur, num, target, i + 1, eval + cur, cur);

                    Helper(rst, path + "-" + cur, num, target, i + 1, eval - cur, -cur);
                    // 假如遇到連續乘法運算的話，最後一個參數 multed * cur 會一直累積乘下去
                    // 下一輪遇到加或減的話，multed 會重新 init 為當前截取的數字
                    
                    // 思考點，上一輪運算是加或減，接下來遇到一到多個乘法運算
                    Helper(rst, path + "*" + cur, num, target, i + 1, eval - multed + multed * cur, multed * cur);
                    // 2+2*3*4+5
                    // 2+2 = 4 , multed = 2
                    // 2+2*3 =  4-2 + (2*3) = 8, multed = 2*3
                    // 2+2*3*4 = 8 - 6 + (6*4) = 26 multed = 6*4
                    // 2+2*3*4+5 = 26 + 5 = 31 multed
                }
            }
        }



    }
}
