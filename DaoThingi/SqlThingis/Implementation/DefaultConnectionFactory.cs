using DaoThingi.Reflection;
using DaoThingi.SqlThingis.Interface;
using System.Configuration;
using System.Data.Common;
using System.Threading.Tasks;

namespace DaoThingi.SqlThingis.Implementation
{
    [Injectable]
    class DefaultConnectionFactory : IConnectionFactory
    {
        private DbProviderFactory dbProviderFactory;

        public DefaultConnectionFactory(string pn, string cs, DbProviderFactory f)
        {
            ConnectionString = cs;
            ProviderName = pn;
            dbProviderFactory = f;
        }

        public string ConnectionString { get; }

        public string ProviderName { get; }

        public DbConnection CreateConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }

        public async Task<DbConnection> CreateConnectionAsync()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            await connection.OpenAsync();
            return connection;
        }
    }
}
