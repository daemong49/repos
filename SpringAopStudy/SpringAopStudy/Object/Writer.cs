using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using log4net;

namespace SpringAopStudy.Object
{
    public interface IWriter
    {
        void Execute();
        int ItemCount { set; }
    }

    public class Writer : IWriter
    {

        ILog _logger = LogManager.GetLogger(typeof(Writer));
        int _itemCount = 0;
        int _competedCount = 1;

        public int ItemCount { set { _itemCount = value; } }

        public void Execute()
        {
            Task.Run(() =>
            {
                _competedCount = 1;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                foreach(var item in QueueRepository.Instance.FinalQueue.GetConsumingEnumerable())
                {
                    WriteBusiness(item);
                }

                stopWatch.Stop();
                
                _logger.Info($"{nameof(Writer)}.Process Completed {stopWatch.ElapsedMilliseconds.ToString("n0")}");
            });
        }

        private void WriteBusiness(string item)
        {
            int num = Interlocked.Decrement(ref _itemCount);
            _logger.Info($"RESULT:[{_competedCount++}] {item}");
            if (num.Equals(0))
            {
                QueueRepository.Instance.QueueClosing();
            }
        }
    }
}
