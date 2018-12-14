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
             

            //DAOAttribute dao;
            //DbCommand cmd = factory.CreateCommand();
            //DbParameter param;
            //StringCollection Fields = new StringCollection();
            //StringBuilder sbWhere = new StringBuilder(" WHERE ");
            //bool HasCondition = false; //Indicates that there is a WHERE Condition

            //foreach (System.Reflection.MethodInfo mi in t.GetMethods()) //Go thru each method of the object
            //{
            //    foreach (Attribute att in Attribute.GetCustomAttributes(mi))  //Go thru the attributes for the method
            //    {
            //        if (typeof(DAOAttribute).IsAssignableFrom(att.GetType())) //Checks that the Attribute is of the right type
            //        {
            //            dao = (DAOAttribute)att;
            //            Fields.Add(dao.DatabaseColumn); //Append the Fields 

            //            if (dao.PrimaryKey)
            //            {
            //                //Append the Conditions

            //                if (HasCondition) sbWhere.Append(" AND ");
            //                sbWhere.AppendFormat("{0} = @{0}", dao.DatabaseColumn);
            //                param = factory.CreateParameter();
            //                param.ParameterName = "@" + dao.DatabaseColumn;
            //                if (cmd.Parameters.IndexOf(param.ParameterName) == 0)
            //                {
            //                    param.DbType = (DbType)Enum.Parse(typeof(DbType), dao.ValueType.Name);
            //                    cmd.Parameters.Add(param);
            //                    param.Value = mi.Invoke(cmd, null);
            //                }
            //                HasCondition = true; //Set the HasCondition flag to true
            //            }
            //        }
            //    }
            //}
            //string[] arrField = new string[Fields.Count];
            //Fields.CopyTo(arrField, 0);
            //cmd.CommandText = "SELECT " + string.Join(",", arrField) + " FROM " + TableName + (HasCondition ? sbWhere.ToString() : " ");
             
         }
    }
}
