using System;
using System.Data.Common;
using System.Configuration;
using DaoThingi.Database;

namespace DaoThingi
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();

            p.FirstName = "georg";
            p.LastName = "schinnerl";
            p.Id = 123;

            SqlStatements sql = new SqlStatements();
            string s = sql.Select<Person>(p);
            Console.WriteLine("sql select for person : " + s);

            Location l = new Location(34.12, 45.5, "WIen", "Niederösterreich");
            string s1 = sql.Select<Location>(l);
            Console.WriteLine("sql select for Location : " + s1);

            Console.ReadKey();


            //string s = ConfigurationManager.ConnectionStrings["WetrDbConnection"].ProviderName;

            //DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(s);


            //Db d = new Db();
            //DbCommand c = d.CreateSelectCommand(customer, dbProviderFactory, "table");

            //Console.WriteLine(" c.CommandText   " + c.CommandText);

            Console.ReadKey();
        }
    }
}
