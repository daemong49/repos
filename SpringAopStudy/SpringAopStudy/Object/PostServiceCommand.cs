using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpringAopStudy.Object
{
    public class PostServiceCommand : ServiceCommand
    {
        ILog _logger = LogManager.GetLogger(typeof(PostServiceCommand));
        public override void Process()
        {
            foreach (var item in QueueRepository.Instance.PostQueue.GetConsumingEnumerable())
            {
                while (Interlocked.Read(ref _runningTaskNum) == _taskNum)
                {
                    // _logger.Info($"task wait {_runningTaskNum}");
                    Thread.Sleep(100);
                }

                Interlocked.Increment(ref _runningTaskNum);
                Task.Run(() => Run(item));
            }

            _logger.Info($"{nameof(PostServiceCommand)}.Process Completed~~~~~");
        }
    }
}
