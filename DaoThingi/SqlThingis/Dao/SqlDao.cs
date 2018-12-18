using DaoThingi.Database;
using DaoThingi.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DaoThingi.SqlThingis.Dao
{
    public class SqlDao<T>
    {
        protected string Insert()
        {
            Type t = typeof(T);
           // Object obj = null;

            Console.WriteLine("type of t = " + t.GetType().ToString() + ", name = " + t.Name);
            List<string> props = new List<string>();
            List<object> values = new List<object>();

            foreach (Attribute att in t.GetCustomAttributes(true))
            {
                if (att.GetType() == typeof(Table))
                {

                    //Console.WriteLine("yeah - it is a table");
                    //Console.WriteLine("the table " + t.Name + " has the following properties:");

                    foreach (PropertyInfo p in t.GetProperties())
                    {
                        //Console.WriteLine("\t property name: " + p.Name + "m  propertyType() " + p.PropertyType.ToString());
                        props.Add(p.Name); 
                    }
                }
                else
                {
                    throw new NotATableException(t.Name + " is does not have  a 'Table' Attribute");
                }
            }

            string sql = "INSERT INTO  " + t.Name + "(" + string.Join(",", props) + ")" + " VALUES ";
            sql += "(" + string.Join(",", values) + ")";


            return sql;
        }


        public string Select  ( )
        {
            Type t = typeof(T);

            List<string> props = new List<string>();

            foreach (Attribute att in t.GetCustomAttributes(true))
            {
                if (att.GetType() == typeof(Table))
                {
                    //Console.WriteLine("yeah - it is a table");
                    //Console.WriteLine("the table " + t.Name + " has the following properties:");

                    foreach (PropertyInfo p in t.GetProperties())
                    {
                        //Console.WriteLine("\t property name: " + p.Name + "m  propertyType() " + p.PropertyType.ToString());
                        props.Add(p.Name);
                    }
                }
                else
                {
                    throw new NotATableException(t.Name + " is does not have  a 'Table' Attribute");
                }
            }

            string sql = "SELECT " + string.Join(",", props) + " FROM " + t.Name;
            return sql;
        } 
    }
}
