using LabClassLibrary.Models;
using LabWinService.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabWinService
{
    public partial class Service1 : ServiceBase
    {
        private TcpClientHelper<Customer> _tcpClient;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (var sw = File.CreateText(@"C:\servicelog.log"))
            {
                sw.WriteLine("Service started");
            }

            _tcpClient = new TcpClientHelper<Customer>("127.0.0.1", 8888);
            _tcpClient.DataReceived += HandleData;

            var t = new Thread(new ThreadStart(_tcpClient.StartListening));
            t.Start();
        }

        void HandleData(Customer Customer)
        {
            EventLog.WriteEntry(DateTime.Now + " Received:" + Customer.Name + " " + Customer.Address);

            using (var sw = File.AppendText(@"D:\project\applog.log"))
            {
                sw.WriteLine(DateTime.Now + " Received:" + Customer.Name);
            }
        }

        protected override void OnStop()
        {
            _tcpClient.StopListening();
        }
    }
}
