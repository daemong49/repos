using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpringAopStudy.Core;

namespace SpringAopStudy.Object
{
    public class ObjectFactory<T>
    {

        public IBusinessCore<T> BusinessCore { get; set; }


        public ServiceCommand<T> CreateInstance()
        {
            var commander = new ServiceCommand<T>
            {
                BusinessFunc = BusinessCore.Send
            };

            return commander;
        }

    }
}
