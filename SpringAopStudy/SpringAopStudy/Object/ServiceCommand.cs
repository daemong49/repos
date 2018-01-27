using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SpringAopStudy.Object;
using SpringAopStudy.Core;
using System.Threading;

namespace SpringAopStudy
{
    public interface ICommand
    {
        void Execute();

        void Run(IFuncItem item);
       
        void Process();

        bool IsWaiting { get; set; }
        
    }

    public class ServiceCommand : ICommand
    {
        ILog _logger = LogManager.GetLogger(typeof(ServiceCommand));

        public ServiceCommand()
        {
            _id = Guid.NewGuid().ToString();
            _logger.Info($"ServiceCommand created:{_id}");

        }

        private string _id = string.Empty ;
        public IObjectFactory ObjFactory { get; set; }

        public bool IsWaiting { get; set; } = true;

        protected long _runningTaskNum = 0;
        protected int _taskNum = 3;


        public void Execute()
        {
            IsWaiting = false;
            Task.Run(() => Process());
            IsWaiting = true;
            //throw new IndexOutOfRangeException("IndexOutOfRangeException");
            //throw new FormatException("FormatException");

        }

        public virtual void Process()
        {
            foreach (var item in QueueRepository.Instance.Queue.GetConsumingEnumerable())
            {
                while(Interlocked.Read(ref _runningTaskNum) == _taskNum)
                {
                   // _logger.Info($"task wait {_runningTaskNum}");
                    Thread.Sleep(100);
                }

                Interlocked.Increment(ref _runningTaskNum);
                Task.Run(()=> Run(item));
            }
            _logger.Info($"{nameof(ServiceCommand)}.Process Completed~~~~~");
        }


        public  void Run(IFuncItem item)
        {

            try
            {
                var result = item.Sync();

                if (item.ExecuteCount == 1)
                {
                    item.BusinessFunc = (n) => { return Math.Pow(n, 3); };
                    item.Number = result;

                    QueueRepository.Instance.PostQueue.Add(item);
                }
                else
                {
                    QueueRepository.Instance.FinalQueue.Add(result.ToString());
                }
            }
            catch(Exception ex)
            {
                QueueRepository.Instance.FinalQueue.Add("ERROR");
            }
            finally
            {
                
                Interlocked.Decrement(ref _runningTaskNum);

            }

        }

    }
}
