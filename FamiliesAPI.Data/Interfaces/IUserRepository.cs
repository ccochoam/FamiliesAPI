using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> Add(UserModel userModel);
        Task<UserModel> Get(long id);
        Task<List<UserModel>> GetAll();
        Task<UserModel> Update(UserModel userModel);
        Task<int> Delete(long id);
    }
}
