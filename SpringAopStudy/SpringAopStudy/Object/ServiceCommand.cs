using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpringAopStudy.Core;

namespace SpringAopStudy
{
    public interface ICommand<T>
    {
        T Execute(DateTime context);
        Func<DateTime, T> BusinessFunc { get; set; }
    }

    public class ServiceCommand<T> : ICommand<T>
    {
        public ServiceCommand()
        {
            _id = Guid.NewGuid().ToString();
            
        }

        private string _id = string.Empty ;

        public Func<DateTime, T> BusinessFunc { get; set; }

        public T Execute(DateTime  context)
        {
            Console.Out.WriteLine($"Service implementaion : {_id}");

            var result = BusinessFunc(context);
            //throw new IndexOutOfRangeException("IndexOutOfRangeException");
            //throw new FormatException("FormatException");
            return result; 
        }

    }
}
