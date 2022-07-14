using System;
using System.Collections.Generic;

namespace _207._Course_Schedule
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/course-schedule/
            // 模擬一個上課安排的情境
            // 像是要修"演算法"前，必須先修過"計算機概論"
            // input 有兩個
            // 第一個，課程的數量，編號為 0 ~ n-1
            // 第二個，記錄修課的相依關係，e.g. [0, 1] 表示要上 0 之前，要先上過 1
            // 判斷是否能修完全部的課程

            // output: true
            int numCourese = 2;
            int[][] prerequisites = new int[][] { new int[] { 0, 1 } };

            // 1 0 互相依賴，變成 deadlock
            // output: false
            numCourese = 2;
            prerequisites = new int[][]
            {
                new int[] { 1, 0 },
                new int[] { 0, 1 }
            };

            // 還有一門課 同時依賴另外兩門課的 case
            // output: false
            numCourese = 3;
            prerequisites = new int[][]
            {
                new int[] { 1, 0 },
                new int[] { 1, 2 },
                new int[] { 0, 1 },
            };

            // output: true
            numCourese = 2;
            prerequisites = new int[][] { new int[] { 1, 0 } };

            try
            {
                bool result = CanFinish(numCourese, prerequisites);
                Console.WriteLine("result: {0}", result);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ex: " + ex);
            }
        }
        private static bool CanFinish(int numCourses, int[][] prerequisites)
        {
            // 目標先找出不需要先修的課程，假如每門課都有先修條件，就變 deadlock
            // 存在循環依賴的關係，也會造成 deadlock

            // 或者是否能夠一口氣就找出某門課的依賴關係鍊

            int[] independentCourse = FindIndependentCourse(numCourses, prerequisites);
            if (independentCourse.Length == 0)
            {
                return false;
            }

            bool[] finishedCourses = new bool[numCourses];
            Array.Fill(finishedCourses, false);

            // 先把無依賴的課程上完
            for (int i = 0; i < independentCourse.Length; i++)
            {
                finishedCourses[independentCourse[i]] = true;
            }

            bool hasTakeCourse = true;
            while (hasTakeCourse)
            {
                hasTakeCourse = false;
                for (int course = 0; course < numCourses; course++)
                {
                    // course 已上過，略過
                    if (finishedCourses[course])
                    {
                        continue;
                    }

                    // course 還沒上過，執行下面
                    List<int> dependentCourses = new List<int>();
                    for (int k = 0; k < prerequisites.Length; k++) // loop 課程依賴關係結構，找出目標 course
                    {
                        // todo: 注意， A course 同時依賴 B 和 C course 的 case 存在
                        if(prerequisites[k][0] == course)
                        {
                            dependentCourses.Add(prerequisites[k][1]);
                        }
                    }

                    bool allSolve = true;
                    foreach(int dependentCourse in dependentCourses)
                    {
                        if(finishedCourses[dependentCourse] == false)
                        {
                            allSolve = false;
                        }
                    }
                    if(allSolve)
                    {
                        finishedCourses[course] = true;
                        hasTakeCourse = true;
                    }

                }

                if(!hasTakeCourse)
                {
                    break;
                }
            }

            foreach(bool finished in finishedCourses)
            {
                if(!finished)
                {
                    return false;
                }
            }

            return true;
        }
        private static int[] FindIndependentCourse(int numCourses, int[][] prerequisites)
        {
            List<int> independentCourses = new List<int>();
            for (int course = 0; course < numCourses; course++)
            {
                bool containDependency = false;
                for (int k = 0; k < prerequisites.Length; k++)
                {
                    int[] courseDependency = prerequisites[k];
                    if (courseDependency[0] == course)
                    {
                        containDependency = true;
                        break;
                    }
                }

                if (containDependency == false)
                {
                    independentCourses.Add(course);
                }
            }
            return independentCourses.ToArray();
        }


        private static bool CanFinish2(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = new List<int>[numCourses];
            int[] degree = new int[numCourses];
            Queue<int> queue = new Queue<int>();
            int count = 0;

            for(int i = 0; i < numCourses; i++)
            {
                graph[i] = new List<int>();
            }


            // 目前對 degree 的用法尚不能理解，目前不被任何 course 依賴的 course，就納入計算
            // 照理說應該是不依賴其他 course 的 course 才可以先納入計算才對??
            // 這裡的 degree 其實就是 indegree，箭頭指向這個 node(course)

            for(int i = 0; i < prerequisites.Length; i++)
            {
                // 算分支度? 看這門課被幾個 course 依賴
                degree[prerequisites[i][1]]++;
                // 要上 prerequisites[i][0] 之前，要先上過 prerequisites[i][1]
                graph[prerequisites[i][0]].Add(prerequisites[i][1]);
            }

            for(int i = 0; i < degree.Length; i++)
            {
                if(degree[i] == 0)
                {
                    queue.Enqueue(i);
                    count++;
                }
            }

            while(queue.Count != 0)
            {
                int course = queue.Dequeue();
                // graph[course].Count 表示，要上 course 前，需要先上過的其他 course 的數量
                for (int i = 0; i < graph[course].Count; i++)
                {
                    int pointer = graph[course][i];
                    degree[pointer]--;
                    if(degree[pointer] == 0)
                    {
                        queue.Enqueue(pointer);
                        count++;
                    }
                }
            }

            if(count == numCourses)
            {
                return true;
            }
            return false;
        }

        private static bool CanFinish3(int numCourses, int[][] prerequisites)
        {
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                graph[i] = new List<int>();
            }

            int[] outdegree = new int[numCourses];
            for (int i = 0; i < prerequisites.Length; i++) 
            {
                // 計算此 course 需要的先修 course 數量
                outdegree[prerequisites[i][0]]++;
                // 計算此 course 需要先修哪些課
                graph[prerequisites[i][0]].Add(prerequisites[i][1]);
            }

            Queue<int> queue = new Queue<int>();
            int count = 0;
            for (int course = 0; course < outdegree.Length; course++) 
            {
                if(outdegree[course] == 0)
                {
                    queue.Enqueue(course);
                    count++;
                }
            }

            while (queue.Count != 0) 
            {
                int course = queue.Dequeue();
                for (int i = 0; i < graph[course].Count; i++) 
                {
                    // pointer 是需要先修過 course，才能上的課
                    int pointer = graph[course][i];
                    outdegree[pointer]--;

                    // 表示先修 course 都上完了
                    if (outdegree[pointer] == 0) 
                    {
                        queue.Enqueue(pointer);
                        count++;
                    }
                }
            }

            return (count == numCourses);
        }
    }
}
