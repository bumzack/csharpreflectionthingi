using DaoThingi.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoThingi.TestDI
{
    public class Car
    {
        // here is the magic ...
        [Autowire( "FileStorage")]
        public IStorage storage;
        
        public string Name { get; set; }
        public int HorsePower { get; set; }

        public Car() { }

        public Car(string n, int hp)
        {
            Name = n;
            HorsePower = hp;
        }

        public void Store()
        {
            Console.WriteLine("storing the car using the IStorage interface with the injected object");
            storage.StoreData();
        }

        public override string ToString()
        {
            return "Car:  Name: " + Name + ", hp: " + HorsePower;
        }
    }
}
