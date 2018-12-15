using System;

namespace DaoThingi.Reflection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Injectable : Attribute
    {
        // private string name;

        // private string type; 
        public Injectable()
        {
            Console.WriteLine("Constructor Attrbute Injectable");
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
