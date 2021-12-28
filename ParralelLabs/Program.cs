using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MSQueue;
using LFLList;
using Akka.Actor;
using Library;
using LibraryMessages;
namespace ParralelLabs
{
    class Program
    {
        private static void testMutex()
        {
            Mutex a = new Mutex();
        }
        private static void testSkipList()
        {
            var myStartList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var mySkipList = new SkipList(myStartList, 5);

            Console.WriteLine(mySkipList.ToString());
            Console.WriteLine(mySkipList.Find(5));
        }
        private static void testNBQueue()
        {
            var q = new NBQueue<int>();
            q.Push(1);
            q.Push(30);
            q.Push(2);
            Console.WriteLine(q.Pop());
            Console.WriteLine(q.Pop());
            Console.WriteLine(q.Pop());
        }
        private static void testLflList()
        {
            var l = new LFLList<int>();
            l.Insert(1);
            l.Insert(2);
            l.Insert(3);
            Console.WriteLine("good");
            l.Delete(1);
            Console.WriteLine("good");

        }

        private static void testActors()
        {
            var librarySystem = ActorSystem.Create("Library");
            var library = librarySystem.ActorOf(Props.Create<LibraryActor>());
            var visitor1 = librarySystem.ActorOf(Props.Create<VisitorActor>("1"));
            var visitor2 = librarySystem.ActorOf(Props.Create<VisitorActor>("2"));
            library.Tell(new ListBooks(), visitor1);
            library.Tell(new TakeBookHome("1"), visitor1);
            library.Tell(new TakeBookHome("1"), visitor2);
            library.Tell(new TakeBookHome("2"), visitor2);
            Console.ReadLine();
            visitor1.Tell(new ShowABook());
            visitor2.Tell(new ShowABook());
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            //testNBQueue();
            //testLflList();
            //testSkipList();
            testActors();
        }


    }
}
