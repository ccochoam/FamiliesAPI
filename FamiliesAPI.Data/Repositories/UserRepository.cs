using FamiliesAPI.Data.Common;
using FamiliesAPI.Data.Interfaces;
using System.Data;
using Dapper;
using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnection = dbConnectionFactory.CreateConnection();
        }

        public async Task<UserModel> Add(UserModel userModel)
        {
            var modelDB = QueryModels.UserModelDB(userModel);
            return _dbConnection.QueryFirstOrDefault<UserModel>("addUserDB", modelDB, commandType: CommandType.StoredProcedure);
        }

        public async Task<UserModel> Get(long id)
        {
            var model = QueryModels.GetUserQuery(id.ToString(), "byId");
            return _dbConnection.QueryFirstOrDefault<UserModel>("getUserDB", model, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<UserModel>> GetAll()
        {
            return _dbConnection.Query<UserModel>("getAllUsersDB", commandType: CommandType.StoredProcedure).ToList();
        }

        public async Task<UserModel> Update(UserModel userModel)
        {
            var modelDB = QueryModels.UserModelDB(userModel);
            return _dbConnection.QueryFirstOrDefault<UserModel>("updateUserDB", modelDB, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> Delete(long id)
        {
            return _dbConnection.QueryFirstOrDefault<int>("deleteUserDB", new { id = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
