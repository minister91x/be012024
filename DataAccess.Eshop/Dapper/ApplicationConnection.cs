using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Eshop.Dapper
{
    public class ApplicationConnection : IApplicationDbConnection, IDisposable

    {
        private readonly IDbConnection connection;

        public ApplicationConnection(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("ConnStr"));

        }

        public IDbConnection GetConnection => this.connection;

        public void Dispose()
        {
            connection?.Dispose();
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.ExecuteAsync(sql, param, transaction, 60, commandType);
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await connection.QueryAsync<T>(sql, param, transaction, commandTimeout: 600, commandType: commandType))?.AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}