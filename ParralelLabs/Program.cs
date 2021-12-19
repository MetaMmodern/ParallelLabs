using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace ParralelLabs
{

   

    //class MyList
    //{
    //    internal Node head;
    //    public Node GetLastNode()
    //    {
    //        Node temp = head;
    //        while (temp.next != null)
    //        {
    //            temp = temp.next;
    //        }
    //        return temp;
    //    }
    //    public void InsertLast(int data)
    //    {
    //        Node new_node = new Node(data);
    //        if (head == null)
    //        {
    //            head = new_node;
    //            return;
    //        }
    //        GetLastNode().next = new Node(data);
    //    }
    //    public void InsertAfter(Node prev, int data)
    //    {
    //        Node new_node = new Node(data);
    //        new_node.next = prev.next;
    //        prev.next = new_node;
    //    }

    //    public void InsertAfter(int value, int data)
    //    {
    //        Node prev = FindNode(value);
    //        InsertAfter(prev, data);
    //    }
    //    public Node FindNode(int value)
    //    {
    //        Node result = head;
    //        while (result.Value != value)
    //        {
    //            result = result.next;
    //        }
    //        return result;
    //    }
    //}

    

    class Program
    {
        private void testMutex()
        {
            Mutex a = new Mutex();
        }
        private void testSkipList()
        {
            var myStartList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var mySkipList = new SkipList(myStartList, 5);

            Console.WriteLine(mySkipList.ToString());
            Console.WriteLine(mySkipList.Find(5));
        }
        static void Main(string[] args)
        {
            

        }


    }
}
