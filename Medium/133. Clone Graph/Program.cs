using System;
using System.Collections.Generic;

namespace _133._Clone_Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/clone-graph/
            // 給一個無向連通圖形的 node
            // 回傳一個深度複製的圖形
            // 單純用 bfs dfs 就能解嗎?
            // 感覺拜訪過的 node 不做記錄的話，就會變成無窮遞迴迴圈之類的
            // node 丟進 stack 或 queue，取出的當下才算 visited
            // 取出的同時，把他的 neighbors 再塞進 stack 或 queue
            // 推測比較麻煩的點會是 neighbors 的複製吧?

            // 需處理 node 為空的 case
            // 條件有限制每個 node 的 value 是唯一值，所以下面 key 的部分其實可以用 int


            Node node = BuildGraph();
            //Node cloneNode = CloneGraph(node);
            Node cloneNode = CloneGraph2(node);
        }
        private static Node CloneGraph(Node node)
        {
            if(node == null)
            {
                return node;
            }
            // 先走訪一次 origin graph 產生全部的 clone nodes
            // 再走訪一次 origin graph，根據 mapper 設定 clone nodes 的 neighbors
            // 先暫定這樣解
            // <origin, clone>
            Dictionary<Node, Node> mapper = new Dictionary<Node, Node>();
            Stack<Node> stack = new Stack<Node>();

            stack.Push(node);
            while (stack.Count > 0)
            {
                var popNode = stack.Pop();
                if (!mapper.ContainsKey(popNode))
                {
                    mapper.Add(popNode, CloneNode(popNode));

                    // 還沒複製過的 neighbor 才丟進 stack
                    foreach (Node neighbor in popNode.neighbors)
                    {
                        if (!mapper.ContainsKey(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }

            // 替換 clone node 的 neighbors 成 clone nodes
            foreach (var kvp in mapper)
            {
                Node cloneNode = kvp.Value;
                List<Node> cloneNeighbors = new List<Node>();
                foreach (var neighbor in cloneNode.neighbors)
                {
                    cloneNeighbors.Add(mapper[neighbor]);
                }
                cloneNode.neighbors = cloneNeighbors;
            }

            // 或是利用遞迴，在每一階呼叫時，把目前的 node 傳入，並在下一階同時 取出 neighbors?

            return mapper[node];
        }

        private static Node CloneGraph2(Node node)
        {
            Dictionary<int, Node> mapper = new Dictionary<int, Node>();
            return DFS(node, mapper);
        }
        private static Node DFS(Node node, Dictionary<int, Node> mapper)
        {
            if (node == null)
            {
                return node;
            }

            // 已走訪過此 node
            if (mapper.ContainsKey(node.val))
            {
                return mapper[node.val];
            }

            // 複製 node 並加進 mapper (也代表已走訪過)
            Node cloneNode = new Node(node.val);
            mapper.Add(node.val, cloneNode);

            foreach (Node neighbor in node.neighbors)
            {
                cloneNode.neighbors.Add(DFS(neighbor, mapper));
            }

            return cloneNode;
        }


        private static Node CloneNode(Node node)
        {
            Node cloneNode = new Node(node.val);
            cloneNode.neighbors = node.neighbors;
            return cloneNode;
        }
        private static Node BuildGraph()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);

            node1.neighbors = new List<Node> { node2, node4 };
            node2.neighbors = new List<Node> { node1, node3 };
            node3.neighbors = new List<Node> { node2, node4 };
            node4.neighbors = new List<Node> { node1, node3 };

            return node1;
        }

        public class Node
        {
            public int val;
            public IList<Node> neighbors;

            public Node()
            {
                val = 0;
                neighbors = new List<Node>();
            }

            public Node(int _val)
            {
                val = _val;
                neighbors = new List<Node>();
            }

            public Node(int _val, List<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }
    }
}
