using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringAopStudy
{
    public interface ICommand
    {
        object Execute(object context);

    }

    public class ServiceCommand : ICommand
    {
        public object Execute(object context)
        {
            Console.Out.WriteLine($"Service implementaion : {context}");
            //throw new IndexOutOfRangeException("IndexOutOfRangeException");
            throw new FormatException("FormatException");
            return DateTime.Now;
        }
    }
}
