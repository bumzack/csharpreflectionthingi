using DaoThingi.Reflection;
using System;
using System.Threading.Tasks;

namespace DaoThingi.TestDI
{
    [Injectable]
    public class WetrManager : IWetrManager
    {
        public async Task GetStationsAsync()
        {
            throw new NotImplementedException();
        }

        public void SayHello()
        {
            Console.WriteLine($"hello from the wetrmaanager - my address is {this}");
        }
    }
}
