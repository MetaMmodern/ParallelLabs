using System;
using System.Collections.Generic;
using System.Threading;

namespace ParralelLabs
{
    class SkipList {
        public List<LinkedList<int>> layers;
        public SkipList()
        {
            layers = new List<LinkedList<int>>();
        }
        private int AddLayer() {
            var layerLevel = 0; 
            return layerLevel;
        }
        private void DeleteLayer()
        {
            var layerLevel = 0;
        }
        public void Add(int value) { }
        public int Get(int value) { return 0; }
        public void Delete() { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Hello World Thread";
            Console.WriteLine(Thread.CurrentThread.Name);
            return;
        }

        bool Cas(ref int a, int oldValue, int newValue) {
            if (a != oldValue)
                return false;
            a = newValue;
            return true;

        }
    }
}
