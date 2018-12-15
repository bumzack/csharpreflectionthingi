using DaoThingi.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
