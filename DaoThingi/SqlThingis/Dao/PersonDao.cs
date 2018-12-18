using DaoThingi.DomainObjects;
using DaoThingi.Reflection;
using DaoThingi.SqlThingis;
using System;
using System.Collections.Generic;
using static DaoThingi.SqlThingis.AdoTemplate;

namespace DaoThingi.SqlThingis.Dao
{
    public class PersonDao : SqlDao<Person>
    {
        [Autowire]
        private IAdoTemplate adoTemplate;

        private static readonly RowMapper<Person> personMapper = row => new Person
        {
            FirstName = (string)row["first_name"],
            LastName = (string)row["last_name"],
            Id = (int)row["id"]
        };

        public PersonDao()
        {
            Console.WriteLine("constructor PersonDao");
        }

        ICollection<Person> GetAll()
        {
            string sql = Select();
            Console.WriteLine($"GetAll sql statement: {sql}");

            return adoTemplate.Query<Person>(sql, personMapper);
        }

        //public override bool Insert(Person p)
        //{

        //    return false;
        //}
        public bool InsertPerson(Person item)
        {
            string sql = Insert();
            Console.WriteLine($"InsertPerson sql statement: {sql}");
            adoTemplate.Execute(sql);
            return true;
        }
    }
}


