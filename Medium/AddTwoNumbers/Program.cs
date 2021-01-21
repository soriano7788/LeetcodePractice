using System;

namespace AddTwoNumbers
{
    /**
     * Definition for singly-linked list.
     * public class ListNode {
     *     public int val;
     *     public ListNode next;
     *     public ListNode(int val=0, ListNode next=null) {
     *         this.val = val;
     *         this.next = next;
     *     }
     * }
     */
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
    class Program
    {
        static void Main(string[] args)
        {
            // todo: 直接轉成正常數字後相加，再轉成 List 就夠了... 靠
            // 假如數字大小超過 int 範圍就會爆，所以像 list 可以是一種存放極大數字的方法

            ListNode l1 = GenerateReversedListNumber(18);
            RenderListNumber(l1);
            ListNode l2 = GenerateReversedListNumber(0);
            RenderListNumber(l2);

            ListNode l3 = AddTwoNumbers(l1, l2);
            RenderListNumber(l3);
        }

        // LeetCode 的說明，head 當作 dummy
        private static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode head = new ListNode();
            ListNode current = head;
            int carryDigit = 0; // 進位的數字

            while (l1 != null || l2 != null)
            {
                ListNode node = new ListNode();

                int l1Val = (l1 == null ? 0 : l1.val);
                int l2Val = (l2 == null ? 0 : l2.val);

                int sum = l1Val + l2Val + carryDigit;
                node.val = sum % 10;
                carryDigit = sum / 10;  // 或是 sum > 10 的話，就設為 1

                current.next = node;
                // current = node;
                current = current.next;
                // current.next = null;

                if (l1 != null)
                {
                    l1 = l1.next;
                }
                if (l2 != null)
                {
                    l2 = l2.next;
                }
            }

            if (carryDigit != 0)
            {
                ListNode node = new ListNode(carryDigit);
                current.next = node;
            }
            head = head.next;
            return head;
        }

        private static ListNode GenerateReversedListNumber(int num)
        {
            // todo: 直接把數字變 string 來處理，各別字元處理好像更輕鬆?
            ListNode head = new ListNode();
            ListNode current = new ListNode();
            int digitCount = num.ToString().Length;
            int i = digitCount;
            i = 1;

            bool isFirst = true;
            while (i <= digitCount)
            {
                int modNum = PowerOfTen(i);
                int digitNum = num % modNum;
                int divideNum = PowerOfTen(i - 1);
                digitNum = digitNum / divideNum;  // 注意除數為0
                ListNode node = new ListNode();

                if (isFirst)
                {
                    head = node;
                    isFirst = false;
                }


                node.val = digitNum;
                current.next = node;
                current = node;
                current.next = null;
                i++;
            }
            return head;
        }
        private static void RenderListNumber(ListNode node)
        {
            int i = 0;
            while (node != null)
            {
                Console.Write(node.val);
                node = node.next;
                if (node != null)
                {
                    Console.Write(" -> ");
                }
                i++;
                if (i >= 50)
                {
                    Console.WriteLine("i > 50");
                    break;
                }
            }
            Console.WriteLine();
        }

        private static int PowerOfTen(int power)
        {
            if (power == 0)
                return 1;
            return 10 * PowerOfTen(power - 1);
        }
        private static bool NodeIsZero(ListNode node)
        {
            if (node.val == 0 && node.next == null)
                return true;
            return false;
        }
    }
}
