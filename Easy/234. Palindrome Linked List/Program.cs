using System;
using System.Collections.Generic;

namespace _234._Palindrome_Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/palindrome-linked-list/
            // 給一個單向鏈結串列，判斷是否為回文

            // 1 -> 2 -> 2 -> 1
            ListNode head = BuildLinkedList();

            bool result = IsPalindrome(head);
            Console.WriteLine("result: {0}", result);
        }

        private static bool IsPalindrome(ListNode head)
        {
            List<int> list = new List<int>();
            ListNode current = head;
            while(current != null)
            {
                list.Add(current.val);
                current = current.next;
            }

            int middle = list.Count / 2;
            int left, right;

            if(list.Count % 2 != 0)
            {
                // 奇數
                left = middle;
                right = middle;
            }
            else
            {
                // 偶數
                left = middle - 1;
                right = middle;
            }

            while (left >= 0 && right < list.Count)
            {
                if (list[left] != list[right])
                {
                    return false;
                }
                left--;
                right++;
            }
            return true;
        }

        private static ListNode BuildLinkedList()
        {
            ListNode node1 = new ListNode(1);
            ListNode node2 = new ListNode(2);
            ListNode node3 = new ListNode(2);
            ListNode node4 = new ListNode(1);

            node1.next = node2;
            node2.next = node3;
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
