using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using static DaoThingi.SqlThingis.AdoTemplate;

namespace DaoThingi
{
    public interface IAdoTemplate
    {
        ICollection<T> Query<T>(string sql, RowMapper<T> rowMapper, SqlParameter[] parameters = null);

        void AddParameters(SqlParameter[] parameters, DbCommand command);

        int Execute(string sql, SqlParameter[] parameters = null);
    }
}