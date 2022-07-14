using System;

namespace _147._Insertion_Sort_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/insertion-sort-list/
            // 給一個單向鏈結串列，使用 insertion sort 排序
            // insertion sort
            // http://notepad.yehyeh.net/Content/Algorithm/Sort/Insertion/1.php

            try
            {
                ListNode head = BuildList();
                //ShowList(head);
                //ListNode result = InsertionSortList(head);
                //ListNode result = InsertionSortList2(head);
                //ListNode result = InsertionSortList3(head);
                //ListNode result = InsertionSortList4(head);
                ListNode result = InsertionSortListFinal(head);
                ShowList(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex: {0}", ex);
            }


        }
        private static ListNode InsertionSortList(ListNode head)
        {
            if(head == null || head.next == null)
            {
                return head;
            }

            ListNode dummy = new ListNode();
            dummy.next = head;

            ListNode cur = head;
            while(cur.next != null)
            {
                ListNode curNext = cur.next;

                // 這次要移動位置的 target node
                ListNode target = cur.next;
                ListNode preTarget = cur;

                // 重置已排序的 list
                ListNode subCur = head;
                ListNode preSubCur = dummy;
                // 從已排序的 list， head 掃到 target 為止
                while(subCur != null && subCur != target)
                {
                    if(target.val > subCur.val)
                    {
                        preSubCur = subCur;
                        subCur = subCur.next;
                    }
                    else
                    {
                        // 這邊表示 target 比 subCur 小，所以要到 subCur 的前面
                        preTarget.next = target.next;
                        preSubCur.next = target;
                        target.next = subCur;

                        break;
                    }

                    //if (target.val < subCur.val)
                    //{
                    //    // 把 target 接到 subCur 前面，然後 break;
                    //    // 要注意 subCur 可能是串列開頭的 case，
                    //    // 也可以一開始就把 input 的串列接在一個 dummy node 後面
                    //    preTarget.next = target.next;
                    //    preSubCur.next = target;
                    //    target.next = subCur;
                    //    break;
                    //}
                    //else
                    //{
                    //}
                    //preSubCur = subCur;
                    //subCur = subCur.next;
                }
                //cur = cur.next;
                cur = curNext;
                // 發生跑完上面那行後， cur 變 null，可能是內層迴圈的關係??
                //Console.WriteLine("cur == null: {0}", (cur == null));
            }

            return dummy.next;
        }

        private static ListNode InsertionSortList2(ListNode head)
        {
            if(head == null)
            {
                return head;
            }

            ListNode dummy = new ListNode();
            ListNode cur = head;
            ListNode pre = dummy;
            ListNode next = null;

            while(cur != null)
            {
                // 先把 cur.next 保存起來
                next = cur.next;
                // find the right place to insert (看起來是一直往右找 小於 cur.val 的 node)
                while (pre.next != null && pre.next.val < cur.val)
                {
                    pre = pre.next;
                }
                // insert between pre and pre.next
                cur.next = pre.next;
                pre.next = cur;
                pre = dummy;

                // cur 在上面都沒被改動，這邊再把上面的 next 指定給 cur (next 也沒有變動，不過有直接修改 cur.next)
                // 看起來和 cur = cur.next 一樣
                cur = next;
            }
            return dummy.next;
        }

        private static ListNode InsertionSortList3(ListNode head)
        {
            ListNode dummy = new ListNode();
            ListNode cur = dummy;
            while(head != null)
            {
                ListNode t = head.next;
                // 每一 loop 都先用 dummy 覆蓋 cur
                cur = dummy;
                while(cur.next != null && cur.next.val <= head.val)
                {
                    cur = cur.next;
                }
                head.next = cur.next;
                cur.next = head;
                head = t;
            }
            return dummy.next;
        }

        private static ListNode InsertionSortList4(ListNode head)
        {
            // https://leetcode.com/problems/insertion-sort-list/discuss/46459/Accepted-Solution-using-JAVA

            ListNode dummy = new ListNode(0);
            ListNode pre = dummy;
            ListNode current = head;

            // current 會一步一步移動
            while (current != null)
            {
                // pre 每輪會被重置成 dummy (重點是 dummy 是不是會隨著每一輪，逐步變長?)
                // (嘗試每輪印出結果，看起來是，所以 dummy 是目前已排序的 list 的 頭)
                // pre 是用來走訪已排序的 list，
                // 一開始，已排序 list 是空的，
                pre = dummy;

                // current 是現在要加入已排序 list 的 node，此迴圈是要找出該插入哪一個位置
                while (pre.next != null && pre.next.val < current.val)
                {
                    pre = pre.next;
                }

                ListNode next = current.next;

                current.next = pre.next;
                // 把 current 加入已排序 list(被加到 dummy list 上面)
                pre.next = current;

                // 靠 next，讓 current 可以正常往前移動
                current = next;
            }

            // 整個過程，從傳入的 list，從頭 依序拿出一個 node，
            // 移動到已排序 list 中(dummy 那條，當然也要先找一下要插入 dummy 的哪一個位置)


            return dummy.next;
        }

        private static ListNode InsertionSortListFinal(ListNode head)
        {
            ListNode resultHelperNode = new ListNode();
            ListNode cur = head;
            ListNode resultCur = resultHelperNode;

            while (cur != null)
            {
                // resultCur 先回到 result 的開頭
                resultCur = resultHelperNode;

                // 還沒到 result list 的尾端，且，目前 cur target value 大於 目前 result list cur node
                // 一個不符合就中止
                while(resultCur.next != null && resultCur.next.val < cur.val)
                {
                    resultCur = resultCur.next;
                }

                // 重點
                ListNode next = cur.next;

                cur.next = resultCur.next;
                resultCur.next = cur;

                // 重點
                cur = next;
            }

            return resultHelperNode.next;
        }


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
