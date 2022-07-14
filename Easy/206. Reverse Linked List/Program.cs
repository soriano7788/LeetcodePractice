using System;

namespace _206._Reverse_Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/reverse-linked-list/
            // 反轉鏈結串列

            ListNode head = BuildLinkedList();
            ShowList(head);
            ListNode result = ReverseList(head);
            ShowList(result);
        }
        private static ListNode ReverseList(ListNode head)
        {
            ListNode previous = null, 
                     current = head, 
                     next = null;

            while(current != null)
            {
                // 先把反轉前的 current 的 next 保留起來
                next = current.next;
                // 把現在的 next 指向 previous
                current.next = previous;
                // previous 往前移
                previous = current;
                // current 往前移
                current = next;
            }

            // 回傳 previous 是因為 loop 的最後， current 會變成 null
            return previous;
        }

        private static ListNode ReverseList2(ListNode head)
        {
            if(head == null || head.next == null)
            {
                return head;
            }

            ListNode p = ReverseList2(head.next);
            // 把 head 的 next 的下一個 node 指向 head
            head.next.next = head;
            // 把 head 的 next 指向 null
            head.next = null;
            return p;
        }

        private static void ShowList(ListNode node)
        {
            while(node != null)
            {
                Console.Write(node.val);
                node = node.next;
            }
            Console.WriteLine();
        }

        private static ListNode BuildLinkedList()
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
