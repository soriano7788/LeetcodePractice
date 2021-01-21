using System;
using System.Collections.Generic;

namespace MergeIntervals
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/merge-intervals/

            int[][] intervals = new int[][]
            {
                new int[] {1, 3},
                new int[] {2, 6},
                new int[] {8, 10},
                new int[] {15, 18}
            };
            // output: [1,6] [8,10] [15,18]

            // intervals = new int[][]
            // {
            //     new int[] {1, 4},
            //     new int[] {4, 5}
            // };
            // output: [1,5]


            // intervals = new int[][]
            // {
            //     new int[] {1, 4},
            //     new int[] {0, 4}
            // };
            // output: [0,4]  每組 pair 的第一個 element 不一定是升序....要再看看

            // intervals = new int[][]
            // {
            //     new int[] {1, 4},
            //     new int[] {0, 1}
            // };
            // output: [0,4]

            intervals = new int[][]
            {
                new int[] {1, 4},
                new int[] {0, 0}
            };
            // output: [0,0] [1,4]

            intervals = new int[][]
            {
                new int[] {2, 3},
                new int[] {4, 5},
                new int[] {6, 7},
                new int[] {8, 9},
                new int[] {1, 10},
            };
            // output: [1,10]

            // intervals = new int[][]
            // {
            //     new int[] {1, 3},
            //     new int[] {2, 6},
            //     new int[] {8, 10},
            //     new int[] {15, 18}
            // };
            // output: [1,6] [8,10] [15,18]

            // intervals = new int[][]
            // {
            //     new int[] {1, 4},
            //     new int[] {4, 5}
            // };
            // output: [1,5]


            // Console.WriteLine("[{0},{1}] [{2},{3}] overlap: {4}", 2, 6, 1, 3, Overlap(new int[] { 2, 6 }, new int[] { 1, 3 }));
            // Console.WriteLine("[{0},{1}] [{2},{3}] overlap: {4}", 8, 10, 1, 3, Overlap(new int[] { 8, 10 }, new int[] { 1, 3 }));

            // int[][] results = Merge(intervals);



            // int[][] results = LeetCodeApproach1(intervals);
            int[][] results = LeetCodeApproach2(intervals);
            foreach (int[] result in results)
            {
                Console.WriteLine(string.Join(", ", result));
            }

            #region 測試 BuildGraph method 的結果
            // BuildGraph(intervals);
            // foreach (var kvp in graph)
            // {
            //     int[] node = kvp.Key;
            //     List<int[]> children = kvp.Value;

            //     Console.WriteLine("key: {0}", string.Join(",", node));
            //     Console.Write("value: ");
            //     foreach (int[] child in children)
            //     {
            //         Console.Write(string.Join(",", child) + "  ");
            //     }
            //     Console.WriteLine();
            //     Console.WriteLine();
            // }
            #endregion

        }
        private static int[][] LeetCodeApproach1(int[][] intervals)
        {
            BuildGraph(intervals);
            BuildComponents(intervals);

            #region 印出 nodeInComp
            // for (int comp = 0; comp < nodeInComp.Count; comp++)
            // {
            //     Console.WriteLine("comp: {0}, ", comp);
            //     for (int i = 0; i < nodeInComp[comp].Count; i++)
            //     {
            //         Console.Write(string.Join(",", nodeInComp[comp][i]) + " ");
            //     }
            //     Console.WriteLine();
            //     Console.WriteLine();
            // }
            #endregion



            List<int[]> merged = new List<int[]>();
            for (int comp = 0; comp < nodeInComp.Count; comp++)
            {
                merged.Add(MergeNodes(nodeInComp[comp]));
            }

            return merged.ToArray();
        }

        private static Dictionary<int[], List<int[]>> graph;
        private static Dictionary<int, List<int[]>> nodeInComp;
        private static HashSet<int[]> visited;

        private static Dictionary<int[], List<int[]>> BuildGraph(int[][] intervals)
        {
            graph = new Dictionary<int[], List<int[]>>();
            // init
            foreach (int[] interval in intervals)
            {
                graph.Add(interval, new List<int[]>());
            }
            foreach (int[] intervalA in intervals)
            {
                foreach (int[] intervalB in intervals)
                {
                    if (Overlap(intervalA, intervalB))
                    {
                        graph[intervalA].Add(intervalB);
                        graph[intervalB].Add(intervalA);
                    }
                }
            }
            return graph;
        }
        private static bool Overlap(int[] intervalA, int[] intervalB)
        {
            return intervalA[0] <= intervalB[1] && intervalB[0] <= intervalA[0];
        }
        private static void BuildComponents(int[][] intervals)
        {
            nodeInComp = new Dictionary<int, List<int[]>>();
            visited = new HashSet<int[]>();
            int compNumber = 0;

            foreach (int[] interval in intervals)
            {
                // 走訪過的 node 就不會再 visit
                if (!visited.Contains(interval))
                {
                    MarkComponentDFS(interval, compNumber);
                    compNumber++;
                }
            }
        }
        // 用 DFS 還是 BFS 應該都沒差???
        private static void MarkComponentDFS(int[] interval, int compNumber)
        {
            Stack<int[]> stack = new Stack<int[]>();
            stack.Push(interval);

            while (stack.Count != 0)
            {
                int[] node = stack.Pop();
                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    if (!nodeInComp.ContainsKey(compNumber))
                    {
                        nodeInComp.Add(compNumber, new List<int[]>());
                    }
                    nodeInComp[compNumber].Add(node);

                    // 從自己(node)(interval)(為 graph dictionary 的 key)的 graph，找出與自己有連通的 interval(node)，丟進 stack
                    foreach (int[] child in graph[node])
                    {
                        // 等一下還沒拜訪過的 node 就會被加進 visited 記錄了
                        stack.Push(child);
                    }
                }
            }
        }
        private static int[] MergeNodes(List<int[]> nodes)
        {
            // nodes 來自 nodeInComp 的 loop
            // 就是把這個 list 裡的多個 intervals 合併成一個 interval
            int minStart = nodes[0][0];
            int maxEnd = nodes[0][1];
            foreach (int[] node in nodes)
            {
                minStart = Math.Min(minStart, node[0]);
                maxEnd = Math.Max(maxEnd, node[1]);
            }
            return new int[] { minStart, maxEnd };
        }

        class IntervalComparator : IComparer<int[]>
        {
            public int Compare(int[] a, int[] b)
            {
                return (a[0] < b[0] ? -1 : (a[0] == b[0] ? 0 : 1));
            }
        }
        private static int[][] LeetCodeApproach2(int[][] intervals)
        {
            // 先根據各 interval 的 start value 升冪排序
            Array.Sort(intervals, new IntervalComparator());
            List<int[]> merged = new List<int[]>();

            foreach (int[] interval in intervals)
            {
                // 假如 merged 目前為空，或是 目前 merged 內最後一個 interval 和目前 loop 到的 interval 沒有重疊
                if (merged.Count == 0 || (merged[merged.Count - 1][1] < interval[0]))
                {
                    merged.Add(interval);
                }
                else
                {
                    // 因為已經以各 interval 的 start 數字升冪排序了，
                    // 所以前面的 interval start 小於等於後面的 interval start，
                    // 不需再比較來決定新的 start 
                    merged[merged.Count - 1][1] = Math.Max(merged[merged.Count - 1][1], interval[1]);
                }
            }
            return merged.ToArray();
        }



        // 失敗的解法
        private static int[][] Merge(int[][] intervals)
        {
            List<int[]> results = new List<int[]>();

            HashSet<int> records = new HashSet<int>();
            int j = 0;
            while (j < intervals.Length)
            {
                if (records.Contains(j))
                {
                    j++;
                    continue;
                }

                int[] intervalA = intervals[j];
                int headA = intervalA[0],
                    tailA = intervalA[1];

                int curHead = headA,
                    curTail = tailA;

                int k = j;
                while (k < intervals.Length)
                {
                    if (records.Contains(k))
                    {
                        k++;
                        continue;
                    }

                    int[] intervalB = intervals[k];
                    int headB = intervalB[0],
                        tailB = intervalB[1];

                    bool overlap = false;
                    // overlap 有四種case
                    // [0,0]
                    // [1,4]

                    // A 在前 B 在後， A後段 與 B前段 重疊
                    if (curHead < tailB &&
                        curTail >= headB &&
                        curTail <= tailB)
                    {
                        curTail = tailB;
                        overlap = true;
                    }
                    // B 在前 A 在後， B後段 與 A前段 重疊
                    if (headB < curHead &&
                        curTail >= headB &&
                        curTail >= tailB &&
                        tailB >= curHead)
                    {
                        curHead = headB;
                        overlap = true;
                    }
                    // A 完全包住 B
                    if (curHead <= headB && curTail >= tailB)
                    {
                        overlap = true;
                    }
                    // B 完全包住 A
                    if (headB <= curHead && tailB >= curTail)
                    {
                        curHead = headB;
                        curTail = tailB;
                        overlap = true;
                    }

                    if (overlap)
                    {
                        records.Add(k);
                    }
                    k++;
                }
                results.Add(new int[] { curHead, curTail });
                j++;
            }
            return results.ToArray();
        }
    }
}
