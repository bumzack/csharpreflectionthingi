using DaoThingi.Reflection;
using System;

namespace DaoThingi.TestDI
{
    [Injectable]
    public class FileStorage : IStorage
    {
        public FileStorage()
        {
            Console.WriteLine("Construcot filestorage");
        }

        public bool StoreData()
        {
            Console.WriteLine("Hello from the Filestore");
            return true;
        }
    }
}
