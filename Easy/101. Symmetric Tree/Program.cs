using System;
using System.Collections.Generic;

namespace _101._Symmetric_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/symmetric-tree/

            // 檢查此二元樹是否為對稱二元樹，
            // 就是二元樹從中間切一半，會是左右對稱，跟鏡子一樣

            //TreeNode root = BuildTree1();
            TreeNode root = BuildTree2();

            bool result = IsSymmetric(root);
            Console.WriteLine("result: {0}", result);

        }
        private static bool IsSymmetric(TreeNode root)
        {
            // 使用兩個 stack，2. 都使用 preorder 走訪(右邊的記得左右子樹走訪順序要相反)

            Stack<TreeNode> leftStack = new Stack<TreeNode>();
            Stack<TreeNode> rightStack = new Stack<TreeNode>();

            TreeNode leftRoot = root.left;
            TreeNode rightRoot = root.right;

            // 只有其中一個子樹為 null 的話，就代表左右不對稱了
            if((leftRoot == null) ^ (rightRoot == null))
            {
                return false;
            }

            leftStack.Push(leftRoot);
            rightStack.Push(rightRoot);

            while(leftStack.Count > 0 && rightStack.Count > 0)
            {
                TreeNode leftNode = leftStack.Pop();
                TreeNode rightNode = rightStack.Pop();

                if(leftNode != null && rightNode != null)
                {
                    if (leftNode.val != rightNode.val)
                    {
                        return false;
                    }

                    leftStack.Push(leftNode.right);
                    leftStack.Push(leftNode.left);

                    rightStack.Push(rightNode.left);
                    rightStack.Push(rightNode.right);
                }

                if((leftNode == null) ^ (rightNode == null))
                {
                    return false;
                }
            }
            return true;
        }

        private static TreeNode BuildTree1()
        {
            TreeNode root = new TreeNode(1);
            TreeNode leftNode2 = new TreeNode(2);
            TreeNode rightNode2 = new TreeNode(2);

            root.left = leftNode2;
            root.right = rightNode2;

            TreeNode leftNode3 = new TreeNode(3);
            TreeNode rightNode3 = new TreeNode(3);

            TreeNode leftNode4 = new TreeNode(4);
            TreeNode rightNode4 = new TreeNode(4);

            leftNode2.left = leftNode3;
            leftNode2.right = leftNode4;

            rightNode2.left = rightNode4;
            rightNode2.right = rightNode3;

            return root;
        }
        private static TreeNode BuildTree2()
        {
            TreeNode root = new TreeNode(1);
            TreeNode leftNode2 = new TreeNode(2);
            TreeNode rightNode2 = new TreeNode(2);

            root.left = leftNode2;
            root.right = rightNode2;

            TreeNode leftNode3 = new TreeNode(3);
            TreeNode rightNode3 = new TreeNode(3);

            leftNode2.right = leftNode3;
            rightNode2.right = rightNode3;

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
