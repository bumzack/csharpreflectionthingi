using DaoThingi.Reflection;
using DaoThingi.SqlThingis.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DaoThingi.SqlThingis
{
    [Injectable]
    public class AdoTemplate : IAdoTemplate
    {
        public delegate T RowMapper<T>(IDataRecord row);

        [Autowire]
        private readonly IConnectionFactory ConnectionFactory;

        [Autowire]
        private ILogger Logger;

        public ICollection<T> Query<T>(string sql, RowMapper<T> rowMapper, SqlParameter[] parameters = null)
        {
            try
            {
                using (DbConnection connection = ConnectionFactory.CreateConnection())
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;

                    if (parameters != null)
                    {
                        // Logger.Info("adding parameters " );

                        AddParameters(parameters, command);
                    }

                    var items = new List<T>();

                    try
                    {
                        // Daten auslesen aus dem Reader
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            int i = 0;
                            while (reader.Read())
                            {
                                //Logger.Info("Reader line " +i+"  : " + reader);
                                i++;
                                items.Add(
                                    rowMapper(reader)
                                );
                            }
                            Logger.Info("Query '" + sql + "' successfull\t\t results: " + items);
                            //Logger.Error("Query sleeping for 1 seconds");
                            //Thread.Sleep(1000);
                            return items;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("error executing an query in AdoTemplate.Query()\t SQL = '" + sql + "'   \t   Error message:  " + ex.Message);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"exception in AdoTemplate.Query:  sql =  {sql}, exception = {ex.Message}");
            }
            return null;
        }

        public void AddParameters(SqlParameter[] parameters, DbCommand command)
        {
            throw new System.NotImplementedException();
        }

        public int Execute(string sql, SqlParameter[] parameters = null)
        {
            throw new System.NotImplementedException();
        }


        //public class AdoTemplate : IAdoTemplate
        //{
        //    private readonly IConnectionFactory ConnectionFactory;
        //    private ILogger Logger;

        //    public AdoTemplate(IConnectionFactory connectionFactory, ILogger logger)
        //    {
        //        ConnectionFactory = connectionFactory;
        //        Logger = logger;
        //    }

        //    public ICollection<T> Query<T>(String sql, RowMapper<T> rowMapper, WetrSqlParameter[] parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = ConnectionFactory.CreateConnection())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    // Logger.Info("adding parameters " );

        //                    AddParameters(parameters, command);
        //                }

        //                var items = new List<T>();

        //                try
        //                {
        //                    // Daten auslesen aus dem Reader
        //                    using (DbDataReader reader = command.ExecuteReader())
        //                    {
        //                        int i = 0;
        //                        while (reader.Read())
        //                        {
        //                            //Logger.Info("Reader line " +i+"  : " + reader);
        //                            i++;
        //                            items.Add(
        //                                rowMapper(reader)
        //                            );
        //                        }
        //                        Logger.Info("Query '" + sql + "' successfull\t\t results: " + items);
        //                        //Logger.Error("Query sleeping for 1 seconds");
        //                        //Thread.Sleep(1000);
        //                        return items;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    Logger.Error("error executing an query in AdoTemplate.Query()\t SQL = '" + sql + "'   \t   Error message:  " + ex.Message);
        //                }
        //                return null;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.Query:  sql =  {sql}, exception = {ex.Message}");
        //        }
        //        return null;
        //    }

        //    public void AddParameters(WetrSqlParameter[] parameters, DbCommand command)
        //    {
        //        foreach (var p in parameters)
        //        {
        //            DbParameter dbParam = command.CreateParameter();
        //            // Logger.Info("AddParameters " + p.Name + "  : " + p.Value);

        //            dbParam.ParameterName = p.Name;
        //            dbParam.Value = p.Value;
        //            command.Parameters.Add(dbParam);
        //        }
        //    }

        //    public int Execute(String sql, WetrSqlParameter[] parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = ConnectionFactory.CreateConnection())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    AddParameters(parameters, command);
        //                }

        //                try
        //                {
        //                    int res = command.ExecuteNonQuery();
        //                    Logger.Info("Query '" + sql + "' succesfull \t\t result: " + res);
        //                    return res;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Logger.Error("error executing an query in AdoTemplate.Execute(..)  sql = '" + sql + "' - Error message:  " + ex.Message);
        //                    return -1;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.Execute:  sql =  {sql}, exception = {ex.Message}");
        //        }
        //        return -1;
        //    }

        //    public int ExecuteReturnId(String sql, WetrSqlParameter[] parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = ConnectionFactory.CreateConnection())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    AddParameters(parameters, command);
        //                }

        //                try
        //                {
        //                    int id = (int)command.ExecuteScalar();
        //                    Logger.Info("Query '" + sql + "' succesfull \t\t inserted id : " + id);
        //                    return id;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Logger.Error("error executing an query in AdoTemplate.Execute(..)  sql = '" + sql + "' - Error message:  " + ex.Message);
        //                    return -1;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.Execute:  sql =  {sql}, exception = {ex.Message}");
        //        }
        //        return -1;
        //    }



        //    public int ExecuteMultipleInsert(String sql, IList<WetrSqlParameter[]> parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = ConnectionFactory.CreateConnection())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    //String s = "query params: ";
        //                    //foreach (var pArr in parameters)
        //                    //{
        //                    //    s += pArr.ToString() + " // ";
        //                    //}
        //                    //Logger.Info(s);

        //                    foreach (var pArr in parameters)
        //                    {
        //                        AddParameters(pArr, command);
        //                    }
        //                }

        //                try
        //                {
        //                    int res = command.ExecuteNonQuery();

        //                    Logger.Info("Query ExecuteMultipleInsert '" + sql + "' succesfull \t\t result: " + res);
        //                    return res;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Logger.Error("error executing an query in AdoTemplate.ExecuteMultipleInsert(..)  sql = '" + sql + "' - \nError message:  " + ex.Message);
        //                    return -1;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.ExecuteMultipleInsert:  sql =  {sql}, \nexception = {ex.Message}");
        //        }
        //        return -1;
        //    }

        //    public async Task<IEnumerable<T>> QueryAsync<T>(String sql, RowMapper<T> rowMapper, WetrSqlParameter[] parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = await ConnectionFactory.CreateConnectionAsync())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    AddParameters(parameters, command);
        //                }

        //                var items = new List<T>();

        //                // Daten auslesen aus dem Reader
        //                using (DbDataReader reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        items.Add(
        //                            rowMapper(reader)
        //                        );
        //                    }
        //                    return items;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.QueryAsync:  sql =  {sql}, \nexception = {ex.Message}");
        //        }
        //        return null;
        //    }

        //    public async Task<T> QuerySingleAsync<T>(String sql, RowMapper<T> rowMapper, WetrSqlParameter[] parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = await ConnectionFactory.CreateConnectionAsync())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    AddParameters(parameters, command);
        //                }
        //                using (DbDataReader reader = await command.ExecuteReaderAsync())
        //                {
        //                    await reader.ReadAsync();
        //                    if (reader.HasRows)
        //                    {
        //                        return rowMapper(reader);
        //                    }
        //                    return default(T);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.QuerySingleAsync:  sql =  {sql}, \nexception = {ex.Message}");
        //        }
        //        return default(T);
        //    }

        //    public async Task<int> ExecuteAsync(string sql, WetrSqlParameter[] sqlParameter)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = await ConnectionFactory.CreateConnectionAsync())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (sqlParameter != null)
        //                {
        //                    AddParameters(sqlParameter, command);
        //                }
        //                //Logger.Error("ExecuteAsync() -  sleeping for 4 seconds");
        //                //Thread.Sleep(4000);

        //                return await command.ExecuteNonQueryAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.QuerySingleAsync:  sql =  {sql}, \nexception = {ex.Message}");
        //        }
        //        return default(int);
        //    }

        //    public async Task<int> ExecuteReturnScalarAsync(String sql, WetrSqlParameter[] parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = await ConnectionFactory.CreateConnectionAsync())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    AddParameters(parameters, command);
        //                }

        //                try
        //                {
        //                    int id = (int)await command.ExecuteScalarAsync();
        //                    Logger.Info("Query '" + sql + "' succesfull \t\t inserted id : " + id);
        //                    return id;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Logger.Error("error executing an query in AdoTemplate.Execute(..)  sql = '" + sql + "' - Error message:  " + ex.Message);
        //                    return -1;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.Execute:  sql =  {sql}, exception = {ex.Message}");
        //        }
        //        return -1;
        //    }


        //    public async Task<int> ExecuteMultipleInsertAsync(String sql, IList<WetrSqlParameter[]> parameters = null)
        //    {
        //        try
        //        {
        //            using (DbConnection connection = await ConnectionFactory.CreateConnectionAsync())
        //            using (DbCommand command = connection.CreateCommand())
        //            {
        //                command.CommandText = sql;

        //                if (parameters != null)
        //                {
        //                    foreach (var pArr in parameters)
        //                    {
        //                        AddParameters(pArr, command);
        //                    }
        //                }

        //                try
        //                {
        //                    int res = await command.ExecuteNonQueryAsync();
        //                    Logger.Info("Query ExecuteMultipleInsertAsync '" + sql + "' succesfull \t\t result: " + res);
        //                    return res;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Logger.Error("error executing an query in AdoTemplate.ExecuteMultipleInsertAsync(..)  sql = '" + sql + "' - Error message:  " + ex.Message);
        //                    return -1;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error($"exception in AdoTemplate.ExecuteMultipleInsertAsync:  sql =  {sql}, \nexception = {ex.Message}");
        //        }
        //        return -1;
        //    }
        //}

    }
}
