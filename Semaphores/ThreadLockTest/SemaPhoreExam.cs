using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using System.Threading;

namespace ThreadLockTest
{
    public class SemaPhoreExam
    {
        private Semaphore _pool;

        private int _padding;

        public void Main()
        {
            
            _pool = new Semaphore(0, 3);

            for (int i = 1; i <= 5; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Worker));

                // Start the thread, passing the number.
                //
                t.Start(i);
            }

            // Wait for half a second, to allow all the
            // threads to start and to block on the semaphore.
            //make s
            Thread.Sleep(500);

            // The main thread starts out holding the entire
            // semaphore count. Calling Release(3) brings the 
            // semaphore count back to its maximum value, and
            // allows the waiting threads to enter the semaphore,
            // up to three at a time.
            //
            Console.WriteLine("Main thread calls Release(3).");
            _pool.Release(3);

            Console.WriteLine("Main thread exits.");

        }

        private void Worker(object num)
        {
            // Each worker thread begins by requesting the
            // semaphore.
            Console.WriteLine("{1} Thread {0} begins " +
                "and waits for the semaphore.", num, DateTime.Now.ToString("mm:ss fff"));
            _pool.WaitOne();

            // A padding interval to make the output more orderly.
            int padding = Interlocked.Add(ref _padding, 100);

            Console.WriteLine("Thread {0} enters the semaphore.", num);  

            // The thread's "work" consists of sleeping for 
            // about a second. Each thread "works" a little 
            // longer, just to make the output more orderly.
            //
            Thread.Sleep(1000 + padding);

            Console.WriteLine("{1} Thread {0} releases the semaphore.", num, DateTime.Now.ToString("mm:ss fff"));
            Console.WriteLine("{2} Thread {0} previous semaphore count: {1}",
                num, _pool.Release(), DateTime.Now.ToString("mm:ss fff"));
        }
    }
}
