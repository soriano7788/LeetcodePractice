using System;
using System.Collections.Generic;

namespace _155._Min_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://leetcode.com/problems/min-stack/
            // 實作一個 MinStack 資料結構

            // Methods pop, top and getMin operations will always be called on non-empty stacks.
            // 看起來條件算是給很寬鬆

            MinStack minStack = new MinStack();
            minStack.Push(-2);
            minStack.Push(0);
            minStack.Push(-3);
            minStack.GetMin(); // return -3
            minStack.Pop();
            minStack.Top();    // return 0
            minStack.GetMin(); // return -2
        }
        public class MinStack
        {
            /** initialize your data structure here. */
            private List<int> _list;


            public MinStack()
            {
                _list = new List<int>();
            }

            public void Push(int val)
            {
                _list.Insert(0, val);
            }

            public void Pop()
            {
                if(_list.Count != 0)
                {
                    _list.RemoveAt(0);
                }
            }

            public int Top()
            {
                return _list[0];
            }

            public int GetMin()
            {
                if(_list.Count != 0)
                {
                    int min = _list[0];
                    foreach(int element in _list)
                    {
                        if(element < min)
                        {
                            min = element;
                        }
                    }
                    return min;
                }
                throw new Exception("empty");
            }
        }
    }
}
