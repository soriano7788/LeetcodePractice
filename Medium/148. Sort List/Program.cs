using System;

namespace _148._Sort_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/sort-list/
            // 改成升冪排序
            // 目標: O(n logn) time、O(1) memory

            // 4-2-1-3

            // leetcoe 方法依序取得的 mid
            // 4-2: 2
            // 4-2-1: 2
            // 4-2-1-3: 1

            // 我的方法依序取得的 mid (看起來，偶數的情況下，我的會抓到右邊的尾端)(4-2 這個 case 就會出事)
            // 4-2: 4
            // 4-2-1: 2
            // 4-2-1-3: 2


            ListNode list = BuildList();

            //ListNode mid = GetMid(list);
            //Console.WriteLine(mid.val);
            //ShowList(list);

            //ShowList(list);
            //ListNode result = SortList(list);
            ListNode result = SortList2(list);
            ShowList(result);
        }

        #region bottom-up 解
        // bottom-up 解
        static ListNode tail = new ListNode();
        static ListNode nextSubList = new ListNode();
        private static ListNode SortList2(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            int n = GetCount(head);

            ListNode start = head;
            ListNode dummyHead = new ListNode();

            // 這邊單純 loop 應該是 logN
            // size 是 切割出的 subproblem size，從 1 開始，倍數成長(1 2 4 8.....)
            for (int size = 1; size < n; size *= 2)
            {
                tail = dummyHead;
                while(start != null)
                {
                    if(start.next == null)
                    {
                        tail.next = start;
                        break;
                    }
                    ListNode mid = Split(start, size);
                    Merge2(start, mid);
                    start = nextSubList;
                }
                start = dummyHead.next;
            }

            return dummyHead.next;
        }
        private static int GetCount(ListNode head)
        {
            int count = 0;
            ListNode cur = head;
            while(cur!= null)
            {
                count++;
                cur = cur.next;
            }
            return count;
        }
        private static ListNode Split(ListNode start, int size)
        {
            // midPrev 是 slow pointer
            // end 是 fast pointer
            ListNode midPrev = start;
            ListNode end = start.next;

            // start 是起始位置，size 是大小
            // 要找出從 start 開始往後，長度為 size，範圍內的串列的 中心點(mid)

            //use fast and slow approach to find middle and end of second linked list 
            // (不超過 size 大小，或 end 的下一格還沒到最尾端)
            // (也就是說當 到達 size 極限，或是 fast pointer 到達尾端，就結束)
            for(int index = 1; index < size || (end.next != null); index++)
            {
                // end 是一次跳兩格(也就是 fast pointer)
                if(end.next != null)
                {
                    // 假如後兩格非 null，就跳兩格，假如是 null，就跳一格
                    end = (end.next.next != null) ? end.next.next : end.next;
                }
                // midPrec 是一次跳一格(也就是 slow pointer)
                if (midPrev.next != null)
                {
                    midPrev = midPrev.next;
                }
            }
            // 中心點是 midPrev(slow pointer) 的下一格
            ListNode mid = midPrev.next;
            // 把第一條串列 跟 第二條串列截斷
            midPrev.next = null;

            // ???? end 是 fast pointer，這邊是????
            // 因為接下來，要 合併 start~midPrev 和 mid~end 兩個串列，nextSubList 是剩下的後半段串列
            nextSubList = end.next;
            // 把第二條串列跟 後面剩下的串列截斷
            end.next = null;

            // return the start of second linked list
            return mid;
        }
        private static void Merge2(ListNode list1, ListNode list2)
        {
            ListNode dummyHead = new ListNode();
            ListNode newTail = dummyHead;
            while (list1 != null && list2 != null)
            {
                if (list1.val < list2.val)
                {
                    newTail.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    newTail.next = list2;
                    list2 = list2.next;
                }
                newTail = newTail.next;
            }

            newTail.next = (list1 != null) ? list1 : list2;

            // traverse till the end of merged list to get the newTail
            while (newTail.next != null)
            {
                newTail = newTail.next;
            }
            // link the old tail with the head of merged list
            tail.next = dummyHead.next;
            // update the old tail to the new tail of merged list
            tail = newTail;
        }
        #endregion


        #region Top-down 遞迴解法 merge sort
        // Top-down 遞迴解法 merge sort
        private static ListNode SortList(ListNode head)
        {
            // null 和只有一個 node 的不處理，直接 return
            if(head == null ||  head.next == null)
            {
                return head;
            }

            ListNode mid = GetMid(head);
            ListNode left = SortList(head);
            ListNode right = SortList(mid);

            return Merge(left, right);
        }

        private static ListNode Merge(ListNode list1, ListNode list2)
        {
            ListNode dummyHead = new ListNode();
            ListNode tail = dummyHead;
            while(list1 != null && list2 != null)
            {
                if(list1.val < list2.val)
                {
                    tail.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    tail.next = list2;
                    list2 = list2.next;
                }
                tail = tail.next;
            }

            tail.next = (list1 != null) ? list1 : list2;
            return dummyHead.next;
        }

        private static ListNode GetMid(ListNode head)
        {
            // 這邊的 slow 起始位置是 null，第一輪時，才會移動到 head 的位置
            //ListNode midPrev = null;
            //while (head != null && head.next != null)
            //{
            //    midPrev = (midPrev == null) ? head : midPrev.next;
            //    head = head.next.next;
            //}
            //ListNode mid = midPrev.next;
            //midPrev.next = null;
            //return mid;

            // 看看1~3個 node 的情況下，兩種做法有何差異
            // 不如說這次的做法是為了配合上面 SortList 的切割處理方式。
            // 當數量為偶數時，回傳的 node 必須是右半段的 head
            // 一方面 midPrev.next = null; 是要把前後截斷

            // 下面嘗試一下截斷，結果失敗(不知道是否是因為作法有誤?)


            // 這個做法在 reorder list 那題能用，在這邊用會出問題
            // (X)當傳入的串列有一個 node 時，
            ListNode slow = head,
                     fast = head,
                     prev = slow;
            while (fast.next != null && fast.next.next != null)
            {
                prev = slow;
                slow = slow.next;
                fast = fast.next.next;
            }
            prev.next = null;


            ShowList(head);
            Console.WriteLine("mid: {0}", slow.val);

            return slow;
        }
        #endregion


        private static ListNode BuildList()
        {
            // 4-2-1-3
            ListNode node4 = new ListNode(4);
            ListNode node2 = new ListNode(2);
            ListNode node1 = new ListNode(1);
            ListNode node3 = new ListNode(3);

            node4.next = node2;
            node2.next = node1;
            node1.next = node3;

            return node4;
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
