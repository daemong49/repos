using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsServiceTest
{
    public class Controller
    {
        bool _isContinue = false;
        public void Start()
        {
            _isContinue = true;

            Task.Run(() =>
            {
                while(_isContinue)
                {
                    Console.WriteLine("Run...");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Stop");
            });
        }

        public void Stop()
        {
            _isContinue = false;
        }
    }
}
