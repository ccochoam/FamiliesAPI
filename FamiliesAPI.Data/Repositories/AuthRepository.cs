using FamiliesAPI.Data.Common;
using FamiliesAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _dbConnection;

        public AuthRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnection = dbConnectionFactory.CreateConnection();
        }

        public async Task<UserModel> Authenticate(string username)
        {
            var model = QueryModels.GetUserQuery(username, "byUsername");
            return _dbConnection.QueryFirstOrDefault<UserModel>("getUserDB", model, commandType: CommandType.StoredProcedure);
        }
    }
}
