using System;

namespace DaoThingi.Reflection
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Autowire : Attribute
    {
        private string name; 

        // private string type; 
        public Autowire(string n)
        {
            Console.WriteLine("Constructor Attrbute Autowire");
            this.Name = n;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value; 
            }
        }
    }
}
