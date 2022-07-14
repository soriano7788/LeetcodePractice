using System;

namespace _21._Merge_Two_Sorted_Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/merge-two-sorted-lists/
            ListNode result = MergeTwoLists(BuilListNode1(), BuilListNode2());

        }
        private static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode result = new ListNode();
            ListNode current = result;

            while(l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    current.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    current.next = l2;
                    l2 = l2.next;
                }
                current = current.next;
            }

            while(l1 != null)
            {
                current.next = l1;
                current = current.next;
                l1 = l1.next;
            }
            while (l2 != null)
            {
                current.next = l2;
                current = current.next;
                l2 = l2.next;
            }

            return result.next;
        }

        private static ListNode MergeTwoLists2(ListNode l1, ListNode l2)
        {
            if (l1 == null)
            {
                return l2;
            }
            if (l2 == null)
            {
                return l1;
            }

            if(l1.val < l2.val)
            {
                l1.next = MergeTwoLists2(l1.next, l2);
                return l1;
            }
            else
            {
                l2.next = MergeTwoLists2(l2.next, l1);
                return l2;
            }
        }

        private static ListNode BuilListNode1()
        {
            ListNode node1 = new ListNode(1);
            ListNode node2 = new ListNode(2);
            ListNode node4 = new ListNode(4);

            node1.next = node2;
            node2.next = node4;

            return node1;
        }
        private static ListNode BuilListNode2()
        {
            ListNode node1 = new ListNode(1);
            ListNode node3 = new ListNode(3);
            ListNode node4 = new ListNode(4);

            node1.next = node3;
            node3.next = node4;

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
