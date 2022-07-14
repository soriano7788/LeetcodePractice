using System;
using System.Collections.Generic;

namespace _310._Minimum_Height_Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/minimum-height-trees/
            // 給一個無向圖形，任意兩個 node 間只有一條路徑
            // 也就是說圖形內沒有循環路徑，代表此圖形是 tree

            // 給一個 tree，有 n 個 node，label 為 0 ~ (n-1)
            // 有 (n-1) 個 edges

            // 可以任意選一個 node 來當作這個 tree 的 root
            // 選擇某一個 node 做為 root，tree 的高度為 h
            // 目標，找出 選擇哪些 nodes 做為 root 時，
            // tree 的高度會最小(表示目標 nodes 不一定只有一個)

            int[][] edges = BuildTree();
            int n = 6;

            //IList<int> labels = FindMinHeightTrees(n, edges);
            IList<int> labels = FindMinHeightTrees2(n, edges);

        }
        private static IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            // n 表示 tree 的 node 數量
            // edges 的內容，例如: [[3,0],[3,1],[3,2],[3,4],[5,4]]
            // 表示 3-0 間有 edge，3-1 間有 edge，依此類推

            // 使用暴力解?
            IList<int> result = new List<int>();

            // node value 為 0 ~ (n-1)

            int minHeight = Int32.MaxValue;
            // 初始一個紀錄是否 visited 過 edge 的記錄
            bool[] visited = new bool[n];
            Array.Fill(visited, false);

            for (int i = 0; i < n; i++)
            {
                minHeight = Math.Min(minHeight, FindMinHeight(n, visited, edges, 1, i + 1));
            }

            return null;
        }

        private static int FindMinHeight(int n, bool[] visited, int[][] edges, int curHeight, int curNodeVal)
        {
            // 只靠 visited 不夠，走到某個 leaf node 時，本來就不可能再走到其他 edge，
            // 要判斷的是，是否還有下一步，
            // 沒有的話就直接 return，有的話才繼續遞迴

            for (int i = 0; i < n; i++)
            {
                bool hasNextNode = false;
                for (int k = 0; k < n; k++) 
                {
                    int[] edge = edges[k];
                    if (edge[0] == curNodeVal || edge[1] == curNodeVal)
                    {
                        int nextNode = (edge[0] == curNodeVal) ? edge[1] : edge[0];
                        visited[nextNode - 1] = true;

                        FindMinHeight(n, visited, edges, curHeight + 1, nextNode);
                    }

                }
            }




            return curHeight;
        }
        private static bool HasNextStep(int n, bool[] visited, int[][] edges, int curNodeVal)
        {



            return false;
        }



        private static IList<int> FindMinHeightTrees2(int n, int[][] edges) 
        {
            // base case
            if (n < 2)
            {
                List<int> centroids = new List<int>();
                for (int i = 0; i < n; i++)
                {
                    centroids.Add(i);
                }
                return centroids;
            }

            // Build the graph with the ajancency list
            // 先初始化
            List<HashSet<int>> neighbors = new List<HashSet<int>>();
            for (int i = 0; i < n; i++)
            {
                neighbors.Add(new HashSet<int>());
            }

            // 先把各 node 都先加入 
            // (這邊是要收集每一個 node 與他有直接連結的 nodes)
            // (leaf node 只會與他的 parent node 相連，所以 leaf node 只有一個相鄰的 node)
            foreach (int[] edge in edges)
            {
                int start = edge[0], end = edge[1];
                // 雙向都加的意思? neighbors 的 index 表示的是做為 root 的 node 的數字
                // neighbors，[key] 是 node，values 是與這個 node 相鄰的 nodes
                neighbors[start].Add(end);
                neighbors[end].Add(start);
            }

            // Initialize the first layer of leaves
            List<int> leaves = new List<int>();
            for (int i = 0; i < n; i++)
            {
                // leaf node 只會和另一個 node 相鄰，所以判斷依據是 count 為 1
                if (neighbors[i].Count == 1)
                {
                    leaves.Add(i);
                }
            }

            // Trim the leaves until reaching the centroids
            int remainingNodes = n;
            while (remainingNodes > 2)
            {
                // 一開始先減掉目前 leaf nodes 的數量
                remainingNodes -= leaves.Count;
                // 因為上一行減掉當前的 leaf nodes 數量，這邊要找出新的 leaf nodes
                List<int> newLeaves = new List<int>();

                // remove the current leaves along with the edges
                foreach (int leaf in leaves)
                {
                    // the only neighbor left for the leaf node
                    // (java 版本為 neighbors.get(leaf).iterator().next();)
                    // iterator() 回傳 Iterator 型態的物件
                    // next() 游標向後移動，取得後面的元素 (我不太清楚意思...是指目前位置的忽略，然後直接取後面的元素嗎?)
                    // Java Iterator vs C# IEnumerable
                    // https://stackoverflow.com/questions/19991193/java-iterator-vs-c-sharp-ienumerable
                    // 額外測試結果，意思就是取第一個元素，就這樣
                    var e = neighbors[leaf].GetEnumerator();
                    e.MoveNext();
                    int neighbor = e.Current;
                    // 上面就是在取出當前 leaf node 的向內連接的 node，接下來把他變成新的 leaf node

                    // remove the edge along with the leaf node
                    neighbors[neighbor].Remove(leaf);
                    if (neighbors[neighbor].Count == 1)
                    {
                        newLeaves.Add(neighbor);
                    }

                }
                // prepare for the next round
                leaves = newLeaves;
            }
            // The remaining nodes are the centroids of the graph
            return leaves;
        }




        private static int[][] BuildTree()
        {
            // 6 個 node
            // [[3,0],[3,1],[3,2],[3,4],[5,4]]

            // 前面的 index 代表 edge 的數量
            int[][] edges = new int[][]
            {
                new int[] { 3, 0 },
                new int[] { 3, 1 },
                new int[] { 3, 2 },
                new int[] { 3, 4 },
                new int[] { 5, 4 },
            };
            return edges;
        }



    }
}
