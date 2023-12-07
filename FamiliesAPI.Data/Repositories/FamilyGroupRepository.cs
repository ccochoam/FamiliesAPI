using System.Data;
using Dapper;
using FamiliesAPI.Data.Interfaces;
using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Repositories
{
    public class FamilyGroupRepository : IFamilyGroupRepository
    {
        private readonly IDbConnection _dbConnection;

        public FamilyGroupRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnection = dbConnectionFactory.CreateConnection();
        }

        public async Task<List<FamilyGroupModel>> GetAll()
        {
            return _dbConnection.Query<FamilyGroupModel>("getAllFamilyGroupsDB", commandType: CommandType.StoredProcedure).ToList();
        }

        public async Task<FamilyGroupModel> GetFamilyGroupById(int familyGroupId)
        {
            return _dbConnection.QueryFirstOrDefault<FamilyGroupModel>("getFamilyGroupDB", new { familyGroupId = familyGroupId }, commandType: CommandType.StoredProcedure);
        }

        public async Task<FamilyGroupModel> Add(string name, string numberId)
        {
            var model = getModelAdd(name, numberId);
            return _dbConnection.QueryFirstOrDefault<FamilyGroupModel>("AddFamilyGroupDB", model, commandType: CommandType.StoredProcedure);
        }

        public async Task<FamilyGroupModel> Update(FamilyGroupModel familyGroupModel)
        {
            var model = getModelAdd(familyGroupModel.Name, familyGroupModel.NumberId, familyGroupModel.FamilyGroupId);
            var id = _dbConnection.QueryFirstOrDefault<FamilyGroupModel>("updateFamilyGroupDB", model, commandType: CommandType.StoredProcedure);

            return id;
        }

        public async Task<int> Delete(int id)
        {
            return _dbConnection.QueryFirstOrDefault<int>("deleteFamilyGroupDB", new { id = id }, commandType: CommandType.StoredProcedure);
        }

        public object getModelAdd(string name, string numberId, long familyGroupId = 0)
        {
            return new
            {
                FamilyGroupId = familyGroupId,
                Name = name,
                NumberId = numberId
            };
        }
    }
}
