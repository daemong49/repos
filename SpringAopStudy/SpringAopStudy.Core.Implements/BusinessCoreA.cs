using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpringAopStudy.Core;

namespace SpringAopStudy.Core.Implements
{
    public class BusinessCoreA : IBusinessCore
    {
        public double Send(double num)
        {
            var result = num + 1;

            return result;
        }
    }



    public class BusinessCoreB : IBusinessCore
    {
        public double Send(double num)
        {
            double result = Math.Pow(num, 2);

            return result;
        }
    }
}
