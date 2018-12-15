using System;

namespace DaoThingi.Reflection
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Autowire : Attribute
    {
        private string name; 

        // private string type; 
        public Autowire()
        {
            Console.WriteLine("Constructor Attrbute Autowire");
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
