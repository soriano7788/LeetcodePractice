using System;
using System.Collections.Generic;

namespace _399._Evaluate_Division
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/evaluate-division/

            // equations: equations[i] 為 [ A, B ] 這樣兩個變數一組的 array
            // values: values[i] 為 equations[i] 的兩個變數相除後的結果
            // 以上面範例就是  equations[i][0] / equations[i][1] = values[i]
            // A / B = values[i]

            // queries: 就是各種變數除法的解，假如用到 equations 裡面不存在的變數，解直接設為 -1
            // 這題要解出 queries 裡面各題的答案(變數相除的答案)


            // 這題....紙上寫一下算式會比較清楚
            // ( a / c ) 等於 ( a / b) 除以 ( b / c )
            // 就跟求 a 到 c 的路徑很像 (這邊是  a -> b -> c)

            // 注意，起點和終點是固定的(中間路徑多長不重要)


            // [6.00000,0.50000,-1.00000,1.00000,-1.00000]
            IList<IList<string>> equations = new List<IList<string>> 
            {
                new List<string> { "a", "b" },
                new List<string> { "b", "c" }
            };
            double[] values = new double[] { 2.0, 3.0 };
            IList<IList<string>> queries = new List<IList<string>> 
            {
                new List<string> { "a", "c" },
                new List<string> { "b", "a" },
                new List<string> { "a", "e" },
                new List<string> { "a", "a" },
                new List<string> { "x", "x" },
            };
            // 上面除了知道 a/b 等於 2，也要能夠知道 b/a 等 1/2
            // 這題用圖形來思考的話，其實是雙向圖， a -> b 權重為 2， b -> a 權重為 1/2


            // Output: [3.75000, 0.40000, 5.00000, 0.20000]
            //equations = new List<IList<string>>
            //{
            //    new List<string> { "a", "b" },
            //    new List<string> { "b", "c" },
            //    new List<string> { "bc", "cd" }
            //};
            //values = new double[] { 1.5, 2.5, 5.0 };
            //queries = new List<IList<string>>
            //{
            //    new List<string> { "a", "c" },
            //    new List<string> { "c", "b" },
            //    new List<string> { "bc", "cd" },
            //    new List<string> { "cd", "bc" }
            //};

            // Output: [0.50000, 2.00000, -1.00000, -1.00000]
            //equations = new List<IList<string>>
            //{
            //    new List<string> { "a", "b" },
            //};
            //values = new double[] { 0.5 };
            //queries = new List<IList<string>>
            //{
            //    new List<string> { "a", "b" },
            //    new List<string> { "b", "a" },
            //    new List<string> { "a", "c" },
            //    new List<string> { "x", "y" }
            //};

            // Output: [360.00000,0.00833,20.00000,1.00000,-1.00000,-1.00000]
            //equations = new List<IList<string>>
            //{
            //    new List<string> { "x1", "x2" },
            //    new List<string> { "x2", "x3" },
            //    new List<string> { "x3", "x4" },
            //    new List<string> { "x4", "x5" }
            //};
            //values = new double[] { 3.0, 4.0, 5.0, 6.0 };
            //queries = new List<IList<string>>
            //{
            //    new List<string> { "x1", "x5" },
            //    new List<string> { "x5", "x2" },
            //    new List<string> { "x2", "x4" },
            //    new List<string> { "x2", "x2" },
            //    new List<string> { "x2", "x9" },
            //    new List<string> { "x9", "x9" }
            //};

            // Output: [-1.00000,-1.00000,1.00000,1.00000]
            equations = new List<IList<string>>
            {
                new List<string> { "a", "b" },
                new List<string> { "c", "d" }
            };
            values = new double[] { 1.0, 1.0 };
            queries = new List<IList<string>>
            {
                new List<string> { "a", "c" },
                new List<string> { "b", "d" },
                new List<string> { "b", "a" },
                new List<string> { "d", "c" }
            };


            //double[] result = CalcEquation(equations, values, queries);
            double[] result = CalcEquation2(equations, values, queries);
            Console.WriteLine("result: {0}", string.Join(", ", result));
        }

        private static double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            // 先調整 equation 和 values，把反向的解也加入
            List<double> newValues = new List<double>(values);
            int equationsCount = equations.Count;
            for (int i = 0; i < equationsCount; i++) 
            {
                string start = equations[i][0];
                string end = equations[i][1];

                equations.Add(new List<string> { end, start });
                newValues.Add(1 / values[i]);
            }
            values = newValues.ToArray();


            // 記錄所有存在的變數
            List<string> variables = new List<string>();
            for(int i = 0 ; i < equations.Count; i++)
            {
                string start = equations[i][0];
                string end = equations[i][1];

                if(!variables.Contains(start))
                {
                    variables.Add(start);
                }
                if (!variables.Contains(end))
                {
                    variables.Add(end);
                }
            }


            // 用來記錄每個問題的解
            double[] answers = new double[queries.Count];
            for(int i = 0; i < queries.Count; i++)
            {
                // 題目使用不存在的變數，無解
                if(!variables.Contains(queries[i][0]) || !variables.Contains(queries[i][1]))
                {
                    answers[i] = -1;
                }
                else if (queries[i][0] == queries[i][1])  // 除數和被除數一樣，答案為 1
                {
                    answers[i] = 1;
                }
                else
                {
                    List<string> route = new List<string>();
                    route.Add(queries[i][0]);

                    // 相鄰矩陣每解一個新的題目時，都要重建一次
                    Dictionary<string, List<string>> adjList = BuildAdjList(equations);
                    Dfs(queries[i][0], queries[i][1], adjList, route);

                    // route 的尾端不是 end，表示無解
                    if (route[route.Count - 1] != queries[i][1])
                    {
                        answers[i] = -1;
                    }
                    else
                    {
                        // 假如前面遞迴 route 的過程中，順便求值，是否會比較方便?
                        double ans = 1;
                        for (int k = 0; k < route.Count - 1; k++)
                        {
                            string start = route[k];
                            string end = route[k + 1];

                            for (int x = 0; x < equations.Count; x++)
                            {
                                if (equations[x][0] == start && equations[x][1] == end)
                                {
                                    ans *= values[x];
                                    break;
                                }
                            }
                        }
                        answers[i] = ans;
                    }
                }
            }

            return answers;
        }

        private static void Dfs(string current, string end, Dictionary<string, List<string>> adjList, List<string> route)
        {
            // 這塊怪怪的..
            if (!adjList.ContainsKey(current))
            {
                return;
            }

            List<string> lists = adjList[current];

            for (int i = 0; i < lists.Count; i++)
            {
                string next = lists[i];
                route.Add(next);
                // todo: 這邊修改相鄰矩陣的內容，可能會影響到解後續題目，因為有好幾題要解
                // 修正後的結果，答案是 yes!!!
                lists.RemoveAt(i);

                if (next == end)
                {
                    return;
                }

                Dfs(next, end, adjList, route);

                if(route[route.Count - 1] == end)
                {
                    return;
                }

                route.RemoveAt(route.Count - 1);
                lists.Insert(i, next);

            }
        }

        private static Dictionary<string, List<string>> BuildAdjList(IList<IList<string>> equations)
        {
            Dictionary<string, List<string>> adjList = new Dictionary<string, List<string>>();
            for (int i = 0; i < equations.Count; i++)
            {
                string start = equations[i][0];
                string end = equations[i][1];

                // 紀錄相鄰
                if (!adjList.ContainsKey(start))
                {
                    adjList.Add(start, new List<string> { end });
                }
                else
                {
                    adjList[start].Add(end);
                }
            }
            return adjList;
        }




        private static double[] CalcEquation2(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            // Build graph.
            Dictionary<string, Dictionary<string, double>> graph = BuildGraph(equations, values);
            double[] result = new double[queries.Count];

            for(int i = 0; i < queries.Count; i++)
            {
                result[i] = GetPathWeight(queries[i][0], queries[i][1], new HashSet<string>(), graph);
            }
            return result;
        }

        private static Dictionary<string, Dictionary<string, double>> BuildGraph(IList<IList<string>> equations, double[] values)
        {
            // 把 除數 和 被除數 和 數字包成一組資料的結構
            // a / b = 答
            // Dictionary<a, Dictionary<b, 答>>
            Dictionary<string, Dictionary<string, double>> graph = new Dictionary<string, Dictionary<string, double>>();
            string u, v;

            for (int i = 0; i < equations.Count; i++)
            {
                u = equations[i][0];
                v = equations[i][1];

                if(!graph.ContainsKey(u))
                {
                    graph.Add(u, new Dictionary<string, double>());
                }
                graph[u].Add(v, values[i]);

                // 同時要加上反向的資料，例如 原本的 equations 只有 a/b，這邊要轉換出 b/a 
                if(!graph.ContainsKey(v))
                {
                    graph.Add(v, new Dictionary<string, double>());
                }
                graph[v].Add(u, 1 / values[i]);
            }
            return graph;
        }

        // start 會循著路徑往下變化
        private static double GetPathWeight(string start,string end, HashSet<string> visited, Dictionary<string, Dictionary<string, double>> graph)
        {
            // rejection case
            if(!graph.ContainsKey(start))
                {
                return -1;
            }

            //accepting case (就是找到 end 了，可以結束啦)
            if(graph[start].ContainsKey(end))
            {
                return graph[start][end];
            }

            visited.Add(start);

            foreach(var neighbor in graph[start])
            {
                if(!visited.Contains(neighbor.Key))
                {
                    double productWeight = GetPathWeight(neighbor.Key, end, visited, graph);
                    if(productWeight != -1)
                    {
                        return neighbor.Value * productWeight;
                    }
                }
            }

            return -1;
        }
    }
}
