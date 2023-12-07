using AutoMapper;
using FamiliesAPI.Data.Interfaces;
using FamiliesAPI.Entities.DTOs;
using FamiliesAPI.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using FamiliesAPI.Services.Common;
using FamiliesAPI.Services.Interface;
using FamiliesAPI.Services.Security;
using System.Globalization;

namespace FamiliesAPI.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServicesResult<UserDTO>> Add(UserDTO userDTO)
        {
            try
            {
                var model = _mapper.Map<UserModel>(userDTO);
                model.HashKey = SaltGenerator.GenerateSalt();
                model.Password = PasswordHasher.EncryptKey(userDTO.GetPassword(), model.HashKey);
                var res = await _userRepository.Add(model);
                if (res.UserId == 0)
                    return ServicesResult<UserDTO>.FailedOperation(409, "User already exist");
                var modelRes = _mapper.Map<UserDTO>(res);
                return ServicesResult<UserDTO>.SuccessfulOperation(modelRes);
            }
            catch (Exception ex)
            {
                return ServicesResult<UserDTO>.FailedOperation(500, "Internal Server Error", ex);
            }
        }

        public async Task<ServicesResult<UserDTO>> Get(long id)
        {
            try
            {
                var res = await _userRepository.Get(id);
                if (res.UserId == 0)
                    return ServicesResult<UserDTO>.FailedOperation(404, "User not found");
                var resDTO = _mapper.Map<UserDTO>(res);
                return ServicesResult<UserDTO>.SuccessfulOperation(resDTO);
            }
            catch (Exception ex)
            {
                return ServicesResult<UserDTO>.FailedOperation(500, "Internal Server Error", ex);
            }
        }

        public async Task<ServicesResult<List<UserDTO>>> GetAll()
        {
            try
            {
                var res = await _userRepository.GetAll();
                var resDTO = _mapper.Map<List<UserDTO>>(res);
                if (resDTO != null)
                    return ServicesResult<List<UserDTO>>.SuccessfulOperation(resDTO);
                return ServicesResult<List<UserDTO>>.FailedOperation(404, "Users not found");

            }
            catch (Exception ex)
            {
                return ServicesResult<List<UserDTO>>.FailedOperation(500, "Internal Server Error", ex);
            }
        }

        public async Task<ServicesResult<UserDTO>> Update(long id, UserDTO userDTO)
        {
            try
            {
                var existingUser = await _userRepository.Get(id);

                if (existingUser == null)
                    return ServicesResult<UserDTO>.FailedOperation(404, $"User with Id: {id}, not found");

                existingUser = GetUserModel(userDTO, existingUser);
                await _userRepository.Update(existingUser);
                var modelDTO = _mapper.Map<UserDTO>(existingUser);
                if (modelDTO != null)
                    return ServicesResult<UserDTO>.SuccessfulOperation(modelDTO);
                return ServicesResult<UserDTO>.FailedOperation(400, "User was not updated");
            }
            catch (Exception ex)
            {
                return ServicesResult<UserDTO>.FailedOperation(500, "Error in update: UserService", ex);
            }
        }
        public async Task<ServicesResult<bool>> Delete(long id)
        {
            try
            {
                var res = await _userRepository.Delete(id);
                if (res > 0)
                    return ServicesResult<bool>.SuccessfulOperation(true);
                return ServicesResult<bool>.FailedOperation(404, "User was not delete");
            }
            catch (Exception ex)
            {
                return ServicesResult<bool>.FailedOperation(500, "Error in Delete: UserService", ex);
            }
        }

        private UserModel GetUserModel(UserDTO userDTO, UserModel existingUser)
        {

            if (!string.IsNullOrEmpty(userDTO.Username) && userDTO.Username != existingUser.Username)
                existingUser.Username = userDTO.Username;
            if (!string.IsNullOrEmpty(userDTO.GetPassword()) && userDTO.GetPassword() != existingUser.Password)
                existingUser.Password = userDTO.GetPassword();
            if (!string.IsNullOrEmpty(userDTO.Name) && userDTO.Name != existingUser.Name)
                existingUser.Name = userDTO.Name;
            if (!string.IsNullOrEmpty(userDTO.LastName) && userDTO.LastName != existingUser.LastName)
                existingUser.LastName = userDTO.LastName;
            if (!string.IsNullOrEmpty(userDTO.NumberId) && userDTO.NumberId != existingUser.NumberId)
                existingUser.NumberId = userDTO.NumberId;
            if (userDTO.GenderId > 0 && userDTO.GenderId != existingUser.GenderId)
                existingUser.GenderId = userDTO.GenderId;
            if (!string.IsNullOrEmpty(userDTO.Relationship) && userDTO.Relationship != existingUser.Relationship)
                existingUser.Relationship = userDTO.Relationship;
            if (userDTO.Age > 0 && userDTO.Age != existingUser.Age)
                existingUser.Age = userDTO.Age;
            if (userDTO.UnderAge != null && userDTO.UnderAge != existingUser.UnderAge)
                existingUser.UnderAge = userDTO.UnderAge;
            if (userDTO.Birthdate != null && userDTO.Birthdate != DateOnly.ParseExact(existingUser.Birthdate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None))
                existingUser.Birthdate = userDTO.Birthdate.ToString("dd/MM/yyyy");
            if (userDTO.FamilyGroupId > 0 && userDTO.FamilyGroupId != existingUser.FamilyGroupId)
                existingUser.FamilyGroupId = userDTO.FamilyGroupId;

            return existingUser;
        }
    }
}
