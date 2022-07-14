using System;

namespace _876._Middle_of_the_Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/middle-of-the-linked-list/
            // 給一個單向鏈結串列，回傳串列的中間節點
            // 假如節點數量是偶數，就回傳第二個中間節點(較後面那個)
            ListNode head = BuildList();
            //ShowList(head);
            ListNode result = MiddleNode(head);
            Console.WriteLine("result: " + result.val);
        }

        private static ListNode MiddleNode(ListNode head)
        {
            if(head == null || head.next == null)
            {
                return head;
            }
            ListNode slow = head, 
                     fast = head;

            while(fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            if(fast.next != null)
            {
                return slow.next;
            }

            return slow;
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
            while(cur != null)
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
