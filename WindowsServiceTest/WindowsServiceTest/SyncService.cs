using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceTest
{
    public partial class SyncService : ServiceBase
    {
        public SyncService()
        {
            InitializeComponent();
        }

        Controller _controller = null;

        protected override void OnStart(string[] args)
        {
            _controller = new Controller();
            _controller.Start();
        }

        protected override void OnStop()
        {
            // TODO: 서비스를 중지하는 데 필요한 작업을 수행하는 코드를 여기에 추가합니다.
            _controller.Stop();
        }
    }
}
