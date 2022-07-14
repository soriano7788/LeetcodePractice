using System;
using System.Collections.Generic;

namespace _145._Binary_Tree_Postorder_Traversal
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/binary-tree-postorder-traversal/
            // 後序走訪
            // 順位為: node left child -> node right child -> node


            // 有使用 queue 或 stack 的解法嗎?
            // 用 stack 的話要記得，想要最先印出來的 node，就要最晚 push 進 stack

            TreeNode root = BuildTree();
            //IList<int> result = PostorderTraversal(root);
            IList<int> result = PostorderTraversal2(root);
            Console.WriteLine("result: {0}", string.Join(", ", result));
        }

        #region 遞迴解法
        private static IList<int> PostorderTraversal(TreeNode root)
        {
            IList<int> result = new List<int>();
            PostorderTraverse(root, result);
            return result;
        }
        private static void PostorderTraverse(TreeNode node, IList<int> records)
        {
            if(node != null)
            {
                PostorderTraverse(node.left, records);
                PostorderTraverse(node.right, records);
                records.Add(node.val);
            }
        }
        #endregion

        private static IList<int> PostorderTraversal2(TreeNode root)
        {
            // 用 stack 的話要記得，想要最先印出來的 node，就要最晚 push 進 stack
            // 順位為: node left child -> node right child -> node
            IList<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();

            TreeNode node = root;
            while(node != null || stack.Count != 0)
            {
                if(node != null)
                {
                    if(node.right != null)
                    {

                    }


                }
                else
                {
                    node = stack.Pop();
                }
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
