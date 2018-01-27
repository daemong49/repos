using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

using WindowsServiceTest;

namespace ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                 new SyncService() 
               
            };

            Controller _controller;

            if(Environment.UserInteractive)
            {
                _controller = new Controller();
                _controller.Start();

                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();

                
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }

            
        }
    }
}
