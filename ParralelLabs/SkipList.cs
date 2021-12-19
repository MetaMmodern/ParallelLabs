using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace ParralelLabs
{
    class Node
    {
        //internal Node next;
        internal LinkedListNode<Node> belowLayer;

        public Node(int value)
        {
            Value = value;
            //otherLayers = new List<Node>();
        }

        public int Value { get; }

    }
    class SkipList
    {
        public int MaxLayers { get; }
        public LinkedList<LinkedListNode<Node>> Sentinel { get; private set; }
        public SkipList(LinkedList<int> startList, int maxLayers = 4)
        {
            MaxLayers = maxLayers;
            Sentinel = new LinkedList<LinkedListNode<Node>>();
            var firstLayer = startList.ToList();
            firstLayer.Sort();
            generateAllLayers(new LinkedList<int>(firstLayer));
        }

        public SkipList(List<int> startList, int maxLayers = 4)
        {
            MaxLayers = maxLayers;
            Sentinel = new LinkedList<LinkedListNode<Node>>();
            var firstLayer = new List<int>(startList);
            firstLayer.Sort();
            generateAllLayers(new LinkedList<int>(startList));
        }

        private void generateAllLayers(LinkedList<int> startList)
        {
            var firstLayer = new LinkedList<Node>();
            var currentItem = startList.First;
            while (currentItem != null)
            {
                firstLayer.AddLast(new Node(currentItem.Value));
                currentItem = currentItem.Next;
            }
            Sentinel.AddFirst(firstLayer.First);

            var latest = Sentinel.First;
            for (int i = 0; i < MaxLayers - 1; i++)
            {
                var newLayer = new LinkedList<Node>();
                //newLayer.AddLast(new Node(-1));

                var latestToInsert = latest.Value;
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
                Sentinel.AddLast(newLayer.First);
                latest = latest.Next;
            }

        }

        public bool Find(int valueToFind)
        {
            var currentLayer = Sentinel.Last;
            var currentNode = currentLayer.Value;
            while (currentLayer != null)
            {
                if (currentNode == null || currentNode.Next == null)
                {
                    currentLayer = currentLayer.Previous;
                    currentNode = currentLayer.Value;
                    continue;
                }
                //else if (valueToFind == currentNode?.Value?.Value)
                //{
                //    return true;
                //}
                else if (valueToFind == currentNode.Next?.Value?.Value)
                {
                    return true;
                }
                //else if (valueToFind < currentNode?.Value?.Value)
                //{
                //    currentLayer = currentLayer.Previous;
                //    currentNode = currentLayer.Value.First;

                //}
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
            string setw = (Sentinel.First.Value.List.Last.Value.Value.ToString().Length + 2).ToString();
            var latestLine = Sentinel.First;

            for (int i = 0; i < MaxLayers; i++)
            {
                var latestEl = latestLine.Value;
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

}
