using DaoThingi.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DaoThingi.Database
{
    public class SqlStatements
    {
        public string Select<T>(T obj)
        {
            Type t = obj.GetType();

            //Console.WriteLine("type of t = " + t.GetType().ToString() + ", name = " + t.Name);
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

        public string Insert (object obj)
        {
            Type t = obj.GetType();

            //Console.WriteLine("type of t = " + t.GetType().ToString() + ", name = " + t.Name);
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
                          values.Add(p.GetValue(obj));

                        Console.WriteLine("\t property name: " + p.Name + "m  propertyType() " + p.PropertyType.ToString() +
                            "  value: "+ p.GetValue(obj)); 
                    }
                }
                else
                {
                    throw new NotATableException(t.Name + " is does not have  a 'Table' Attribute");
                }
            }

            string sql = "INSERT INTO  " +t.Name +"("+ string.Join(",", props) +")" + " VALUES ";
            sql += "(" + string.Join(",", values) + ")";

            return sql;
        }


        public string CreateTable(object obj)
        {
            Type t = obj.GetType();

            //Console.WriteLine("type of t = " + t.GetType().ToString() + ", name = " + t.Name);
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
                        values.Add(p.GetValue(obj));

                        Console.WriteLine("\t property name: " + p.Name + "m  propertyType() " + p.PropertyType.ToString() +
                            "  value: " + p.GetValue(obj));
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
    }
}
