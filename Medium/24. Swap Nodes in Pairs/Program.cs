using System;
using System.Text;

namespace SwapNodesInPairs
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/swap-nodes-in-pairs/
            // Given a linked list, swap every two adjacent nodes and return its head.
            // You may not modify the values in the list's nodes, only nodes itself may be changed.
            
            // Example:
            // Given 1->2->3->4, you should return the list as 2->1->4->3.
            
            int[] nums = new int[] {1, 2, 3, 4};
            ListNode head = GenerateListNodes(nums);
            ShowListNodes(head);
            ListNode newHead = SwapPairs(head);
            ShowListNodes(newHead);

            // todo: 自己想的另一個方法是先用 奇偶數 把 list 分成兩列，
            // 然後再合併，合併順序為 偶數list -> 奇數list -> 偶數list -> 奇數list -> .....
            // 依此類推，優點大概是比較容易理解吧??? 缺點是需要額外的 space


        }
        private static ListNode SwapPairs(ListNode head)
        {
            if(head == null)
                return head;

            ListNode dummy = new ListNode();
            dummy.next = head;
            ListNode current = dummy;
            ListNode nodeA = head,
                     nodeB = head.next;

            while(nodeA != null && nodeB != null)
            {
                nodeA.next = nodeB.next;
                nodeB.next = nodeA;
                current.next = nodeB;

                // 調整回 A 在前 B 在後的順序
                ListNode temp = nodeA;
                nodeA = nodeB;
                nodeB = temp;

                // 注意中途遇到 null 的問題
                if(nodeB.next == null)
                    break;
                nodeB = nodeB.next.next;
                if(nodeB == null)
                    break;
                nodeA = nodeA.next.next;
                current = current.next.next;

                // Console.WriteLine("current: {0}", current.val);
                // Console.WriteLine("nodeA: {0}", nodeA.val);
                // Console.WriteLine("nodeB: {0}", nodeB.val);
                // break;
            }

            // 直接傳 head 的話，結果是錯的
            return dummy.next;
        }
        private static ListNode GenerateListNodes(int[] nums)
        {
            ListNode dummy = new ListNode();
            ListNode current = dummy;
            foreach(int num in nums)
            {
                ListNode node = new ListNode(num);
                current.next= node;
                current = current.next;
            }
            return dummy.next;
        }
        private static void ShowListNodes(ListNode head)
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
        
        class ListNode 
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
