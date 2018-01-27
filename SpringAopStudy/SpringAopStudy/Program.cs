using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Spring.Context.Support;
using Spring.Core;
using SpringAopStudy.Core;
using Spring.Context;
using log4net;

using SpringAopStudy.Object;

namespace SpringAopStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = ContextRegistry.GetContext();

            var main = context.GetObject<IController>("Main");
            main.Context = context;
            main.Start();


            Console.ReadLine();
        }


       
    }

    public interface IController
    {
        void Start();

        void Stop();

        IApplicationContext Context { get; set; }
    }

    public class MainController : IController
    {
        ILog _logger = LogManager.GetLogger(typeof(FuncItem));

        System.Timers.Timer _timer = new System.Timers.Timer();

        public IApplicationContext Context { get; set; }

        public ICommand Commander { get; set; }

        public ICommand PostCommander { get; set; }

        public IReader IFReader { get; set; }

        public IWriter Writer { get; set; }


        public void Start()
        {
            IFReader.ObjFactory.Context = Context;

            _timer.Elapsed += Process;
            _timer.Interval = 20000;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void Process(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Commander.IsWaiting)
            {
                

                QueueRepository.Instance.QueueReset();
                try
                {
                    int num = IFReader.Execute();
                    
                    _logger.Info($"{nameof(ServiceCommand)}.Process Completed~~~~~");

                    Commander.Execute();
                    PostCommander.Execute();
                    Writer.ItemCount = num;
                    Writer.Execute();
                }
                catch { }
                
                
            }
        }
    }
        
}
