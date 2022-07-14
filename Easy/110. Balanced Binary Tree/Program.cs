using System;

namespace _110._Balanced_Binary_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/balanced-binary-tree/

            // 判斷 input 是否為為平衡二元樹
            // a binary tree in which the left and right subtrees of every node differ in height by no more than 1.
            // 也就是每個 node 的 左右子樹的 height 不超過 1

            // The number of nodes in the tree is in the range [0, 5000].
            // -104 <= Node.val <= 104

            // Input: root = [3,9,20,null,null,15,7]
            // Output: true

            // 要寫個 method，把上面的 array 轉成 tree?
            // 不然就只能手動建了...

            // 先嘗試印出 前中後序走訪看看??
            TreeNode tree = BuildTree();

            Console.Write("InOrder: ");
            InOrder(tree);
            Console.WriteLine();

            Console.Write("PreOrder: ");
            PreOrder(tree);
            Console.WriteLine();

            Console.Write("PostOrder: ");
            PostOrder(tree);
            Console.WriteLine();

            //int height = GetTreeHeight(tree, 0);
            //Console.WriteLine("height: {0}", height);

            bool isBalanced = IsBalanced(tree);
            Console.WriteLine("isBalanced: {0}", isBalanced);
        }

        private static bool IsBalanced(TreeNode root)
        {
            // 檢查每一個 node，並且把每一個 node 的左右子樹都跑到底一便

            // 需要一個紀錄目前深度的參數嗎? 或是 method

            return TraverseTreeAndDetectIsBalance(root);
        }

        private static int GetTreeHeight(TreeNode node, int curHeight)
        {
            if(node == null)
            {
                return curHeight;
            }
            curHeight++;

            // 需判斷左右 height 是否相差小於等於 1，所以需要兩個 method 搭配嗎?
            return Math.Max(GetTreeHeight(node.left, curHeight), GetTreeHeight(node.right, curHeight));
        }

        private static bool TraverseTreeAndDetectIsBalance(TreeNode node)
        {
            if(node == null)
            {
                // node 是 null，也沒有判斷左右子樹的必要，至少不會是高度落差超過 1 的情形
                return true;
            }

            int leftHeight = GetTreeHeight(node.left, 0);
            int rightHeight = GetTreeHeight(node.right, 0);
            int diff = Math.Abs(leftHeight - rightHeight);
            if(diff > 1)
            {
                return false;
            }

            bool r1 = TraverseTreeAndDetectIsBalance(node.left);
            bool r2 = TraverseTreeAndDetectIsBalance(node.right);

            return r1 && r2;
        }


        private static TreeNode BuildTree()
        {
            // Input: root = [3,9,20,null,null,15,7]
            TreeNode root = new TreeNode(3);
            TreeNode node9 = new TreeNode(9);
            TreeNode node20 = new TreeNode(20);
            TreeNode node15 = new TreeNode(15);
            TreeNode node7 = new TreeNode(7);

            root.left = node9;
            root.right = node20;

            node20.left = node15;
            node20.right = node7;


            return root;
        }

        private static void PreOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            Console.Write("{0} ", node.val);
            PreOrder(node.left);
            PreOrder(node.right);
        }

        private static void InOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            InOrder(node.left);
            Console.Write("{0} ", node.val);
            InOrder(node.right);
        }

        private static void PostOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            PostOrder(node.left);
            PostOrder(node.right);
            Console.Write("{0} ", node.val);
        }


        class TreeNode
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
