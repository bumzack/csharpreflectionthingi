using DaoThingi.Reflection;
using System;

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
