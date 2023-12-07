using FamiliesAPI.Entities.DTOs;
using FamiliesAPI.Services.Common;

namespace FamiliesAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<ServicesResult<UserDTO>> Authenticate(string userName, string password);
    }
}
