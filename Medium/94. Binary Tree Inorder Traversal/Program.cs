using System;
using System.Collections.Generic;

namespace _94._Binary_Tree_Inorder_Traversal
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/binary-tree-inorder-traversal/

            TreeNode root = BuildTree();

            //IList<int> result = InorderTraversal(root);
            IList<int> result = InorderTraversal2(root);
            Console.WriteLine("reuslt: {0}", string.Join(", ", result));
        }

        #region 遞迴走訪
        private static IList<int> InorderTraversal(TreeNode root)
        {
            IList<int> result = new List<int>();
            InorderTraverse(root, result);
            return result;
        }
        private static void InorderTraverse(TreeNode node, IList<int> records)
        {
            if(node != null)
            {
                InorderTraverse(node.left, records);
                records.Add(node.val);
                InorderTraverse(node.right, records);
            }
        }
        #endregion

        private static IList<int> InorderTraversal2(TreeNode root)
        {
            IList<int> result = new List<int>();

            // 用 stack 的話，一開始就先嘗試取左右子樹，然後先把右子樹 push，再把自己 push
            // 利用 push 進 stack 的順序，來控制加入 result 的順序

            // node 把自己 push 進 stack 前，先嘗試取出左子樹，依序這樣做，直到沒左子樹後，開始 pop
            // pop 出來時，先把 node value 加入 record，
            // 再開始處理 node 的 right child，使用類似上述的做法

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode node = root;
            while(node != null || stack.Count > 0)
            {
                while(node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }

                node = stack.Pop();
                result.Add(node.val);
                node = node.right;
            }

            return result;
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
