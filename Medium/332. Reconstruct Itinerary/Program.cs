using System;
using System.Collections.Generic;
using System.Linq;

namespace _332._Reconstruct_Itinerary
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/reconstruct-itinerary/

            // 有多個 tickets
            // tickets[i] = [from, to]
            // 從 from 離開，抵達 to
            // 要安排行程
            // 有效的行程可能有多個
            // 要根據 lexical order 的最小值為目標行程
            // 起點一定要是 "JFK"

            // 假如有下面兩張票
            // ["JFK", "LGA"]
            // ["JFK", "LGB"]
            // 必須要先選 ["JFK", "LGA"]
            // 因為根據 lexical order，"LGA" 小於 "LGB"
            // 以上排序規則可能不只針對 開頭出發而已，中間遇到多選票的話，也要根據這個規則....有點麻煩?


            // 這題的限定條件有說一定能完成所有行程嗎?

            // Output: ["JFK","MUC","LHR","SFO","SJC"]
            IList<IList<string>> tickets = new List<IList<string>>
            {
                new List<string> { "MUC", "LHR" },
                new List<string> { "JFK", "MUC" },
                new List<string> { "SFO", "SJC" },
                new List<string> { "LHR", "SFO" }
            };

            // Output: ["JFK","ATL","JFK","SFO","ATL","SFO"]
            //tickets = new List<IList<string>>
            //{
            //    new List<string> { "JFK", "SFO" },
            //    new List<string> { "JFK", "ATL" },
            //    new List<string> { "SFO", "ATL" },
            //    new List<string> { "ATL", "JFK" },
            //    new List<string> { "ATL", "SFO" }
            //};

            // Output: ["JFK","ATL","PHX","LAX","JFK","ORD","PHL","ATL"]
            //tickets = new List<IList<string>>
            //{
            //    new List<string> { "JFK", "ATL" },
            //    new List<string> { "ORD", "PHL" },
            //    new List<string> { "JFK", "ORD" },
            //    new List<string> { "PHX", "LAX" },
            //    new List<string> { "LAX", "JFK" },
            //    new List<string> { "PHL", "ATL" },
            //    new List<string> { "ATL", "PHX" }
            //};

            // 這題找開頭雖然 "KUL" 比 "NRT" 小，但是選 "KUL" 的話，馬上就斷尾，所以必須要先選 "NRT"
            // 選下一個行程時，不只考慮字母小的而已，還要考慮會不會斷尾...
            // 選最後一個行程時，就不需要考慮是否會斷尾了
            // Output: ["JFK","NRT","JFK","KUL"]
            //tickets = new List<IList<string>>
            //{
            //    new List<string> { "JFK", "KUL" },
            //    new List<string> { "JFK", "NRT" },
            //    new List<string> { "NRT", "JFK" }
            //};


            //CompareTwoString("ATL", "ORD");

            //IList<string> result = FindItinerary(tickets);
            IList<string> result = FindItinerary3(tickets);
            Console.WriteLine("result: {0}", string.Join(", ", result));
        }

        private static IList<string> FindItinerary(IList<IList<string>> tickets)
        {
            List<string> result = new List<string>();

            // <ticket index, ticket-to>
            Dictionary<int, string> candidateTickets = new Dictionary<int, string>();
            // 先找出 from 是 "JFK" 的票
            for (int i = 0; i < tickets.Count; i++)
            {
                string from = tickets[i][0];
                if(from == "JFK")
                {
                    candidateTickets.Add(i, tickets[i][1]);
                }
            }

            // 根據 lexical order，找出最小的 to
            int minIndex = candidateTickets.First().Key;
            string minTo = candidateTickets.First().Value;
            foreach (var kvp in candidateTickets)
            {
                int index = kvp.Key;
                string to = kvp.Value;

                // 比對 string 大小
                for (int i = 0; i < to.Length; i++)
                {
                    if (to[i] < minTo[i])
                    {
                        minIndex = index;
                        minTo = tickets[index][1];
                        break;
                    }
                    if (to[i] > minTo[i]) 
                    {
                        break;
                    }
                }
            }

            result.Add(tickets[minIndex][0]);
            result.Add(tickets[minIndex][1]);

            bool[] usedTickets = new bool[tickets.Count];
            Array.Fill(usedTickets, false);
            usedTickets[minIndex] = true;

            // 接下來 loop 行為和上面找開頭那段 code 差不多
            // 應該可以完全整合在一起才對

            int totalTicketCount = tickets.Count;
            int count = 1;
            // 或是條件改成 掃完總票數就好?
            while(count < totalTicketCount)
            {
                // 找出候選 ticket
                candidateTickets.Clear();
                string targetFrom = tickets[minIndex][1];
                for(int i = 0; i < tickets.Count; i++)
                {
                    if(tickets[i][0] == targetFrom && usedTickets[i] == false)
                    {
                        candidateTickets.Add(i, tickets[i][1]);
                    }
                }

                // check candidateTickets
                // todo: 這邊有發生 candidateTickets 內沒東西的情形
                minIndex = candidateTickets.First().Key;
                minTo = candidateTickets.First().Value;
                foreach (var kvp in candidateTickets) 
                {
                    int index = kvp.Key;
                    string to = kvp.Value;

                    // 比對 string 大小
                    for (int i = 0; i < to.Length; i++)
                    {
                        if (to[i] < minTo[i])
                        {
                            minIndex = index;
                            minTo = to;
                            break;
                        }
                        if (to[i] > minTo[i])
                        {
                            break;
                        }
                    }
                }
                usedTickets[minIndex] = true;
                result.Add(tickets[minIndex][1]);

                count++;
            }

            return result;
        }

        private static IList<string> FindItinerary2(IList<IList<string>> tickets)
        {
            // 還是一樣，airport 當成 vertex，ticket 當成 directed edge
            // 蒐集各個 airport 自己的 neighbors 有哪些 airports

            return new List<string>();
        }
        private static IList<string> FindItinerary3(IList<IList<string>> tickets)
        {
            // 1. 必須能用完全部的 tickets
            // 2. 有多種選擇時，優先選擇 字母排序較小的 下一站
            // 注意: 每次都按照 2 的方式，有可能會發生無法繼續前進的 case
            // 所以仍然要考慮是否會遇到死路的問題

            // "backtracking" feature of DFS

            if(tickets == null || tickets.Count == 0)
            {
                return new List<string>();
            }


            // 記錄相鄰點
            Dictionary<string, List<string>> adjList = new Dictionary<string, List<string>>();
            // build graph
            for(int i = 0; i < tickets.Count; i++)
            {
                if(!adjList.ContainsKey(tickets[i][0]))
                {
                    adjList.Add(tickets[i][0], new List<string> { tickets[i][1] });
                }
                else
                {
                    adjList[tickets[i][0]].Add(tickets[i][1]);
                }
            }

            // 排序相鄰點
            foreach(var kvp in adjList)
            {
                kvp.Value.Sort();

                //Console.WriteLine("key: {0}, neighbors: {1}", kvp.Key, string.Join(", ", kvp.Value));
            }

            List<string> route = new List<string>();
            route.Add("JFK");

            int numTickets = tickets.Count;
            int numTicketsUsed = 0;
            DfsRoute("JFK", route, adjList, ref numTicketsUsed, numTickets);

            return route;
        }

        private static void DfsRoute(
            string v, 
            List<string> route, 
            Dictionary<string, List<string>> adjList, 
            ref int numTicketsUsed, 
            int numTickets)
        { 
            // 找不到可繼續前進的 ticket
            if(!adjList.ContainsKey(v))
            {
                //Console.WriteLine("numTicketsUsed: {0}", numTicketsUsed);
                return;
            }

            // 取得 v 相鄰的其他機場
            List<string> list = adjList[v];

            for (int i = 0; i < list.Count; i++)
            {
                string neighbor = list[i];
                list.RemoveAt(i);
                route.Add(neighbor);
                numTicketsUsed++;

                DfsRoute(neighbor, route, adjList, ref numTicketsUsed, numTickets);

                // 確定這個路徑下去有用完全部的 tickets，可以直接 return 了
                //Console.WriteLine("numTicketsUsed: {0}, numTickets: {1}", numTicketsUsed, numTickets);
                if(numTicketsUsed == numTickets)
                {
                    //Console.WriteLine("numTicketsUsed: {0}", numTicketsUsed);
                    // 看起來這邊的 return 不足以讓整個遞迴 stack 結束?
                    //Console.WriteLine("finish");

                    // 終於知道之前 numTicketsUsed 達標，但仍然沒有結束 stack 的原因了...
                    // numTicketsUsed 一開始單純用 int 傳值，
                    // 更內層遞迴修改 numTicketsUsed 的值，不會反應到外層遞迴的 numTicketsUsed 的值
                    // 所以要改傳 reference 才行，總之要確保 存取的 numTicketsUsed 是同一個
                    // 不然就要用 return 回傳 numTicketsUsed 來確保 變動 能被傳回去
                    return;
                }

                // 失敗的話，剛剛的動作要全部還原
                list.Insert(i, neighbor);
                route.RemoveAt(route.Count - 1);
                numTicketsUsed--;
            }
        }


        private static void CompareTwoString(string s1, string s2)
        {
            for (int i = 0; i < s1.Length; i++)
            {
                if(s1[i] > s2[i])
                {
                    Console.WriteLine("{0} > {1}", s1, s2);
                    return;
                }
                if (s1[i] < s2[i])
                {
                    Console.WriteLine("{0} < {1}", s1, s2);
                    return;
                }
            }
            Console.WriteLine("{0} = {1}", s1, s2);
        }

    }
}
