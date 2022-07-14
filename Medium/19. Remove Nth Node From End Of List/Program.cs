using System;
using System.Text;

namespace RemoveNthNodeFromEndOfList
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/remove-nth-node-from-end-of-list/solution/
            // 沒加數字的 methods，list 的 開頭 是一個 dummy node
            // 名稱 postfix 為 2 的 methods，開頭就已經是數字了

            // 網站中的解法2，有啥原理嗎? 我怎麼覺得花的時間 實際上是差不多的???
            
            // Given a linked list, remove the n-th node from the end of list and return its head.
            int[] nums = new int[] {1, 2, 3, 4, 5};
            //nums = new int[] {1};

            // ListNode linkedList = GenerateLinkedList(nums);
            // ShowLinkedList(linkedList);
            // ListNode result = RemoveNthFromEnd(linkedList, 1);
            // ShowLinkedList(result);

            ListNode linkedList = GenerateLinkedList2(nums);
            ShowLinkedList2(linkedList);
            ListNode result = RemoveNthFromEnd2(linkedList, 2);
            ShowLinkedList2(result);

        }
        private static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            int count = 0;
            ListNode current = head.next;
            while(current != null)
            {
                count++;
                current = current.next;
            }

            current = head.next;
            int index = count - n;
            count = 0;

            // if(index <= 0)
            // {
            //     return null;
            // }

            while(current != null)
            {
                count++;
                if(count == index)
                {
                    current.next = current.next.next;
                    break;
                }
                current = current.next;
            }

            return head;
        }
        private static ListNode GenerateLinkedList(int[] nums)
        {
            ListNode head = new ListNode();
            ListNode current = head;
            foreach(int num in nums)
            {
                ListNode node = new ListNode(num);
                current.next = node;
                current = current.next;
            }
            return head;
        }
        private static void ShowLinkedList(ListNode head)
        {
            ListNode current = head.next;
            StringBuilder sb = new StringBuilder();
            while(current != null)
            {
                sb.Append(current.val);
                if(current.next != null)
                {
                    sb.Append(" -> ");
                }
                current = current.next;
            }
            Console.WriteLine(sb.ToString());
        }

        private static ListNode RemoveNthFromEnd2(ListNode head, int n)
        {
            ListNode dummy = new ListNode();
            dummy.next = head;
            ListNode current = head;

            int count = 0;
            while(current != null)
            {
                count++;
                current = current.next;
            }

            current = dummy;
            int targetIndex = count - n; // target index 是要刪除 的第 n 個 node，的前一個 node
            while(current != null && targetIndex > 0)
            {
                current = current.next;
                targetIndex--;
            }
            current.next = current.next.next;

            return dummy.next;


            ///////////////////////////////
            // ListNode current = head;
            // int count = 0;
            // while(current != null)
            // {
            //     count++;
            //     current = current.next;
            // }

            // int index = count - n;
            // if(index == 0)
            // {
            //     // 通常表示要把 list 的 head 拿掉?
            //     return head.next;
            // }

            // count = 0;
            // current = head;
            // while(current != null)
            // {
            //     count++;
            //     if(count == index)
            //     {
            //         current.next = current.next.next;
            //         break;
            //     }
            //     current = current.next;
            // }
            // return head;
        }
        private static ListNode GenerateLinkedList2(int[] nums)
        {
            ListNode dummy = new ListNode();
            ListNode current = dummy;
            foreach(int num in nums)
            {
                ListNode node = new ListNode(num);
                current.next = node;
                current = current.next;
            }
            return dummy.next;
        }
        private static void ShowLinkedList2(ListNode head)
        {
            ListNode current = head;
            StringBuilder sb = new StringBuilder();
            while(current != null)
            {
                sb.Append(current.val);
                if(current.next != null)
                {
                    sb.Append(" -> ");
                }
                current = current.next;
            }
            Console.WriteLine(sb.ToString());
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
