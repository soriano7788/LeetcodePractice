using System;
using System.Collections.Generic;

namespace _257._Binary_Tree_Paths
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/binary-tree-paths/
            // input 一個二元樹，找出從樹的 root 到 leaf 的所有可能路徑

            // Output: ["1->2->5", "1->3"]
            TreeNode root = BuildTree();
            //IList<string> result = BinaryTreePaths(root);
            //IList<string> result = BinaryTreePaths2(root);
            IList<string> result = BinaryTreePaths3(root);

            foreach (string r in result)
            {
                Console.WriteLine(r);
            }

        }


        #region 遞迴 + backtracking
        private static IList<string> BinaryTreePaths(TreeNode root)
        {
            IList<string> result = new List<string>();

            if (root == null)
            {
                return result;
            }

            Solve(root, root.val.ToString(), result);

            return result;
        }
        private static void Solve(TreeNode node, string currentPath, IList<string> result)
        {
            if (node.left != null)
            {
                Solve(node.left, currentPath + "->" + node.left.val, result);
            }

            if (node.right != null)
            {
                Solve(node.right, currentPath + "->" + node.right.val, result);
            }

            if (node.left == null && node.right == null)
            {
                result.Add(currentPath);
            }
        }
        #endregion

        #region BFS with queue
        private static IList<string> BinaryTreePaths2(TreeNode root)
        {
            IList<string> result = new List<string>();
            Queue<TreeNode> queue = new Queue<TreeNode>();
            Queue<string> queueStr = new Queue<string>();

            if(root == null)
            {
                return result;
            }

            queue.Enqueue(root);
            queueStr.Enqueue("");

            while(queue.Count > 0)
            {
                TreeNode currentNode = queue.Dequeue();
                string currentStr = queueStr.Dequeue();

                if (currentNode.left == null && currentNode.right == null)
                {
                    result.Add(currentStr + currentNode.val);
                }

                if(currentNode.left != null)
                {
                    queue.Enqueue(currentNode.left);
                    queueStr.Enqueue(currentStr + currentNode.val + "->");
                }

                if (currentNode.right != null)
                {
                    queue.Enqueue(currentNode.right);
                    queueStr.Enqueue(currentStr + currentNode.val + "->");
                }
            }

            return result;
        }
        #endregion


        #region DFS with stack
        private static IList<string> BinaryTreePaths3(TreeNode root)
        {
            IList<string> result = new List<string>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            Stack<string> stackStr = new Stack<string>();

            if(root == null)
            {
                return result;
            }

            stack.Push(root);
            stackStr.Push("");

            while(stack.Count > 0)
            {
                TreeNode currentNode = stack.Pop();
                string currentStr = stackStr.Pop();

                if(currentNode.left == null && currentNode.right == null)
                {
                    result.Add(currentStr + currentNode.val);
                }

                if(currentNode.left != null)
                {
                    stack.Push(currentNode.left);
                    stackStr.Push(currentStr + currentNode.val + "->");
                }
                if (currentNode.right != null)
                {
                    stack.Push(currentNode.right);
                    stackStr.Push(currentStr + currentNode.val + "->");
                }
            }

            return result;
        }
        #endregion


        private static TreeNode BuildTree()
        {
            TreeNode root = new TreeNode(1);

            TreeNode node2 = new TreeNode(2);
            TreeNode node3 = new TreeNode(3);

            root.left = node2;
            root.right = node3;

            TreeNode node5 = new TreeNode(5);

            node2.right = node5;

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
    };
}
