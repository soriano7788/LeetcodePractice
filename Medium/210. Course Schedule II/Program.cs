using System;
using System.Collections.Generic;

namespace _210._Course_Schedule_II
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/course-schedule-ii/
            // 基本敘述和 Course_Schedule 差不多
            // 現在的目標是找出修課的順序
            // 可能有多種順序，不過這題只要回傳任意一種就可以了
            // 假如沒辦法完成全部課程就回傳空陣列

            // output: 0,1
            int numCourses = 2;
            int[][] prerequisites = new int[][] { new int[] { 1, 0 } };

            // output: 0,1,2,3
            numCourses = 4;
            prerequisites = new int[][] 
            { 
                new int[] { 1, 0 },
                new int[] { 2, 0 },
                new int[] { 3, 1 },
                new int[] { 3, 2 }
            };

            numCourses = 1;
            prerequisites = new int[][] { };

            int[] result = FindOrder(numCourses, prerequisites);
            Console.WriteLine("result: {0}", string.Join(", ", result));

        }

        private static int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            // 我要想清楚，定義好自己認為的箭頭關係
            // A -> B   表示 要先修 A 才能修 B
            // A 的 outdegree 為 1
            // B 的 indegree 為 1
            // 所以我要先找出 indegree 為 0 的 course 先上

            int[] indegree = new int[numCourses];
            Array.Fill(indegree, 0);

            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < prerequisites.Length; i++)
            {
                indegree[prerequisites[i][0]]++;
                graph[prerequisites[i][1]].Add(prerequisites[i][0]);
            }

            Queue<int> queue = new Queue<int>();
            int count = 0;
            for (int course = 0; course < numCourses; course++)
            {
                if(indegree[course] == 0)
                {
                    count++;
                    queue.Enqueue(course);
                }
            }

            List<int> result = new List<int>();
            while(queue.Count != 0)
            {
                int course = queue.Dequeue();
                result.Add(course);

                for (int i = 0; i < graph[course].Count; i++)
                {
                    int dependentCourse = graph[course][i];
                    indegree[dependentCourse]--;

                    if(indegree[dependentCourse] == 0)
                    {
                        queue.Enqueue(dependentCourse);
                        count++;
                    }
                }
            }

            if(count == numCourses)
            {
                return result.ToArray();

            }
            return new int[] { };
        }

    }
}
