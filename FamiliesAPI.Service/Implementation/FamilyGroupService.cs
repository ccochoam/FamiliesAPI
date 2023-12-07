using FamiliesAPI.Services.Interface;
using FamiliesAPI.Data.Interfaces;
using FamiliesAPI.Entities.Models;
using FamiliesAPI.Entities.DTOs;
using AutoMapper;
using FamiliesAPI.Services.Common;

namespace FamiliesAPI.Services.Implementation
{
    public class FamilyGroupService : IFamilyGroupService
    {
        private readonly IFamilyGroupRepository _familyGroupRepository;
        private readonly IMapper _mapper;

        public FamilyGroupService(IFamilyGroupRepository familyGroupRepository, IMapper mapper)
        {
            _familyGroupRepository = familyGroupRepository;
            _mapper = mapper;
        }

        public async Task<ServicesResult<FamilyGroupDto>> Add(string name)
        {
            try
            {
                string numberId = GetNumerId(name);
                var res = await _familyGroupRepository.Add(name, numberId);
                var familyGroupDto = _mapper.Map<FamilyGroupDto>(res);
                if (familyGroupDto != null)
                    return ServicesResult<FamilyGroupDto>.SuccessfulOperation(familyGroupDto);
                return ServicesResult<FamilyGroupDto>.FailedOperation(400, "User was not added");
            }
            catch (Exception ex)
            {
                return ServicesResult<FamilyGroupDto>.FailedOperation(500, "Error in Update: FamilyGroupService", ex);
            }
        }

        public async Task<ServicesResult<List<FamilyGroupDto>>> GetAll()
        {
            try
            {
                var res = await _familyGroupRepository.GetAll();
                var familyGroupDtoList = _mapper.Map<List<FamilyGroupDto>>(res);
                if (familyGroupDtoList != null)
                    return ServicesResult<List<FamilyGroupDto>>.SuccessfulOperation(familyGroupDtoList);
                return ServicesResult<List<FamilyGroupDto>>.FailedOperation(404, "Users not found");
            }
            catch (Exception ex)
            {
                return ServicesResult<List<FamilyGroupDto>>.FailedOperation(500, "Error in GetAll: FamilyGroupService", ex);
            }
        }

        public async Task<ServicesResult<FamilyGroupDto>> Get(int familyGroupId)
        {
            try
            {
                var res = await _familyGroupRepository.GetFamilyGroupById(familyGroupId);
                var familyGroupDto = _mapper.Map<FamilyGroupDto>(res);
                if (familyGroupDto != null)
                    return ServicesResult<FamilyGroupDto>.SuccessfulOperation(familyGroupDto);
                return ServicesResult<FamilyGroupDto>.FailedOperation(404, "User not found");
            }
            catch (Exception ex)
            {
                return ServicesResult<FamilyGroupDto>.FailedOperation(500, "Error in Get: FamilyGroupService", ex);
            }
        }

        public async Task<ServicesResult<bool>> Delete(int id)
        {
            try
            {
                var res = await _familyGroupRepository.Delete(id);
                if (res > 0)
                    return ServicesResult<bool>.SuccessfulOperation(true);
                return ServicesResult<bool>.FailedOperation(400, "User was not delete");
            }
            catch (Exception ex)
            {
                return ServicesResult<bool>.FailedOperation(500, "Error in Update: FamilyGroupService", ex);
            }
        }

        public async Task<ServicesResult<FamilyGroupDto>> Update(int id, FamilyGroupDto familyGroupDTO)
        {
            try
            {
                var familyGroupModel = await _familyGroupRepository.GetFamilyGroupById(id);

                if (familyGroupModel == null)
                    return ServicesResult<FamilyGroupDto>.FailedOperation(404, $"FamilyGroup with Id: {id}, not found");

                familyGroupModel = GetFamilyGroupModel(familyGroupDTO, familyGroupModel);
                await _familyGroupRepository.Update(familyGroupModel);
                var modelDTO = _mapper.Map<FamilyGroupDto>(familyGroupModel);
                if (modelDTO == null)
                    return ServicesResult<FamilyGroupDto>.FailedOperation(400, "FamilyGroup was not updated");
                return ServicesResult<FamilyGroupDto>.SuccessfulOperation(modelDTO);
            }
            catch (Exception ex)
            {
                return ServicesResult<FamilyGroupDto>.FailedOperation(500, "Error in Update: FamilyGroupService", ex.InnerException);
            }
        }
        private FamilyGroupModel GetFamilyGroupModel(FamilyGroupDto familyGroupDto, FamilyGroupModel familyGroupModel)
        {

            if (!string.IsNullOrEmpty(familyGroupDto.Name) && familyGroupDto.Name != familyGroupModel.Name)
                familyGroupModel.Name = familyGroupDto.Name;


            return familyGroupModel;
        }

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static string GetTimestamp()
        {
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            return now.ToUnixTimeSeconds().ToString();
        }

        private static string GetNumerId(string name)
        {
            return name.Substring(0, 2).ToUpper() + GetTimestamp();
        }
    }
}
