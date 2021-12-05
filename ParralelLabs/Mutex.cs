using System;
using System.Threading;


namespace ParralelLabs
{
    class Mutex
    {
        private int flag = 0;
        private Thread lockThread;
        private bool notify = false;
        public void _lock()
        {
            int currentValue = flag;
            int newValue = 1;
            while (Interlocked.CompareExchange(ref flag, newValue, newValue) == 0)
            {
                Thread.Yield();
                currentValue = flag;
            }
            lockThread = Thread.CurrentThread;
        }
        public void _unlock()
        {
            lockThread = null;
            Interlocked.Exchange(ref flag, 0);
        }
        public void _wait()
        {
            Thread currentThread = Thread.CurrentThread;
            if (lockThread != currentThread)
            {
                throw new Exception("Concurrent Modification");
            }
            this._unlock();
            while (notify == false)
            {
                Thread.Yield();
            }
            this._lock();
            notify = false;
        }
        public void _notify()
        {
            Thread currentThread = Thread.CurrentThread;
            if (lockThread != currentThread)
            {
                throw new Exception("Concurrent Modification");
            }
            notify = true;
        }
        public void _notifyAll() { }

    }
}
