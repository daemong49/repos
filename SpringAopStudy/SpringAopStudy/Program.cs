using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spring.Context.Support;
using Spring.Core;
using SpringAopStudy.Core;

using SpringAopStudy.Object;

namespace SpringAopStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = ContextRegistry.GetContext();
            
            var commander = context.GetObject<ICommand<double>>("commander");
            //var factory = context.GetObject<ObjectFactory<double>>("ObjFactory");

            //var commander = factory.CreateInstance();
            //commander.BusinessFunc = factory.BusinessCore.Send;
            var result = commander.Execute(DateTime.Now.AddDays(-1));

            Console.WriteLine($"result:{result}");
           

            

            

            Console.ReadLine();
        }
    }
}
