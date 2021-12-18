using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace ParralelLabs
{

    class Node {
        //internal Node next;
        internal LinkedListNode<Node> belowLayer;

        public Node (int value)
        {
            Value = value;
            //otherLayers = new List<Node>();
        }

        public int Value { get; }

    }

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

    class SkipList { 
        public int MaxLayers { get; }
        public LinkedList<LinkedList<Node>> Sentinel { get; private set; }
        public SkipList (LinkedList<int> startList, int maxLayers = 4)
        {
            MaxLayers = maxLayers;
            Sentinel = new LinkedList<LinkedList<Node>>();
            var firstLayer = startList.ToList();
            firstLayer.Sort();
            generateAllLayers(new LinkedList<int>(firstLayer));
        }

        public SkipList(List<int> startList, int maxLayers = 4)
        {
            MaxLayers = maxLayers;
            Sentinel = new LinkedList<LinkedList<Node>>();
            var firstLayer = new List<int>(startList);
            firstLayer.Sort();
            generateAllLayers(new LinkedList<int>(startList));
        }

        private void generateAllLayers(LinkedList<int> startList)
        {
            Sentinel.AddFirst(new LinkedList<Node>());
            var currentItem = startList.First;
            while(currentItem != null)
            {
                Sentinel.First.Value.AddLast(new Node(currentItem.Value));
                currentItem = currentItem.Next;
            }

            var latest = Sentinel.First;
            for (int i = 0; i < MaxLayers - 1; i++)
            {
                var newLayer = new LinkedList<Node>();
                Sentinel.AddLast(newLayer);
                var latestToInsert = latest.Value.First;
                while (latestToInsert != null)
                {
                    if (new Random().Next(100) <= 50)
                    {
                        var newNode = new Node(latestToInsert.Value.Value);
                        newNode.belowLayer = latestToInsert;
                        newLayer.AddLast(newNode);
                    }
                    latestToInsert = latestToInsert.Next;
                }
                latest = latest.Next;

            }

        }

        public bool Find(int valueToFind)
        {
            var currentLayer = Sentinel.Last;
            var currentNode = currentLayer.Value.First;
            while (currentLayer != null)
            {
                if (currentNode == null || currentNode.Next == null)
                {
                    currentLayer = currentLayer.Previous;
                    currentNode = currentLayer.Value.First;
                    continue;
                }
                else if (valueToFind == currentNode?.Value?.Value)
                {
                    return true;
                }
                else if (valueToFind == currentNode.Next?.Value?.Value)
                {
                    return true;
                }
                else if (valueToFind < currentNode?.Value?.Value)
                {
                    currentLayer = currentLayer.Previous;
                    currentNode = currentLayer.Value.First;

                }
                else if (valueToFind < currentNode.Next?.Value?.Value)
                {
                    currentLayer = currentLayer.Previous;
                    currentNode = currentNode.Value.belowLayer;
                    continue;
                }
                else if (valueToFind == currentNode.Next?.Value?.Value)
                {
                    return true;
                }
                else
                {
                    //currentLayer = currentLayer.Previous;
                    currentNode = currentNode.Next;
                    continue;
                }
            }
            return false;
        }
        public override string ToString()
        {
            List<string> lines = new List<string>();
            string setw = (Sentinel.First.Value.Last.Value.Value.ToString().Length +2).ToString();
            var latestLine = Sentinel.First;
            
            for (int i = 0; i < MaxLayers; i++)
            {
                var latestEl = latestLine.Value.First;
                string line = "Layer " + (i + 1).ToString() + ": ";
                while (latestEl != null)
                {
                    line += String.Format($"{{0, {setw}}}", $">{latestEl.Value.Value}").Replace(" ", "-");
                    latestEl = latestEl.Next;
                }
                latestLine = latestLine.Next;
                lines.Add(line);
            }
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
            Console.WriteLine(mySkipList.Find(5));
            //Mutex a = new Mutex();

            //return;

        }


    }
}
