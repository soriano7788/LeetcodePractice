using System;
using System.Collections.Generic;

namespace _113._Path_Sum_II
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/path-sum-ii/
            // 給一個二元樹 和一個 targetSum
            // 找出從 root 到各 leaf 的 path 中，
            // path 上的 node val 總和為 targetSum 的 list
            // 找出所有符合的 list

            TreeNode root = BuildTree();
            int targetSum = 22;

            //IList<IList<int>> results = PathSum(root, targetSum);
            IList<IList<int>> results = PathSum2(root, targetSum);
            Console.WriteLine("result count: {0}", results.Count);

            foreach(IList<int> result in results)
            {
                Console.WriteLine(string.Join(", ", result));
            }
        }

        #region 我的解法(recursive)
        private static IList<IList<int>> PathSum(TreeNode root, int targetSum)
        {
            IList<IList<int>> results = new List<IList<int>>();
            if(root == null)
            {
                return results;
            }
            
            Solve(root, targetSum, 0, new List<int>(), results);

            return results;
        }
        private static void Solve(TreeNode node, int targetSum, int curSum, IList<int> curPath, IList<IList<int>> results)
        {
            // is leaf node
            if(node.left == null && node.right == null)
            {
                if(curSum + node.val == targetSum)
                {
                    curPath.Add(node.val);
                    results.Add(new List<int>(curPath));
                    curPath.RemoveAt(curPath.Count - 1);
                }
                return;
            }

            if (node.left != null)
            {
                curPath.Add(node.val);
                int length = curPath.Count;
                Solve(node.left, targetSum, curSum + node.val, curPath, results);
                // 需要把 curPath 維持住當前階段的狀態，上面那行遞迴往下走可能會多加一些數字進去，這邊要排除掉
                curPath.RemoveAt(length - 1);
            }

            if (node.right != null)
            {
                curPath.Add(node.val);
                int length = curPath.Count;
                Solve(node.right, targetSum, curSum + node.val, curPath, results);
                curPath.RemoveAt(length - 1);
            }
        }
        #endregion

        private static IList<IList<int>> PathSum2(TreeNode root, int targetSum)
        {
            // 參考此解法，但答案不對
            // https://leetcode.com/problems/path-sum-ii/discuss/36695/Java-Solution%3A-iterative-and-recursive

            IList<IList<int>> results = new List<IList<int>>();
            if (root == null)
            {
                return results;
            }

            List<int> path = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            int sum = 0;
            TreeNode current = root;
            TreeNode previous = null;

            while(current != null || stack.Count > 0)
            {
                // 先往左子樹方向鑽到底
                while(current != null)
                {
                    stack.Push(current);
                    path.Add(current.val);
                    sum += current.val;
                    current = current.left;
                }

                // 從 stack 取出一個 node
                current = stack.Pop();

                if (current.right != null && current.right != previous)
                {
                    current = current.right;
                    continue;
                }

                // 已到達 leaf node，且總和符合 target
                if (current.left == null && current.right == null && sum == targetSum)
                {
                    results.Add(new List<int>(path));
                }

                previous = current;
                //stack.Pop();
                path.RemoveAt(path.Count - 1);
                sum -= current.val;
                current = null;
            }

            return results;
        }


        private static TreeNode BuildTree()
        {
            TreeNode root = new TreeNode(5);
            
            TreeNode leftNode4 = new TreeNode(4);
            TreeNode node8 = new TreeNode(8);
            root.left = leftNode4;
            root.right = node8;

            TreeNode node11 = new TreeNode(11);
            leftNode4.left = node11;

            TreeNode node7 = new TreeNode(7);
            TreeNode node2 = new TreeNode(2);
            node11.left = node7;
            node11.right = node2;

            TreeNode node13 = new TreeNode(13);
            TreeNode rightNode4 = new TreeNode(4);
            node8.left = node13;
            node8.right = rightNode4;

            TreeNode node5 = new TreeNode(5);
            TreeNode node1 = new TreeNode(1);
            rightNode4.left = node5;
            rightNode4.right = node1;

            return root;
        }



        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }
}
