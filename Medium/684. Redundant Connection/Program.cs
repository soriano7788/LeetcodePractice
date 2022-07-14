using System;
using System.Collections.Generic;

namespace _684._Redundant_Connection
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/redundant-connection/
            // 給予一堆 edges，可以組成一個 graph
            // 此題目標: 找出一個多餘的 edge，拿掉這個 edge，
            // graph 就可以變成 tree
            // 注意，可能有多個解，就回傳最後一個答案

            // output: [1, 4]
            int[][] edges = new int[][]
            {
                new int[] { 1, 2 },
                new int[] { 2, 3 },
                new int[] { 3, 4 },
                new int[] { 1, 4 },
                new int[] { 1, 5 }
            };

            // output: [2, 3]
            edges = new int[][]
            {
                new int[] { 1, 2 },
                new int[] { 1, 3 },
                new int[] { 2, 3 },
            };

            try
            {
                //int[] result = FindRedundantConnection(edges);
                int[] result = FindRedundantConnection2(edges);
                //int[] result = FindRedundantConnection3(edges);
                Console.WriteLine("result: {0}", string.Join(", ", result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex: " + ex);
            }


        }
        private static int[] FindRedundantConnection(int[][] edges)
        {
            // 用 dfs 走訪，順便檢查加上這個 edge 後，是否造成 cycle 這樣?

            // 一樣先建立各 node 以及他的 neighbors 的資料結構
            //Dictionary<int, List<int>> graph = BuildAdjList(edges);

            // 先準備好各個 node 的 neighbors 容器
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            foreach(int[] edge in edges)
            {
                if(!graph.ContainsKey(edge[0]))
                {
                    graph.Add(edge[0], new List<int>());
                }
                if (!graph.ContainsKey(edge[1]))
                {
                    graph.Add(edge[1], new List<int>());
                }
            }

            HashSet<int> visited = new HashSet<int>();
            
            // 逐步處理每一個 edge，想成是從一堆 edges，把 edge 一個一個抓出來 建成 graph
            // 過程中檢查，加上這個新 edge 後，會不會出現 cycled graph

            // 每加上一個新的 edge，都用 dfs 走訪一次
            for (int i = 0; i < edges.Length; i++)
            {
                visited.Clear();

                // graph 會記錄目前圖型已經有的 node
                // 1. 確定 edge 的第一個 node 已經有紀錄
                // 2. 確定 edge 的第二個 node 已經有紀錄
                // 3. 用 dfs 走訪檢查看看 現在要加入的這個新 edge，他的兩個 node 在目前的 graph 上是不是已經有連通了，
                // 是的話，再把現在這個 edge 加進去的話，就會變 cycled graph 了
                if (graph[edges[i][0]].Count != 0 && 
                    graph[edges[i][1]].Count != 0 && 
                    Dfs(graph, edges[i][0], edges[i][1], visited))
                {
                    return edges[i];
                }

                // 這邊把目前這個新 edge 的兩個 node，記錄到 node neighbors(graph) 的資料結構中
                graph[edges[i][0]].Add(edges[i][1]);
                graph[edges[i][1]].Add(edges[i][0]);
            }

            throw new Exception("zzz");
        }
        private static bool Dfs(Dictionary<int, List<int>> graph, int source, int target, HashSet<int> visited)
        {
            if(!visited.Contains(source))
            {
                visited.Add(source);

                // 已經走到終點的意思
                if(source == target)
                {
                    return true;
                }

                foreach (int neighbor in graph[source])
                {
                    if (Dfs(graph, neighbor, target, visited))
                    {
                        return true;
                    }
                }
            }

            // 到不了終點的意思
            return false;
        }



        #region 使用 Union-Find
        // 使用 Union-Find
        private static int[] FindRedundantConnection2(int[][] edges)
        {
            int MAX_EDGE_VAL = 1000;

            DSU dsu = new DSU(MAX_EDGE_VAL + 1);
            foreach(int[] edge in edges)
            {
                // dsu.Union 為 false 的話，表示 edge[0] 和 edge[1] 兩個節點早就已經連通了，沒辦法再做一次聯集
                if (!dsu.Union(edge[0], edge[1]))
                {
                    return edge;
                }
            }

            throw new Exception("AssertionError");
            //return null;
        }
        class DSU 
        {
            private int[] parent { get; set; }
            private int[] rank { get; set; }

            public DSU(int size)
            {
                parent = new int[size];
                for (int i = 0; i < size; i++)
                {
                    parent[i] = i;
                }
                rank = new int[size];
            }

            public int Find(int x) 
            {
                if(parent[x] != x)
                {
                    parent[x] = Find(parent[x]);
                }
                return parent[x];
            }

            public bool Union(int x, int y) 
            {
                int xr = Find(x), yr = Find(y);

                // 合併的規則以 高度較小的 tree 變成 高度較大的 tree 的 subtree 為原則，
                // 為了讓合併後的樹盡量保持平衡，不會太過傾斜(太過傾斜會造成後續的操作處理花費時間較多)

                // 表示 x, y node 的 root 是同一個，代表這兩個 node 早就已經連通了
                if (xr == yr)
                {
                    return false;
                }
                else if (rank[xr] < rank[yr])
                {
                    parent[xr] = yr;
                }
                else if (rank[xr] > rank[yr])
                {
                    parent[yr] = xr;
                }
                else
                {
                    // 高度一樣的話，就把 y 變成 subtree，另一顆 tree 的 node 也要加 1
                    // 這邊的 rank 是??? 分支出的 subtree 的數量嗎?
                    parent[yr] = xr;
                    rank[xr]++;
                }
                return true;
            }
        }
        #endregion

        private static int[] FindRedundantConnection3(int[][] edges)
        {
            // 用 dfs 走訪，順便檢查加上這個 edge 後，是否造成 cycle 這樣?

            // 一樣先建立各 node 以及他的 neighbors 的資料結構
            //Dictionary<int, List<int>> graph = BuildAdjList(edges);
            //Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

            List<int>[] graph = new List<int>[1001];
            for (int i = 0; i <= 1000; i++)
            {
                graph[i] = new List<int>();
            }

            HashSet<int> visited = new HashSet<int>();
            foreach (int[] edge in edges)
            {
                visited.Clear();
                if (graph[edge[0]].Count != 0 && graph[edge[1]].Count != 0 && Dfs3(graph, edge[0], edge[1], visited))
                {
                    return edge;
                }
                graph[edge[0]].Add(edge[1]);
                graph[edge[1]].Add(edge[0]);
            }

            throw new Exception("zzz");
        }
        private static bool Dfs3(List<int>[] graph, int source, int target, HashSet<int> visited)
        {
            if (!visited.Contains(source))
            {
                visited.Add(source);

                // 已經走到終點的意思
                if (source == target)
                {
                    return true;
                }

                Console.WriteLine("source: {0}", source);
                //if (graph.ContainsKey(source))
                //{
                foreach (int neighbor in graph[source])
                {
                    if (Dfs3(graph, neighbor, target, visited))
                    {
                        return true;
                    }
                }
                //}
            }

            // 到不了終點的意思
            return false;
        }



        private static Dictionary<int, List<int>> BuildAdjList(int[][] edges)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            for(int i = 0; i < edges.Length; i++)
            {
                if (!graph.ContainsKey(edges[i][0]))
                {
                    graph.Add(edges[i][0], new List<int>());
                }
                graph[edges[i][0]].Add(edges[i][1]);


                if (!graph.ContainsKey(edges[i][1]))
                {
                    graph.Add(edges[i][1], new List<int>());
                }
                graph[edges[i][1]].Add(edges[i][0]);
            }
            return graph;
        }
    }
}
