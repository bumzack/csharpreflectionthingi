using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DaoThingi
{
    public class Db
    {
        public  DbCommand CreateSelectCommand(object dataObject, DbProviderFactory factory, string TableName) 
        {
            Type t = dataObject.GetType();
            DAOAttribute dao;
            DbCommand cmd = factory.CreateCommand();
            DbParameter param;
            StringCollection Fields = new StringCollection();
            StringBuilder sbWhere = new StringBuilder(" WHERE ");
            bool HasCondition = false; //Indicates that there is a WHERE Condition

            foreach (System.Reflection.MethodInfo mi in t.GetMethods()) //Go thru each method of the object
            {
                foreach (Attribute att in Attribute.GetCustomAttributes(mi))  //Go thru the attributes for the method
                {
                    if (typeof(DAOAttribute).IsAssignableFrom(att.GetType())) //Checks that the Attribute is of the right type
                    {
                        dao = (DAOAttribute)att;
                        Fields.Add(dao.DatabaseColumn); //Append the Fields 

                        if (dao.PrimaryKey)
                        {
                            //Append the Conditions

                            if (HasCondition) sbWhere.Append(" AND ");
                            sbWhere.AppendFormat("{0} = @{0}", dao.DatabaseColumn);
                            param = factory.CreateParameter();
                            param.ParameterName = "@" + dao.DatabaseColumn;
                            if (cmd.Parameters.IndexOf(param.ParameterName) == 0)
                            {
                                param.DbType = (DbType)Enum.Parse(typeof(DbType), dao.ValueType.Name);
                                cmd.Parameters.Add(param);
                                param.Value = mi.Invoke(cmd, null);
                            }
                            HasCondition = true; //Set the HasCondition flag to true
                        }
                    }
                }
            }
            string[] arrField = new string[Fields.Count];
            Fields.CopyTo(arrField, 0);
            cmd.CommandText = "SELECT " + string.Join(",", arrField) + " FROM " + TableName + (HasCondition ? sbWhere.ToString() : " ");
            return cmd;
        }
    }
}
