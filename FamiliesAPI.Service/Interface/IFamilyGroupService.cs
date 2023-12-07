using FamiliesAPI.Entities.DTOs;
using FamiliesAPI.Services.Common;

namespace FamiliesAPI.Services.Interface
{
    public interface IFamilyGroupService
    {
        Task<ServicesResult<FamilyGroupDto>> Add(string familyGroupModel);
        Task<ServicesResult<FamilyGroupDto>> Get(int id);
        Task<ServicesResult<List<FamilyGroupDto>>> GetAll();
        Task<ServicesResult<FamilyGroupDto>> Update(int id, FamilyGroupDto familyGroupDto);
        Task<ServicesResult<bool>> Delete(int id);
    }
}
