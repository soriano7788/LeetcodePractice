using System;
using System.Collections.Generic;

namespace _86._Partition_List
{
    class Program
    {
        static void Main(string[] args)
        {

            ListNode head = BuildList();
            int x = 3;
            ListNode result = Partition(head, x);
            ShowList(result);

        }
        private static ListNode Partition(ListNode head, int x)
        {
            // todo: 或是從 head 開始檢查，
            // 先找出大於等於 x 的 node
            // 然後以這個 node 為基準，找出後面第一個也是大於 x 的 node，
            // 把位置移到他的正前方，假如就這樣到尾端的話，那就移到尾端
            // 一次 loop 不足以完成，
            // 多試幾次，直到沒發生 swap 動作 就表示已完成。


            ListNode before = new ListNode();
            ListNode after = new ListNode();

            ListNode curBefore = before, 
                     curAfter = after;

            ListNode cur = head;
            while(cur != null)
            {
                if(cur.val < x)
                {
                    curBefore.next = cur;
                    curBefore = curBefore.next;
                }
                else
                {
                    curAfter.next = cur;
                    curAfter = curAfter.next;
                }
                cur = cur.next;
            }
            curBefore.next = null;
            // Last node of "after" list would also be ending node of the reformed list
            curAfter.next = null;

            curBefore.next = after.next;

            return before.next;
        }

        private static ListNode Partition2(ListNode head, int x)
        {
            // 先轉成 array，以 array 的狀態來排序。
            // 完成後再串起來
            List<ListNode> list = new List<ListNode>();
            ListNode cur = head;
            while (cur != null)
            {
                list.Add(cur);
                cur = cur.next;
            }

            for (int i = 0; i < list.Count; i++)
            { }
            return head;
        }

        private static ListNode BuildList()
        {
            ListNode node1 = new ListNode(1);
            ListNode node4 = new ListNode(4);
            ListNode node3 = new ListNode(3);
            ListNode node21 = new ListNode(2);
            ListNode node5 = new ListNode(5);
            ListNode node22 = new ListNode(2);

            node1.next = node4;
            node4.next = node3;
            node3.next = node21;
            node21.next = node5;
            node5.next = node22;

            return node1;
        }

        private static void ShowList(ListNode head)
        {
            ListNode cur = head;
            while (cur != null)
            {
                Console.Write(cur.val + " -> ");
                cur = cur.next;
            }
            Console.WriteLine();
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
