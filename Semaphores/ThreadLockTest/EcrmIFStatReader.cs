using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using System.Collections.Concurrent;

namespace ThreadLockTest
{
    public class EcrmIFStatReader
    {
        ITargetBlock<string> _IFQueue;

        public ITargetBlock<string> IFQueue { get; set; }

        public void Start()
        {
            foreach (var s in Enumerable.Range(0, 10))
                IFQueue.Post(s.ToString());
        }
    }


    public class SyncWorker
    {
        public ISourceBlock<string> IFQueue { get; set; }
        ActionBlock<Task<string>> _saveAction;

        public SyncWorker()
        {
            _saveAction = new ActionBlock<Task<string>>(r =>
           {
               Task.Factory.StartNew(() =>
                    Console.WriteLine($"Result : {r.Result}  thread: {System.Threading.Thread.CurrentThread.ManagedThreadId} : {DateTime.Now.ToString("hhmmssfff")}")
                   );
           }
           );
        }

        public async void Work()
        {
            var syncBlock = new TransformBlock<string, Task<string>>(x => SyncM(x), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 2 });

            var saveBolock2 = new ActionBlock<Task<string>>(r =>
              {
                    Task.Factory.StartNew(()=>
                    Console.WriteLine($"Result200 : {r.Result}  thread: {System.Threading.Thread.CurrentThread.ManagedThreadId} : {DateTime.Now.ToString("hhmmssfff")}")
                );
                }
        );

            syncBlock.LinkTo(_saveAction,  x=> x.Result != "200");
            syncBlock.LinkTo(saveBolock2,  x => x.Result== "200");

            while (await IFQueue.OutputAvailableAsync())
            {
                string input = IFQueue.Receive();
                syncBlock.Post(input);
                
            }
        }

        private Task<String> SyncM(string input)
        {
            Func<string> func = () =>
            {
                int seconds = int.Parse(input) * 10;
                Console.WriteLine($"START : {input} thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(seconds);
                //Console.WriteLine($"{DateTime.Now.ToString("hhmmss:ff")} : {input}");
                return seconds.ToString()  ;
            };
            Task<string> task = Task.Factory.StartNew<string>(func);




            return task;
        }
    }
}
