using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoThingi.Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class Id : System.Attribute
    {
        private string name; 

        // private string type; 
        public Id()
        {
            Console.WriteLine("Constructor Attrbute Id");
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
