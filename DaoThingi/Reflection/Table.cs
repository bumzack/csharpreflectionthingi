using System;

namespace DaoThingi.Reflection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Table : Attribute
    {
        private string name; 

        public Table()
        {
            Console.WriteLine("Constructor Attribute Table");
        } 
    }
}
