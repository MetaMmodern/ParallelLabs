using System;
using System.Threading;

namespace ParralelLabs
{
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
    }
}
