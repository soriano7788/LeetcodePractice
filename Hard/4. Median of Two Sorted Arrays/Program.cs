using System;

namespace MedianOfTwoSortedArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/median-of-two-sorted-arrays/
            int[] nums1 = new int[] {1, 3};
            int[] nums2 = new int[] {2};

            // nums1 = new int[] {1, 2};
            // nums2 = new int[] {3, 4};

            double median = FindMedianSortedArrays(nums1, nums2);
            Console.WriteLine("median: {0}", median);
        }
        private static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int totalCount = nums1.Length + nums2.Length;
            // odd 奇數
            // even 偶數
            bool isOdd = true;
            if(totalCount % 2 == 0)
            {
                isOdd = false;
            }
            double halfOfCount = (double)totalCount / 2;

            int[] nums3 = new int[totalCount];
            int i = 0,
                j = 0,
                k = 0;
            while(k < totalCount && (i < nums1.Length) && (j < nums2.Length))
            {
                if(nums1[i] < nums2[j])
                {
                    nums3[k++] = nums1[i++];
                }
                else
                {
                    nums3[k++] = nums2[j++];
                }
            }
            while(k < totalCount && i < nums1.Length)
            {
                nums3[k++] = nums1[i++];
            }

            while(k < totalCount && j < nums2.Length)
            {
                nums3[k++] = nums2[j++];
            }

            if(isOdd)
            {
                return nums3[(int)halfOfCount];
            }
            else
            {
                return (double)(nums3[(int)halfOfCount] + nums3[(int)halfOfCount -1]) / 2;
            }
        }
    }
}
