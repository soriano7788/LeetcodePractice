using System;
using System.Collections.Generic;

namespace _144._Binary_Tree_Preorder_Traversal
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/binary-tree-preorder-traversal/

            TreeNode root = BuildTree();
            IList<int> result = PreorderTraversal(root);
            Console.WriteLine("result: {0}", string.Join(", ", result));
        }
        private static IList<int> PreorderTraversal(TreeNode root)
        {
            #region 遞迴處理的方式
            //IList<int> result = new List<int>();
            //PreorderTraverse(root, result);
            //return result;
            #endregion

            // 可以用 stack 或 queue 來做?

            // todo: 應該可以用 queue 來做沒錯?
            // 首先，把 rootNode enqueue，
            // 再來，從 queue 中 dequeue，(馬上紀錄 node 的 value)
            // 把取出的 node 的 leftNode 先 enqueue，再把 rightNode enqueue

            // todo: 上述應該是錯的， preorder 應該是類似 DFS(深度優先搜尋)，用 stack 做，
            // queue 應該是類似 BFS(廣度優先搜尋)
            // 以 stack 實作，
            // 一開始，先把 root push 進 stack，
            // 接下來，從 stack pop 一個 node 出來，
            // 立刻把 node 的 value 加進 result，
            // 然後依序把 node 的 rightChild 和 leftChild push 進 stack
            // 最後 開始從 stack pop，pop 出來的 node 立刻把他的 value 加進 result，
            // 接下來就是依循這個步驟，直到 stack isEmpty
            // 注意: pop 出來時記得檢查一下 node 是否為 null，因為在 push 進去前，沒有檢查是否為 null

            IList<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while(stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                if(node != null)
                {
                    result.Add(node.val);
                    stack.Push(node.right);
                    stack.Push(node.left);
                }
            }
            return result;
        }

        private static void PreorderTraverse(TreeNode node, IList<int> records)
        {
            if(node != null)
            {
                records.Add(node.val);
                PreorderTraverse(node.left, records);
                PreorderTraverse(node.right, records);
            }
        }

        private static TreeNode BuildTree()
        {
            TreeNode root = new TreeNode(1);
            TreeNode node2 = new TreeNode(2);
            TreeNode node3 = new TreeNode(3);

            root.right = node2;
            node2.left = node3;

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
