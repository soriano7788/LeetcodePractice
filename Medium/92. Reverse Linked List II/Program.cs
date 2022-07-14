using System;

namespace _92._Reverse_Linked_List_II
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/reverse-linked-list-ii/
            // 反轉鏈結串列，但是有限定條件
            // input 兩個整數，left 和 right，
            // left <= right
            // 把鏈結串列中，排序 left ~ right 之間的 node 反轉
            // 注意: left 和 right 相當於 index，只不過這邊的 index 是從 1 開始

            // 需考慮 left 在 head 的位置
            // 或 right 在 tail 的位置的情形
            // 或兩種情況都出現的情形


            ListNode head = BuildLinkedList();
            int left = 2, right = 4;

            //head = BuildLinkedList2();
            //left = 1;
            //right = 1;

            //head = BuildLinkedList3();
            //left = 1;
            //right = 2;

            ListNode result = ReverseBetween(head, left, right);
            ShowList(result);
        }



        private static ListNode ReverseBetween(ListNode head, int left, int right)
        {
            if(head == null || left == right)
            {
                return head;
            }

            ListNode current = head;
            ListNode leftNode = null, 
                     rightNode = null;

            ListNode previous = null;
            ListNode previousLeftNode = null;

            int index = 1;

            while (current != null && (leftNode == null || rightNode == null))
            {
                if(index == left)
                {
                    leftNode = current;
                    previousLeftNode = previous;
                }
                if (index == right)
                {
                    rightNode = current;
                }

                previous = current;
                current = current.next;

                index++;
            }

            // 開始反轉，起點從 leftNode 開始
            current = leftNode;
            previous = previousLeftNode;

            // 這等於一遇到 rightNode 就停
            while (previous != rightNode)
            {
                ListNode temp = current.next;
                current.next = previous;
                previous = current;
                current = temp;
            }

            // 表示 left 是 1
            if(previousLeftNode == null)
            {
                //head = rightNode;
                head = previous;
            }
            else
            {
                //previousLeftNode.next = current;
                previousLeftNode.next = previous;
            }

            // 表示 right 指向最尾端
            if (previous.next == null)
            {
                leftNode.next = null;
                //head = rightNode;
            }
            else
            {
                leftNode.next = current;
                //previousLeftNode.next = current;
            }

            return head;
        }

        private static void ShowList(ListNode node)
        {
            while (node != null)
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

        private static ListNode BuildLinkedList2()
        {
            ListNode node5 = new ListNode(5);
            return node5;
        }
        private static ListNode BuildLinkedList3()
        {
            ListNode node3 = new ListNode(3);
            ListNode node5 = new ListNode(5);
            node3.next = node5;

            return node3;
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
