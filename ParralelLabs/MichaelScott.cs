using System;
using System.Threading;

// src: https://neerc.ifmo.ru/wiki/index.php?title=%D0%9E%D1%87%D0%B5%D1%80%D0%B5%D0%B4%D1%8C_%D0%9C%D0%B0%D0%B9%D0%BA%D0%BB%D0%B0_%D0%B8_%D0%A1%D0%BA%D0%BE%D1%82%D1%82%D0%B0

namespace MSQueue
{
    class Node<T> {

        public Node(T data, Node<T> next)
        {
            Value = data;
            Next = next;
        }

        public T Value { get; private set; }
        public Node<T> Next;
    }

   class NBQueue<T>
    {
        public Node<T> Head;
        public Node<T> Tail;

        private bool CAS (ref Node<T> location1, Node<T> value, Node<T> comparand)
        {
            return comparand == Interlocked.CompareExchange(ref location1, value, comparand);
        }
        public NBQueue ()
        {
            var dummyNode = new Node<T>(default(T), null);
            Head = dummyNode;
            Tail = dummyNode;
        }
        public void Push(T value)
        {
            var node = new Node<T>(value, null);
            while (true)
            {
                var tail = Tail;
                if (tail != null && CAS(ref tail.Next, node, null))
                {
                    CAS(ref Tail, node, tail);
                    return;
                }
                else
                {
                    CAS(ref Tail, tail.Next, tail);
                }
            }
        }
        public T Pop()
        {
            while (true)
            {
                var head = Head;
                var tail = Tail;
                var nextHead = head.Next;
                if (head == tail)
                {
                    if (nextHead == null) throw new Exception("Empty");
                    else CAS(ref Tail, nextHead, tail);
                }
                else
                {
                    var result = nextHead;
                    if (CAS(ref Head, nextHead, head)) return result.Value;
                }
            }
        }
    }
}
