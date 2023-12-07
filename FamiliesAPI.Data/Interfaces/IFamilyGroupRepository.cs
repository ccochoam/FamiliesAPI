using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Interfaces
{
    public interface IFamilyGroupRepository
    {
        Task<List<FamilyGroupModel>> GetAll();
        Task<FamilyGroupModel> GetFamilyGroupById(int familyGroupId);
        Task<FamilyGroupModel> Add(string name, string numberId);
        Task<FamilyGroupModel> Update(FamilyGroupModel familyGroupModel);
        Task<int> Delete(int familyGroupId);
    }
}
