using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spring.Context.Support;
using Spring.Core;

namespace SpringAopStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = ContextRegistry.GetContext();
            
            var commander = context.GetObject<ICommand>("commander");

            commander.Execute(DateTime.Now);
           

            

            

            Console.ReadLine();
        }
    }
}
