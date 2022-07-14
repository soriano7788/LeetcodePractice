using System;

namespace _143._Reorder_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/reorder-list/
            // 給一個單向鏈結串列，把順序重新排序，範例如下
            // input: 1 -> 2 -> 3 -> 4
            // output: 1 -> 4 -> 2 -> 3
            // 規律: 左一 -> 右一 -> 左二 -> 右二 -> 左三 -> 右三 -> .....

            // 備註：不可修改原串列中，各節點的 value

            ListNode head = BuildList();
            head = BuildList2();

            //ShowList(head);
            //ListNode reversedHead = ReverseList(head);
            //ShowList(reversedHead);

            //ReorderList(head);
            ReorderList2(head);

            ShowList(head);
        }


        private static void ReorderList(ListNode head)
        {
            // list node 數量小於等於 1
            if(head == null || head.next == null)
            {
                return;
            }

            ListNode fast = head, 
                     slow = head;

            // 先取得中心點，fast有沒有到剛好到達最後端不重要，目標是中心點
            while(fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }


            // 把後半段的順序對調
            //Reverse the half after middle  1->2->3->4->5->6  to  1->2->3 -> 6->5->4
            ListNode preMiddle = slow;
            ListNode preCurrent = slow.next;
            while(preCurrent.next != null)
            {
                ListNode current = preCurrent.next;
                preCurrent.next = current.next;
                current.next = preMiddle.next;
                preMiddle.next = current;
            }
            //ShowList(head);
            //ShowList(slow);
            //ShowList(preMiddle);

            //Start reorder one by one  1->2->3->6->5->4 to 1->6->2->5->3->4
            slow = head;
            fast = preMiddle.next;
            while(slow != preMiddle)
            {
                preMiddle.next = fast.next;
                fast.next = slow.next;
                slow.next = fast;
                slow = fast.next;
                fast = preMiddle.next;

            }
        }

        private static void ReorderList2(ListNode head)
        {
            // list node 數量小於等於 1
            if (head == null || head.next == null)
            {
                return;
            }

            ListNode fast = head,
                     slow = head;

            // 先取得中心點，fast有沒有到剛好到達最後端不重要，目標是中心點
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            // 切成兩條
            ListNode pre = null, 
                     cur = slow.next;
            while(cur != null)
            {
                ListNode next = cur.next;
                cur.next = pre;
                pre = cur;
                cur = next;
            }
            slow.next = null;


            // 開始重排序
            ListNode dummy = new ListNode(0);
            cur = dummy;
            ListNode a = head;
            ListNode b = pre;

            while(a != null && b !=null)
            {
                cur.next = a;
                a = a.next;
                cur = cur.next;

                cur.next = b;
                cur = cur.next;
                b = b.next;
            }
            while (a != null)
            {
                cur.next = a;
                cur = cur.next;
                a = a.next;
            }
            while (b != null)
            {
                cur.next = b;
                cur = cur.next;
                b = b.next;
            }

            head = dummy.next;
        }



        private static ListNode ReverseList(ListNode head)
        {
            if(head == null)
            {
                return head;
            }

            ListNode current = head;
            ListNode prev = null;

            ListNode next = null;

            while (current != null)
            {
                next = current.next;

                current.next = prev;
                prev = current;
                
                current = next;
            }
            return prev;
        }

        private static void ShowList(ListNode head)
        {
            ListNode current = head;
            while(current != null)
            {
                Console.Write(current.val + " -> ");
                current = current.next;
            }
            Console.WriteLine();
        }

        private static ListNode BuildList()
        {
            ListNode node1 = new ListNode(1);
            ListNode node2 = new ListNode(2);
            ListNode node3 = new ListNode(3);
            ListNode node4 = new ListNode(4);

            node1.next = node2;
            node2.next = node3;
            node3.next = node4;

            return node1;
        }
        private static ListNode BuildList2()
        {
            ListNode node1 = new ListNode(1);
            ListNode node2 = new ListNode(2);
            ListNode node3 = new ListNode(3);
            ListNode node4 = new ListNode(4);
            ListNode node5 = new ListNode(5);
            ListNode node6 = new ListNode(6);

            node1.next = node2;
            node2.next = node3;
            node3.next = node4;
            node4.next = node5;
            node5.next = node6;

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
