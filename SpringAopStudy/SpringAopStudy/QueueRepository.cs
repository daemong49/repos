using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpringAopStudy.Object;

namespace SpringAopStudy
{
    public class QueueRepository
    {
        BlockingCollection<IFuncItem> IFQueue = null;
        BlockingCollection<IFuncItem> CrossQueue = null;

        BlockingCollection<string> ResultQueue = null;

        private static readonly Lazy<QueueRepository> _instance
           = new Lazy<QueueRepository>(() => new QueueRepository());

        private QueueRepository()
        {

        }
        
        public static QueueRepository Instance
        {
            get { return _instance.Value; }
        }

        public BlockingCollection<IFuncItem>  Queue
        {
            get{ return IFQueue; }
        }

        public BlockingCollection<IFuncItem> PostQueue
        {
            get { return CrossQueue; }
        }

        public BlockingCollection<string> FinalQueue
        {
            get { return ResultQueue; }
        }


        public void QueueReset()
        {
            IFQueue = new BlockingCollection<IFuncItem>();
            CrossQueue = new BlockingCollection<IFuncItem>();
            ResultQueue = new BlockingCollection<string>();
        }

        public void QueueClosing()
        {
            IFQueue.CompleteAdding();
            CrossQueue.CompleteAdding();
            ResultQueue.CompleteAdding();
        }


    }
}
