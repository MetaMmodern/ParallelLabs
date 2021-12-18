using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace ParralelLabs
{

    class Node {
        //internal Node next;
        internal Node next;

        public Node (int value)
        {
            Value = value;
            next = null;
        }

        public int Value { get; }

    }

    class MyList
    {
        internal Node head;
        public Node GetLastNode()
        {
            Node temp = head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            return temp;
        }
        public void InsertLast(int data)
        {
            Node new_node = new Node(data);
            if (head == null)
            {
                head = new_node;
                return;
            }
            GetLastNode().next = new Node(data);
        }
        public void InsertAfter(Node prev, int data)
        {
            Node new_node = new Node(data);
            new_node.next = prev.next;
            prev.next = new_node;
        }

        public void InsertAfter(int value, int data)
        {
            Node prev = FindNode(value);
            InsertAfter(prev, data);
        }
        public Node FindNode(int value)
        {
            Node result = head;
            while (result.Value != value)
            {
                result = result.next;
            }
            return result;
        }
    }

    class SkipList { 
        public LinkedList<LinkedList<int>> Sentinel { get; private set; }
        public int MaxLayers { get; }
        public SkipList (LinkedList<int> startList, int maxLayers = 4)
        {
            MaxLayers = maxLayers;
            Sentinel = new LinkedList<LinkedList<int>>();
            var firstLayer = startList.ToList();
            firstLayer.Sort();
            generateAllLayers(new LinkedList<int>(firstLayer));
        }

        public SkipList(List<int> startList, int maxLayers = 4)
        {
            MaxLayers = maxLayers;
            Sentinel = new LinkedList<LinkedList<int>>();
            var firstLayer = new List<int>(startList);
            firstLayer.Sort();
            generateAllLayers(new LinkedList<int>(startList));
        }

        private void generateAllLayers(LinkedList<int> startList)
        {
            Sentinel.AddFirst(startList);
            var latest = Sentinel.First;
            for (int i = 0; i < MaxLayers; i++)
            {
                LinkedList<int> newLayer = new LinkedList<int>();
                Sentinel.AddLast(newLayer);
                var latestToInsert = latest.Value.First;
                while (latestToInsert != null)
                {
                    if (new Random().Next(100) <= 50)
                    {
                        newLayer.AddLast(latestToInsert.Value);
                    }
                    latestToInsert = latestToInsert.Next;
                }
                latest = latest.Next;

            }

        }

        public override string ToString()
        {
            List<string> lines = new List<string>();
            string setw = (Sentinel.First.Value.Last.Value.ToString().Length +2).ToString();
            var latestLine = Sentinel.First;
            for (int i = 0; i < MaxLayers; i++)
            {
                var latestEl = latestLine.Value.First;
                string line = "Layer " + i.ToString() + ": ";
                while (latestEl != null)
                {
                    line += String.Format($"{{0, {setw}}}", $">{latestEl.Value}").Replace(" ", "-");
                    latestEl = latestEl.Next;
                }
                latestLine = latestLine.Next;
                lines.Add(line);
            }
            lines.Reverse();
            return String.Join("\n", lines);
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            //LinkedList<int> a = new LinkedList<int>();
            //a.AddLast(1);
            //var next = a.Last;
            //Console.WriteLine(next == null);
            var myStartList = new List<int> {1, 2, 3 , 4, 5, 6, 7, 8, 9, 10, 11};
            var mySkipList = new SkipList(myStartList, 5);

            Console.WriteLine(mySkipList.ToString());
            //Mutex a = new Mutex();

            //return;

        }


    }
}
