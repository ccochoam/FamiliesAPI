using FamiliesAPI.Data.Common;
using FamiliesAPI.Data.Interfaces;
using System.Data;
using Dapper;
using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IDbConnection _dbConnection;

        public LoggingRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnection = dbConnectionFactory.CreateConnection();
        }
        public async Task Save(LoggerModel loggerModel)
        {
            var modelDB = QueryModels.LogModelDB(loggerModel);
            await _dbConnection.QueryAsync("LogTraceabilityDB", modelDB, commandType: CommandType.StoredProcedure);
        }
    }
}
