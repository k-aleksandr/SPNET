using LabClassLibrary.BusinessLogic;
using LabClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LabServiceCore.Controllers
{
    [Route("Customers")]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var server = new CustomerServer();
            var Customers = server.GetStoredCollection();

            return Customers;
        }

        [HttpGet]
        [Route("initialize")]
        public void Initialize()
        {
            var server = new CustomerServer();
            var Customers = server.InitializeCollection();
            server.SaveCollection(Customers);
        }

        [HttpPost]
        [Route("add")]
        public void Add([FromBody]Customer Customer)
        {
            EventLog.WriteEntry(
                "Application", 
                DateTime.Now + " Received:" + Customer.Name + " " + Customer.Address);

            var server = new CustomerServer();
            server.AddToCollection(Customer);
        }
    }
}
