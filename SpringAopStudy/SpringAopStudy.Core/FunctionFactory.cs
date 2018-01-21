using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringAopStudy.Core
{
    public class FunctionFactory<T>
    {
        public IBusinessCore<T> BusinessCore { get; set; }

        public Func<DateTime , T> GetBusinessFunc()
        {
            return BusinessCore.Send;
        }

       

    }
}
