using System;
using System.Collections.Generic;
using DaoThingi.Database;
using DaoThingi.DependencyInjection;
using DaoThingi.TestDI;

namespace DaoThingi.DomainObjects
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
            string s = sql.Select(p);
            Console.WriteLine("sql select for person : " + s);

            Location l = new Location(34.12, 45.5, "WIen", "Niederösterreich");
            string s1 = sql.Select(l);
            Console.WriteLine("sql select for Location : " + s1);

            Console.WriteLine("sql insert for Location : " + sql.Insert(l));


            List<string> namespaces = new List<string>();
            namespaces.Add("DaoThingi.DomainObjects");
            namespaces.Add("DaoThingi.TestDI");

            GrgContext grgContext = new GrgContext(namespaces);

            grgContext.ListClasses();
            grgContext.ListInterfaces();
            grgContext.ListInjectables();

            Car c1 = (Car)grgContext.GetBean("Car");

            c1.Name = "Mercedes";
            c1.HorsePower = 12;

            Console.WriteLine("car c1 = " + c1.ToString());


            Car c = new Car("BMW", 2000);
            // c.Store();

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
