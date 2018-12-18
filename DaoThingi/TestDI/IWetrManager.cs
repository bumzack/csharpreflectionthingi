using System.Threading.Tasks;

namespace DaoThingi.TestDI
{
    public interface IWetrManager
    {
        Task GetStationsAsync();
        void SayHello();
    }
}
