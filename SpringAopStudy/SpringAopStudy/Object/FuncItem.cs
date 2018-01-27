using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SpringAopStudy.Object
{
    public interface IFuncItem
    {
        double Number { get; set; }
        Func<double, double> BusinessFunc { get; set; }
        
        double Sync();

        int ExecuteCount { get; set; }
    }

    public class FuncItem : IFuncItem
    {
        ILog _logger = LogManager.GetLogger(typeof(FuncItem));

        public FuncItem()
        {
            _id = Guid.NewGuid().ToString();
           // _logger.Info($"item created:{_id}");
        }

        private string _id;
        public Func<double, double> BusinessFunc { get; set; }

        public int ExecuteCount { get; set; } = 0;

        public double Number { get; set; }
        
        public double Sync()
        {
            ExecuteCount++;
            System.Threading.Thread.Sleep(new Random().Next(300, 800));
            var result = BusinessFunc(Number);
            //if (Number % 2 == 0)
            //{
            //    throw new IndexOutOfRangeException("Intentionally Exception!!!!!!!!!!!!!!!!!!!");
            //}
            return result;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
