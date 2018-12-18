using System;
using System.Collections.Generic;
using DaoThingi.Database;
using DaoThingi.DependencyInjection;
using DaoThingi.SqlThingis.Dao;
using DaoThingi.TestDI;
 
namespace DaoThingi.DomainObjects
{
    class Program
    {
        static void Main(string[] args)
        { 
            List<string> namespaces = new List<string>();
            namespaces.Add("DaoThingi.DomainObjects");
            namespaces.Add("DaoThingi.TestDI");

            GrgContext grgContext = new GrgContext();
            grgContext.AddBean("DaoThingi.SqlThingis.Implementation.Logger", "myLogger", GrgScope.Singleton);
            grgContext.AddBean("DaoThingi.SqlThingis.Implementation.AdoTemplate", "myAdoTemplate", GrgScope.Singleton);
            grgContext.AddBean("DaoThingi.SqlThingis.Implementation.DefaultConnectionFactory", "myDefaultConnectionFactory", GrgScope.Singleton);
            grgContext.AddBean("DaoThingi.DomainObjects.Person", "myPerson", GrgScope.Prototype);
            grgContext.AddBean("DaoThingi.SqlThingis.Dao.PersonDao", "myPersonDao", GrgScope.Singleton);

          

            Console.WriteLine("\n\n\n\nCOntext Data");
            grgContext.ListBeans();
            grgContext.ListInjectables();
            grgContext.ListAutowire();


            PersonDao personDao = (PersonDao) grgContext.GetBeanById("myPersonDao");


        }


        static void Main1(string[] args)
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
            Console.WriteLine("\n\n\n\nCOntext Data");
            grgContext.ListBeans();
            grgContext.ListInjectables();
            grgContext.ListAutowire();

            try
            {
                Car c = new Car("BMW", 2000);
                c.Store();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"NUllreference Exception while calling c.Store() which was expected!  continuing {e.Message}");
            }

            Console.WriteLine($"\n\n\n-------------------------------------------------------------------------------------------------");

            Console.WriteLine($"Get a car object with DI of a an IStorage implementation");

            Car c1 = (Car)grgContext.GetBean("Car");

            c1.Name = "Mercedes";
            c1.HorsePower = 12;
            Console.WriteLine("car c1 = " + c1.ToString());

            // Console.WriteLine($"calling c1.Store() -> this should work, because we've injected an object :-)");
            c1.Store();
            Console.WriteLine($"success c1.Store() - yeah!!! :-)");
            Console.WriteLine($"-------------------------------------------------------------------------------------------------\n\n\n");

            Console.ReadKey();
        }
    }
}
