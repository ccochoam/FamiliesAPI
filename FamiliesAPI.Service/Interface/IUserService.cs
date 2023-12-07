using FamiliesAPI.Services.Common;
using FamiliesAPI.Entities.DTOs;

namespace FamiliesAPI.Services.Interface
{
    public interface IUserService
    {
        Task<ServicesResult<UserDTO>> Add(UserDTO userDTO);
        Task<ServicesResult<UserDTO>> Get(long id);
        Task<ServicesResult<List<UserDTO>>> GetAll();
        Task<ServicesResult<UserDTO>> Update(long id, UserDTO userDTO);
        Task<ServicesResult<bool>> Delete(long id);
    }
}
