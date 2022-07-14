using System;

namespace _61._Rotate_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/rotate-list/
            // 給一個單向鏈結串列，和一個整數 k
            // 串列的每個節點往右移動 k 個位置，
            // 已經在尾端的節點則移動到開頭

            ListNode head = BuildList();
            int k = 2;

            ShowList(head);
            ListNode result = RotateRight(head, k);
            ShowList(result);
        }
        private static ListNode RotateRight(ListNode head, int k)
        {
            if(head == null)
            {
                return null;
            }

            int count = GetCount(head);
            // 真正要往右移動的次數，因為有可能會繞好幾圈
            // 例如 串列長度為 5，假如位移數為 5，那也只是繞一圈回到原點，所以才用取餘數的方式來決定 最終須位移多少位置
            int offset = k % count;

            if (offset == 0)
            {
                return head;
            }

            // 假如 offset 是 2，表示從倒數第 2 個節點到尾端，要變成起頭
            // 也可以想成 count - offset，例如 count =5, offset = 2, count - offset 為 3
            // 表示從串列開頭開始的前 3 個節點，都移動到尾端
            ListNode cur = head;
            ListNode prev = null;
            int index = 0;
            while(cur != null && index != (count - offset))
            {
                index++;
                prev = cur;
                cur = cur.next;
            }
            // 這時候 cur 已經到達新的開頭了(那我要如何保留 cur 的前一個呢? 用 prev 嗎?)

            ListNode dummy = new ListNode();
            dummy.next = cur;
            while(cur.next != null)
            {
                cur = cur.next;
            }
            cur.next = head;
            prev.next = null;

            return dummy.next;
        }
        private static int GetCount(ListNode head)
        {
            ListNode cur = head;
            int count = 0;
            while(cur != null)
            {
                count++;
                cur = cur.next;
            }
            return count;
        }

        private static ListNode BuildList()
        {
            ListNode node1 = new ListNode(1);
            ListNode node2 = new ListNode(2);
            ListNode node3 = new ListNode(3);
            ListNode node4 = new ListNode(4);
            ListNode node5 = new ListNode(5);

            node1.next = node2;
            node2.next = node3;
            node3.next = node4;
            node4.next = node5;

            return node1;
        }
        private static void ShowList(ListNode head)
        {
            ListNode cur = head;
            while(cur!= null)
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
