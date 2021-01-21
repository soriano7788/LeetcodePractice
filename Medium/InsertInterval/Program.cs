using System;
using System.Collections.Generic;

namespace InsertInterval
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] intervals = new int[][]
            {
                new int[] {1, 3},
                new int[] {6, 9}
            };
            int[] newInterval = new int[] { 2, 5 };
            // output: [1,5] [6,9]


            intervals = new int[][]
            {
                new int[] {1, 2},
                new int[] {3, 5},
                new int[] {6, 7},
                new int[] {8, 10},
                new int[] {12, 16}
            };
            newInterval = new int[] { 4, 8 };
            // output: [1,2] [3,10] [12,16]

            intervals = new int[][] { };
            newInterval = new int[] { 5, 7 };
            // output: [5,7]

            intervals = new int[][]
            {
                new int[] {1, 5}
            };
            newInterval = new int[] { 2, 3 };
            // output: [1,5]


            intervals = new int[][]
            {
                new int[] {1, 5}
            };
            newInterval = new int[] { 2, 7 };
            // output: [1,7]


            int[][] results = Insert(intervals, newInterval);
            foreach (int[] result in results)
            {
                Console.WriteLine(string.Join(", ", result));
            }



        }
        private static int[][] Insert(int[][] intervals, int[] newInterval)
        {
            if (intervals.Length == 0)
                return new int[][] { newInterval };

            // insert new interval 目標花費 O(n)
            bool newIntervalInserted = false;
            List<int[]> temp = new List<int[]>();
            for (int i = 0; i < intervals.Length; i++)
            {
                // 找到可以加入newInterval 的位置了
                if (newInterval[0] < intervals[i][0])
                {
                    temp.Add(newInterval);
                    newIntervalInserted = true;
                    for (int k = i; k < intervals.Length; k++)
                    {
                        temp.Add(intervals[k]);
                    }
                    break;
                }
                temp.Add(intervals[i]);
            }
            if(!newIntervalInserted)
            {
                temp.Add(newInterval);
            }
            intervals = temp.ToArray();

            // return intervals;

            List<int[]> merged = new List<int[]>();
            foreach (int[] interval in intervals)
            {
                if (merged.Count == 0 || (merged[merged.Count - 1][1] < interval[0]))
                {
                    merged.Add(interval);
                }
                else
                {
                    merged[merged.Count - 1][1] = Math.Max(merged[merged.Count - 1][1], interval[1]);
                }
            }
            return merged.ToArray();
        }
    }
}
