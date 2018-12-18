using System;

namespace DaoThingi.Reflection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Table : Attribute
    {
        public Table()
        {
            Console.WriteLine("Constructor Attribute Table");
        }
    }
}
