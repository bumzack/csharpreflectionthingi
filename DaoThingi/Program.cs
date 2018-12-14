using System;
using System.Data.Common;
using System.Configuration;
namespace DaoThingi
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            customer.CustomerFirstName = "grg";
            customer.CustomerLastName = "es";
            customer.CustomerIDNumber = "1";


            string s = ConfigurationManager.ConnectionStrings["WetrDbConnection"].ProviderName;

            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(s);

             
 
            Db d = new Db();
            DbCommand c = d.CreateSelectCommand(customer, dbProviderFactory, "table");

            Console.WriteLine(" c.CommandText   " + c.CommandText);

            Console.ReadKey();
        }
    }
}
