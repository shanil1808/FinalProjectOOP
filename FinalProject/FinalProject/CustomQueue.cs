using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CustomQueue<T> : IQueueable<T>
    {
        private List<T> list = new List<T>();

        public void Enqueue(T item)
        {
            list.Add(item);
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new EmptyStructureException("Queue empty");

            T item = list[0];
            list.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new EmptyStructureException("Queue empty");

            return list[0];
        }

        public bool IsEmpty()
        {
            return list.Count == 0;
        }
    }
}
