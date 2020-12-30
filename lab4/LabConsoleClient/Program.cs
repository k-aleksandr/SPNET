using LabClassLibrary.Models;
using LabWinService.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LabConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите команду");
            Console.WriteLine("Доступые команды : getall, add");

            while(true)
            {
                var entered = Console.ReadLine();
                if (entered == "getall")
                {
                    WriteCustomer();
                }
                else if (entered == "add")
                {
                    Console.WriteLine("Enter name");
                    var name = Console.ReadLine();
                    Console.WriteLine("Enter address");
                    var address = Console.ReadLine();

                    Customer Customer = new Customer{Name=name,Address=address};
                    CreateCustomer(Customer).Wait();
                    Console.WriteLine("Customer added!");
                    WriteCustomer();
                }
                else
                {
                    Console.WriteLine("Such command not exists");
                }
            }
        }


        private static IEnumerable<Customer> GetData()
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync("http://localhost:5555/Customers");

            var jsonData = response.Result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonData);
        }
        private static void WriteCustomer()
        {
            var data = GetData();
            foreach (Customer emp in data)
            {
                Console.WriteLine("Customer Name: {0}, Customer Address: {1}\n", emp.Name, emp.Address);
            }
        }
    private static async Task CreateCustomer(Customer Customer)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(Customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(
                "http://localhost:5555/Customers/add", content);

            response.EnsureSuccessStatusCode();
        }

        private static void SendData(Customer Customer)
        {
           // var tcpClient = new TcpClientHelper<Customer>("127.0.0.1", 8889);
            //var client = new TcpClient();
            //client.Connect("127.0.0.1", 8888);
            //TcpClientHelper<Customer>.SendData(client, Customer);

        }
    }
}
