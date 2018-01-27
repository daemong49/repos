using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spring.Context;
using SpringAopStudy.Core;

namespace SpringAopStudy.Object
{
    public interface IObjectFactory
    {
        IApplicationContext Context { get; set; }

        IFuncItem ItemBuild(double number);
    }

    public class ObjectFactory : IObjectFactory
    {
        
        public IApplicationContext Context { get; set; }

        public IFuncItem ItemBuild(double number)
        {
            var funcItem = Context.GetObject<IFuncItem>("funcItem");

            funcItem.Number = number;

            funcItem.BusinessFunc = (n) => 
            {
                return  Math.Pow(n, 2);
            };

            return funcItem;
        }

    }
}
