using System;
using System.Threading;
using System.Collections.Generic;

// src: https://www.cl.cam.ac.uk/research/srg/netos/papers/2001-caslists.pdf

namespace LFLList
{
    class Node<T> {
        public bool marked = false;
        public Node(T data)
        {
            Value = data;
        }

        public T Value { get; private set; }
        public Node<T> Next;
    }

   class LFLList<T>
    {
        public Node<T> Head;
        public Node<T> Tail;

        private bool CAS (ref Node<T> location1, Node<T> value, Node<T> comparand)
        {
            return comparand == Interlocked.CompareExchange(ref location1, value, comparand);
        }
        public LFLList ()
        {
            Head = new Node<T>(default);
            Tail = new Node<T>(default);
            Head.Next = Tail;
        }
        public Node<T> Search(T key, ref Node<T> leftNode)
        {
            Node<T> leftNodeNext = null;
            Node <T> rightNode = null;
            do
            {
                var t = Head;
                var t_next = Head.Next;
                /* 1: Find left_node and right_node */
                do
                {
                    if (!t_next.marked)
                    {
                        leftNode = t;
                        leftNodeNext= t_next;
                    }
                    t_next.marked = false;
                    t = t_next;
                    if (t == Tail) break;
                    t_next = t.Next;
                } while (t_next.marked || Comparer<T>.Default.Compare(t.Value, key) < 0);
                rightNode = t;

                /* 2: Check nodes are adjacent */
                if (leftNodeNext == rightNode)
                    if ((rightNode != Tail) && (rightNode?.Next?.marked ?? false))
                        continue;
                    else
                        return rightNode;

                /* 3: Remove one or more marked nodes */
                if (CAS(ref leftNode.Next, rightNode, leftNodeNext))
                    if ((rightNode != Tail) && (rightNode?.Next?.marked ?? false))
                        continue;
                    else
                        return rightNode;
            }
            while (true);
        }
        public bool Insert(T key)
        {
            var newNode = new Node<T>(key);
            Node<T> rightNode;
            Node<T> leftNode = null;
            do
            {
                rightNode = Search(key, ref leftNode);
                if ((rightNode != Tail) && EqualityComparer<T>.Default.Equals(rightNode.Value, key)) 
                    return false;
                newNode.Next = rightNode;
                if (CAS(ref leftNode.Next, newNode, rightNode)) 
                    return true;
            }
            while (true);
        }
        public bool Find(T search_key)
        {
            Node<T> leftNode = null;
            Node<T> rightNode = Search(search_key, ref leftNode);
            if ((rightNode == Tail) ||
            !EqualityComparer<T>.Default.Equals(rightNode.Value, search_key))
                return false;
            else
                return true;
        }
        public bool Delete(T search_key)
        {
            Node<T> right_node = null;
            Node<T> right_node_next = null; 
            Node<T> left_node = null;

            do
            {
                right_node = Search(search_key, ref left_node);
                if ((right_node == Tail) || !EqualityComparer<T>.Default.Equals(right_node.Value, search_key))
                    return false;
                right_node_next = right_node.Next;
                if (!right_node_next.marked)
                {
                    right_node_next.marked = true;
                    //CAS(ref right_node_next.marked, true, right_node_next.marked);
                    if (CAS(ref (right_node.Next), right_node_next, right_node_next))
                    { break; }
                }
            } 
            while (true);

            if (!CAS(ref (left_node.Next), right_node_next, right_node))
                right_node = Search(right_node.Value, ref left_node);
            return true;
        }
    }
}
