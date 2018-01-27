using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data.SqlClient;

namespace SpringAopStudy.Object
{
    public interface IReader
    {
        int Execute();

        IObjectFactory ObjFactory { get; set; }


    }

    public class Reader : IReader
    {
        ILog _logger = LogManager.GetLogger(typeof(ServiceCommand));
        public IObjectFactory ObjFactory { get; set; }

        public int Execute()
        {
            int num = new Random().Next(1, 30);
                
            _logger.Info($"num:{num}");

            for (int i = 0; i < num; i++)
            {
                var item = ObjFactory.ItemBuild(i);
                QueueRepository.Instance.Queue.Add(item);
            }

            
            return num;
        }
    }
}
