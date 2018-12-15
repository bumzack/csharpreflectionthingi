using DaoThingi.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoThingi.TestDI
{
    [Injectable]
    public class NetworkStorage : IStorage
    {
        public NetworkStorage()
        {
            Console.WriteLine("Construcot NetworkStorage");
        }

        public bool StoreData()
        {
            Console.WriteLine("Hello from the NetworkStorage");
            return true;
        }
    }
}
