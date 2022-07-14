using System;
using System.Collections.Generic;

namespace _226._Invert_Binary_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/invert-binary-tree/

            // 把二元樹左右翻轉
            // Google: 90% of our engineers use the software you wrote (Homebrew), but you can’t invert a binary tree on a whiteboard so fuck off.
            /* 
               input:
                 4
               /   \
              2     7
             / \   / \
            1   3 6   9

            */

            /*
               output:
                 4
               /   \
              7     2
             / \   / \
            9   6 3   1

            */

            TreeNode root = BuildTree();
            //TreeNode result = InvertTree(root);
            //TreeNode result = InvertTree2(root);
            TreeNode result = InvertTree3(root);
            // 列印結果就用 BFS 的方式來做吧
            ShowResult(result);
        }

        #region 額外建議立一個 clone tree
        // 額外建議立一個 clone tree，
        // 原本的 tree 使用 pre order 走訪，
        // clone tree 針對 origin tree 的走訪，
        // 跟著建立自己的 node，但是要左右相反
        // 缺點: 要額外空間
        private static TreeNode InvertTree(TreeNode root)
        {
            if(root == null)
            {
                return root;
            }

            TreeNode clone = new TreeNode(root.val);
            Solve(root, clone);
            return clone;
        }
        private static void Solve(TreeNode origin, TreeNode clone)
        {
            if(origin.left != null)
            {
                clone.right = new TreeNode(origin.left.val);
                Solve(origin.left, clone.right);
            }

            if(origin.right != null)
            {
                clone.left = new TreeNode(origin.right.val);
                Solve(origin.right, clone.left);
            }
        }
        #endregion

        private static TreeNode InvertTree2(TreeNode root)
        {
            if(root == null)
            {
                return null;
            }

            TreeNode right = InvertTree2(root.right);
            TreeNode left = InvertTree2(root.left);

            root.left = right;
            root.right = left;

            return root;
        }



        // 想辦法不透過建立額外的 clone tree，而是直接替換左右兩邊
        private static TreeNode InvertTree3(TreeNode root)
        {
            if(root == null)
            {
                return null;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while(queue.Count > 0)
            {
                TreeNode current = queue.Dequeue();
                
                // current 的左右子樹對調
                TreeNode temp = current.left;
                current.left = current.right;
                current.right = temp;

                if(current.left != null)
                {
                    queue.Enqueue(current.left);
                }
                if (current.right != null)
                {
                    queue.Enqueue(current.right);
                }
            }

            return root;
        }



        private static TreeNode BuildTree()
        {
            TreeNode root = new TreeNode(4);
            TreeNode node2 = new TreeNode(2);
            TreeNode node7 = new TreeNode(7);

            root.left = node2;
            root.right = node7;

            TreeNode node1 = new TreeNode(1);
            TreeNode node3 = new TreeNode(3);

            node2.left = node1;
            node2.right = node3;

            TreeNode node6 = new TreeNode(6);
            TreeNode node9 = new TreeNode(9);

            node7.left = node6;
            node7.right = node9;

            return root;
        }
        private static void ShowResult(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            string result = string.Empty;

            while(queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                if(node != null)
                {
                    result += node.val;
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
            }
            Console.WriteLine("result: {0}", result);
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
