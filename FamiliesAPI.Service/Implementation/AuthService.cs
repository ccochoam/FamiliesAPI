using FamiliesAPI.Services.Common;
using FamiliesAPI.Services.Interface;
using FamiliesAPI.Entities.DTOs;
using FamiliesAPI.Data.Interfaces;
using AutoMapper;
using FamiliesAPI.Services.Security;

namespace FamiliesAPI.Services.Implementation
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<ServicesResult<UserDTO>> Authenticate(string userName, string password)
        {
            try
            {
                var res = await _authRepository.Authenticate(userName);
                if (res == null)
                    return ServicesResult<UserDTO>.FailedOperation(404, "User not found");

                bool valitePass = ValidatePass.ValidatePassword(password, res.Password, res.HashKey);
                if (valitePass)
                {
                    UserDTO userDTO = _mapper.Map<UserDTO>(res);
                    return ServicesResult<UserDTO>.SuccessfulOperation(userDTO);
                }
                return ServicesResult<UserDTO>.FailedOperation(401, "unauthorized");
            }
            catch (Exception ex)
            {
                return ServicesResult<UserDTO>.FailedOperation(500, "Error in Authenticate", ex);
            }
        }
    }
}
