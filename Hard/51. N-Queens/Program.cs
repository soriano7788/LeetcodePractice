using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _51._N_Queens
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/n-queens/

            // C# 解法 (判斷條件和結構超難讀)
            // https://leetcode.com/problems/n-queens/discuss/423775/C-Solution


            // C# 解法2
            // https://leetcode.com/problems/n-queens/discuss/2012545/C-solution

            // 1 <= n <= 9

            // 最內部的第一列是用 string 組成的。
            // 任何一個 queen 都不可以與其他 queen 在同一行以及同一列上

            // (X)這其實就跟數獨的同一個數字不可同行同列的規則一樣 (只差不可在同一個9宮格內)
            // queen 還可以45度斜方向攻擊

            // 思考步驟
            // 1. 從第一列往下依序 每一列 放置一個 queen
            // 2. 從第二列開始，要下棋前，先檢查目前想放的位置會不會可以攻擊到上面的棋子，確定不會的話 才放下去
            // 3. 成功到第 N 列也放完棋子後，把當下的結果複製一份存下來




            int n = 8;
            var results = SolveNQueens3(n);

            // 其實可以想成只有一列，然後一個一個占滿的排列組合有幾種??? 好像又怪怪的??
            // 我錯了，queen 還可以斜線攻擊.... 幹!!!

            Console.WriteLine("Hello World!");
        }



        #region Solution 1
        private static IList<IList<string>> SolveNQueens2(int n)
        {
            var result = new List<IList<string>>();
            Dfs(new int[n], 0, result);
            return result;
        }

        // n 是指現在要下第幾個 row 的棋
        // board 的 length 是 n
        private static void Dfs(int[] board, int n, List<IList<string>> list)
        {
            // 假如是8X8，board.Length一直都會是 8，n 比較算是 row index
            // 只用一維陣列board 來記錄 大概是因為，反正不同 row 的 queen 絕對不可以在同一個 column 上?
            // 已全滿，加入最終解
            if (n == board.Length)
            {
                list.Add(ConvertToResult(board));
                return;
            }


            for (int i = 0; i < board.Length; i++)
            {
                if (IsValid(board, n, i))
                {
                    board[n] = i;
                    Dfs(board, n + 1, list);
                }
            }
        }

        // index 表示的是現在要嘗試的 rowIndex
        // val 表示目前想放置 新的 queen 的位置
        private static bool IsValid(int[] board, int index, int val)
        {
            // board[i]說明，i 表示 row，board[i] 的值表示第 i row 的 queen 在哪個 xIndex

            for (int i = 0; i < index; i++)
            {
                // 應該是檢查 正上方、左上方、右上方? 但是超難讀

                // (board[i] == val) 檢查正上方
                // (i + board[i] == val + index)  old queen 在左上角，new queen 在右下角，可以互相攻擊?
                // (i - board[i] == index - val)

                if (i + board[i] == val + index || i - board[i] == index - val || board[i] == val)
                {
                    return false;
                }
            }
            return true;
        }

        private static List<string> ConvertToResult(int[] board)
        {
            var result = new List<string>();
            for (int i = 0; i < board.Length; i++)
            {
                // 先全部填 .
                var charArray = Enumerable.Repeat('.', board.Length).ToArray();
                // 再把 queen 按照指定位置填入
                charArray[board[i]] = 'Q';
                result.Add(new string(charArray));
            }

            return result;
        }
        #endregion


        #region Solution2
        private static IList<IList<string>> SolveNQueens3(int n)
        {
            List<IList<string>> res = new List<IList<string>>();

            // 初始化 棋盤
            int[][] grid = new int[n][];
            for (int i = 0; i < n; i++)
            {
                grid[i] = new int[n];
            }
            solveNQueens(0, grid, res);
            return res;
        }

        // grid 就是目前沙盤推演的擺法
        // 1 表示放下 queen，0 表示 empty
        private static void solveNQueens(int row, int[][] grid, List<IList<string>> res)
        {
            if (row == grid.Length)
            {
                // 產出問題要求的格式
                List<string> solution = generateSolution(grid);
                res.Add(solution);
            }
            else
            {
                // 嘗試每一個 colIndex
                for (int col = 0; col < grid[0].Length; col++)
                {
                    if (canPlace(row, col, grid))
                    {
                        grid[row][col] = 1;
                        solveNQueens(row + 1, grid, res);
                        grid[row][col] = 0;

                        // 其實不用管 grid 實際內容有沒有塞滿，只要某一個遞迴成功走到第 n row 就得到解了!!!
                    }
                }
            }
        }

        // 檢查現在要放置的 row, col 是否可以放(就是不會讓 queen 之間可以互相攻擊)
        private static bool canPlace(int row, int col, int[][] grid)
        {
            // 這裡的判斷有沒有辦法寫得好讀一點?

            // i 是 row，j 是 col
            int i = 0, j = 0;
            // loop 從最上面的 row 往下，檢查有沒有在同一條直線正上方
            while (i != row)
            {
                if (grid[i++][col] == 1)
                    return false;
            }

            // 初始化為 新位置的45度左上方
            i = row - 1; j = col - 1;
            // 不斷往左上移動，看會不會撞到之前的 queen，當然出界的話就不用繼續 loop 了
            while (i >= 0 && j >= 0)
            {
                if (grid[i--][j--] == 1)
                    return false;
            }

            // 初始化為 新位置的45度右上方
            i = row - 1; j = col + 1;
            while (i >= 0 && j < grid[0].Length)
            {
                if (grid[i--][j++] == 1)
                    return false;
            }
            return true;
        }
        private static List<string> generateSolution(int[][] grid)
        {
            // 把 grid[][] 轉換成 題目指定的格式

            List<string> res = new List<string>();
            for (int i = 0; i < grid.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 0)
                        sb.Append('.');
                    else
                        sb.Append('Q');
                }
                res.Add(sb.ToString());
            }
            return res;
        }

        #endregion


        #region XXX
        private static IList<IList<string>> SolveNQueens(int n)
        {
            IList<IList<string>> result = new List<IList<string>>();

            // init
            List<bool> queenOccupy = new List<bool>();
            for (int i = 1; i <= n; i++)
            {
                queenOccupy.Add(false);
            }

            Recursive(n, result, new List<string>(), 0, queenOccupy.ToArray());

            return result;
        }

        // curCount 是指目前下了幾個 queen
        private static void Recursive(
            int n, 
            IList<IList<string>> chessboards,
            List<string> chessboard, 
            int curCount,
            bool[] queenOccupy)
        {
            // param 還需要有一個紀錄目前還有哪個 col 可用的統計

            if(curCount == n)
            {
                return;
            }



            // 最終確認可行的解，要利用複製的方式，存入chessboards
        }

        private static int GetCanSetQueenPosition(bool[] queenOccupy)
        {
            // 沒位置的話，就回傳 -1
            for(int i = 0; i < queenOccupy.Length; i++)
            { }
            return 0;

        }

        // 判斷現在是否還有辦法下新的 queen
        private static bool CanSetNewQueen(List<string> chessboard, int n)
        {
            // 先蒐集整理現有的 queens
            List<int[]> points = new List<int[]>();
            for (int i = 0; i < chessboard.Count; i++)
            {
                points.Add(new int[] { chessboard.IndexOf("Q"), i });
            }

            // 同 row 是否有
            // 同 col 是否有
            // top left exist?
            // top right exist?
            // bottom left exist?
            // bottom right exist?

            // 需判斷 舊棋 和 現在嘗試下的新棋位置的相對位置

            // 新棋一定在下方，所以只需考慮左右就好
            //
            int newChessX = 0;
            int newChessY = chessboard.Count;
            for (int i = 0; i < n; i++)
            {
                // 找出新的 row 是否可下新棋
                newChessX = i;

                // 現有已下的棋都要比對
                foreach(var p in points)
                {
                    int oldX = p[0], oldY = p[1];

                    // 垂直攻擊
                    if(oldX == newChessX)
                    {
                        return false;
                    }

                    // 新棋在右邊
                    if(oldX < newChessX)
                    { }
                    // 新棋在左邊
                    if (oldX > newChessX)
                    { }

                }

                
            }

            return true;
        }
        private static bool CanInclineAttackEach(int x1, int y1, int x2, int y2)
        {
            // 1 在下
            if(y1 > y2)
            {
                if(x1 > x2)
                { }
            }

            // 1 在上
            if(y1 < y2)
            {

            }
            return false;
        }


        private static string GenerateRowString(int n, int x)
        {
            // n 表示這是 n*n 的棋盤
            // x 表示 queen 要下在第幾個位置

            return string.Empty;
        }

        #endregion
    }
}
