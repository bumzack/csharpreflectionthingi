using DaoThingi.Reflection;

namespace DaoThingi
{
    [Table]
    public class Person
    {
        public string FirstName { get; set;  }
        public string LastName { get; set; }
        public int Id { get; set; } 

        public Person()
        {
            FirstName = "";
            LastName = "";
            Id = -1;
        } 
    }
}
