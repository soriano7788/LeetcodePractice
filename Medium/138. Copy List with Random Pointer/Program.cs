using System;
using System.Collections.Generic;

namespace _138._Copy_List_with_Random_Pointer
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/copy-list-with-random-pointer/
            // 給予一個鏈結串列，複製一份這個串列出來
            // 解鏈結串列的 node 額外包含一個 random property，
            // 指向串列中的任意 node 或是 null

            // 0 <= n <= 1000
            // - 10000 <= Node.val <= 10000
            // Node.random is null or is pointing to some node in the linked list.


            Node head = BuildLinkedList();
            ShowLinkedList(head);

            Node result = CopyRandomList(head);
            ShowLinkedList(result);

        }

        private static Node CopyRandomList(Node head)
        {
            if(head == null)
            {
                return head;
            }

            Node current = head;
            Node cloneHead = new Node(0); // dummy 用，value 隨便
            Node cloneCurrent = cloneHead;

            // 使用額外 space 記錄 original list 的狀態
            // 使用 map 記錄 origin 對應的 clone node (利用 GetHashCode)，

            // <origin node hash, origin node random hash>
            Dictionary<int, int> originalNodeRandomMap = new Dictionary<int, int>();
            // <clone node hash, origin node hash>
            Dictionary<int, int> cloneOriginNodeMap = new Dictionary<int, int>();
            // <origin node hash, clone node hash>
            Dictionary<int, int> originCloneNodeMap = new Dictionary<int, int>();
            // <clone node hash, clone node>
            Dictionary<int, Node> cloneNodeMap = new Dictionary<int, Node>();

            // 先複製單純的單向 linked list
            while (current != null)
            {
                Node cloneNode = new Node(current.val);

                cloneNodeMap.Add(cloneNode.GetHashCode(), cloneNode);

                // 正常來說不會遇到 key 重複的 case
                cloneOriginNodeMap.Add(cloneNode.GetHashCode(), current.GetHashCode());
                originCloneNodeMap.Add(current.GetHashCode(), cloneNode.GetHashCode());

                // 順便記錄 random
                if (current.random != null)
                {
                    originalNodeRandomMap.Add(current.GetHashCode(), current.random.GetHashCode());
                }

                cloneCurrent.next = cloneNode;
                cloneCurrent = cloneCurrent.next;
                current = current.next;
            }

            // 把第一個 dummy node 去掉
            cloneHead = cloneHead.next;
            cloneCurrent = cloneHead;

            // 用來檢查是否有 random pointer，因為前面 cloneNode 複製只有 next 的關聯
            current = head;
            // 設定 clone list 的 node 的 random pointer
            while (cloneCurrent != null)
            {
                if(current.random != null)
                {
                    // 利用 clone node hash 找出 origin node hash
                    int originHashCode = cloneOriginNodeMap[cloneCurrent.GetHashCode()];
                    // 利用 origin node hash 找出他的 origin random node hash
                    int originRandomHash = originalNodeRandomMap[originHashCode];
                    // random node hash (其實也是實際存在的 origin node hash) 找出他的 clone node hash
                    int cloneNodeRandomHash = originCloneNodeMap[originRandomHash];

                    Node cloneNode = cloneNodeMap[cloneNodeRandomHash];
                    cloneCurrent.random = cloneNode;
                }

                cloneCurrent = cloneCurrent.next;
                current = current.next;
            }

            return cloneHead;
        }

        private static Node CopyRandomList2(Node head)
        {
            if(head == null)
            {
                return head;
            }

            Dictionary<Node, Node> map = new Dictionary<Node, Node>();
            Node current = head;
            // 單純只複製 node，next 和 random 都還沒設定
            while(current != null)
            {
                map.Add(current, new Node(current.val));
                current = current.next;
            }

            current = head;
            while(current != null)
            {
                // 注意，next 有可能是 null
                //map[current].next = map[current.next];
                map[current].next = current.next != null ? map[current.next] : null;
                // random 也有可能是 null
                //map[current].random = map[current.random];
                map[current].random = current.random != null ? map[current.random] : null;
                current = current.next;
            }

            return map[head];
        }

        private static Node CopyRandomList3(Node head)
        {
            if(head == null)
            {
                return head;
            }

            Node current = head;

            while (current != null)
            {



                current = current.next;
            }


        }

        private static Node BuildLinkedList()
        {
            Node head = new Node(7);
            Node node13 = new Node(13);
            Node node11 = new Node(11);
            Node node10 = new Node(10);
            Node node1 = new Node(1);

            head.next = node13;
            node13.next = node11;
            node11.next = node10;
            node10.next = node1;

            head.random = null;
            node13.random = head;
            node11.random = node1;
            node10.random = node11;
            node1.random = head;

            return head;
        }

        private static void ShowLinkedList(Node head)
        {
            Node current = head;
            while(current != null)
            {
                Console.Write("{0}({1}) ", current.val, (current.random == null ? "null" : current.random.val.ToString()));
                // todo: 補上 random 的 value 再括號內 (random 為 null 的話就印 null)
                current = current.next;
            }
            Console.WriteLine();
        }

        public class Node
        {
            public int val;
            public Node next;
            public Node random;

            public Node(int _val)
            {
                val = _val;
                next = null;
                random = null;
            }
        }
    }
}
