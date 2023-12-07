using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserModel> Authenticate(string username);
    }
}
