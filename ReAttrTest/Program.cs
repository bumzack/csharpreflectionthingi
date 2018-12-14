using System;
using System.Reflection;
using static ReAttrTest.Program;

namespace ReAttrTest
{
    class Program
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class Table : System.Attribute
        { 
            private string name; 

            public Table(string Name)
            {
                name = Name;
            }

            public string Name
            {
                get
                {
                    return name;
                }
            }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class Id : System.Attribute
        {
            // private string type; 
            public Id()
            {

            }

            //public string Name
            //{
            //    get
            //    {
            //        return name;
            //    }
            //}
        }
    }

    [Table("Person")]
    public class Person
    {
        [Id]
        public int id { get; set; }
        public string firstName { get; set; }


        public Person(int v1, string v2)
        {
            id = v1;
            firstName = v2;
        }
    }

    public class EntryPoint
    {
        static void Main(string[] args)
        {
            Person p = new Person(1, "Grg");

            Type type = typeof(Person);

            foreach (Object attributes in type.GetCustomAttributes(true))
            {
                Table myobj = (Table)attributes;
                if (null != myobj)
                {
                    Console.WriteLine($"\n Name#:{myobj.Name}  ");
                }
            }



            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine("   {0} ({1})", prop.Name, prop.PropertyType.Name);

                foreach (Attribute a in prop.GetCustomAttributes())
                {
                    Console.WriteLine(" \t\t  type {0}    ",   a.GetType().ToString() ); 
                }
            }



            Console.Read();

        }
    }
}

