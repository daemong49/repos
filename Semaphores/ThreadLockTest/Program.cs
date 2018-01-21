using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Threading.Tasks.Dataflow;

namespace ThreadLockTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BufferBlock<string> que = new BufferBlock<string>();

            SyncWorker sync = new SyncWorker();
            sync.IFQueue = que;
            sync.Work();

            Console.WriteLine("input:");
            string input;
            while (true)
            {
                input = Console.ReadLine();

                que.Post(input);
            }
            
        }
    }
}
