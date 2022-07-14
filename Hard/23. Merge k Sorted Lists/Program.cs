using System;

namespace _23._Merge_k_Sorted_Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/merge-k-sorted-lists/

            ListNode[] lists = new ListNode[] { BuildList1(), BuildList2(), BuildList3() };
            ListNode result = MergeKLists(lists);

            //foreach(ListNode node in lists)
            //{
            //    Console.WriteLine("is null: {0}", (node == null));
            //}

            ShowList(result);
        }
        private static ListNode MergeKLists(ListNode[] lists)
        {
            if(lists.Length == 0)
            {
                return null;
            }

            ListNode head = new ListNode(0);
            ListNode current = head;

            // 只要其中一個 list 非 null，就可以繼續
            while(!IsAllListsNull(lists))
            {
                int minIndex = -1;
                int min = Int32.MaxValue;
                for(int i = 0; i < lists.Length; i++)
                {
                    if(lists[i] != null)
                    {
                        // 發現更小的就取代掉
                        if(lists[i].val < min)
                        {
                            min = lists[i].val;
                            minIndex = i;
                        }
                    }
                }

                if(minIndex != -1)
                {
                    current.next = lists[minIndex];
                    current = current.next;
                    lists[minIndex] = lists[minIndex].next;
                }
            }

            return head.next;
        }
        private static bool IsAllListsNull(ListNode[] lists)
        {
            for(int i = 0; i < lists.Length; i++)
            {
                if(lists[i] != null)
                {
                    return false;
                }
            }

            return true;
        }

        private static void ShowList(ListNode head)
        {
            int i = 0;
            ListNode current = head;
            while(current != null && i < 15)
            {
                Console.Write("{0} -> ", current.val);
                current = current.next;
                i++;
            }
            Console.WriteLine();
        }

        private static ListNode BuildList1()
        {
            ListNode node1 = new ListNode(1);
            ListNode node4 = new ListNode(4);
            ListNode node5 = new ListNode(5);

            node1.next = node4;
            node4.next = node5;

            return node1;
        }
        private static ListNode BuildList2()
        {
            ListNode node1 = new ListNode(1);
            ListNode node3 = new ListNode(3);
            ListNode node4 = new ListNode(4);

            node1.next = node3;
            node3.next = node4;

            return node1;
        }
        private static ListNode BuildList3()
        {
            ListNode node2 = new ListNode(2);
            ListNode node6 = new ListNode(6);

            node2.next = node6;

            return node2;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

    }
}
