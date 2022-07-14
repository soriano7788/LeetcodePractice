using System;

namespace _112._Path_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/path-sum/
            // 給一個二元樹，和一個 targetSum
            // 檢查從這個二元樹的 root 到任意 leaf，
            // 是否存在當中路徑上節點數字的總和為 targetSum，
            // 回傳 true or false

            // Constraints:
            // The number of nodes in the tree is in the range[0, 5000].
            // -1000 <= Node.val <= 1000
            // - 1000 <= targetSum <= 1000

            int targetSum = 22;

            TreeNode root = BuildTree();
            //bool result = HasPathSum(root, targetSum);
            bool result = HasPathSum2(root, targetSum);

            Console.WriteLine("result: {0}", result);
        }

        #region 我的解法，使用 recursive
        private static bool HasPathSum(TreeNode root, int targetSum)
        {
            if(root == null)
            {
                return false;
            }

            // 能夠 return false || true; 這樣的敘述句就可以成功了
            return Solve(root, targetSum, 0);
        }
        private static bool Solve(TreeNode node, int targetSum, int currentSum)
        {
            // 注意，一定要是葉節點才行，也就是 left 和 right 都是 null 才能結算
            // todo: 也可把下面這串 if-else 另外用 method 包起來，只回傳 function 或 class 之類的

            // leaf node，已無左右子樹
            if(node.left == null && node.right == null)
            {
                if(currentSum + node.val == targetSum)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(node.left != null && node.right != null)
            {
                // 左右都有子樹，所以只要左右其中一個子樹路徑存在  targetSum 就符合了，因此用 ||
                return Solve(node.left, targetSum, currentSum + node.val) || Solve(node.right, targetSum, currentSum + node.val);
            }
            else if (node.left != null)
            {
                // 只有左子樹
                return Solve(node.left, targetSum, currentSum + node.val);
            }
            else
            {
                //if (node.right != null)
                // 只有右子樹
                return Solve(node.right, targetSum, currentSum + node.val);
            }
        }
        #endregion

        #region 參考 leetcode 的解法
        // https://leetcode.com/problems/path-sum/discuss/36378/AcceptedMy-recursive-solution-in-Java
        private static bool HasPathSum2(TreeNode root, int targetSum)
        {
            if(root == null)
            {
                return false;
            }

            // 必須是 leaf node，中間層的節點都不可以
            if(root.left == null && root.right == null && targetSum - root.val == 0)
            {
                return true;
            }

            // 不用管 left or right 是否為 null
            // 假如 left and right 並非都是 null，就會執行到這邊，還沒到 leaf 的 sum 沒有意義
            return HasPathSum2(root.left, targetSum - root.val) || HasPathSum2(root.right, targetSum - root.val);
        }
        #endregion


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

            TreeNode node1 = new TreeNode(1);
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
