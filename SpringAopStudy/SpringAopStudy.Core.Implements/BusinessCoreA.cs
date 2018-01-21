using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpringAopStudy.Core;

namespace SpringAopStudy.Core.Implements
{
    public class BusinessCoreA : IBusinessCore<double>
    {
        public double Send(DateTime execDate)
        {
            
            var totalDays = (DateTime.Now - execDate).TotalDays;
            return totalDays;
        }
    }

    public class BusinessCoreB : IBusinessCore<double>
    {
        public double Send(DateTime execDate)
        {
            var totalHours = (DateTime.Now - execDate).TotalHours;
            return totalHours;
        }
    }
}
