using AutoMapper;
using FamiliesAPI.Data.Interfaces;
using FamiliesAPI.Entities.DTOs;
using FamiliesAPI.Entities.Models;
using FamiliesAPI.Services.Common;
using FamiliesAPI.Services.Interface;

namespace FamiliesAPI.Services.Implementation
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _logginRespository;
        private readonly IMapper _mapper;

        public LoggingService(ILoggingRepository loggingRepository, IMapper mapper)
        {
            _logginRespository = loggingRepository;
            _mapper = mapper;
        }

        public async Task Save(string Action, string Username, string Process, string Request, string Response, bool Successful, string Exception = null)
        {
            try
            {
                var loggerModel = GetModel(Action, Username, Process, Request, Response, Successful, Exception);
                await _logginRespository.Save(loggerModel);
            }
            catch (Exception ex)
            {
                var a = ex;
            }
        }

        private LoggerModel GetModel(string Action, string Username, string Process, string Request, string Response, bool Successful, string Exception)
        {
            return new LoggerModel
            {
                Action = Action,
                Username = Username,
                Process = Process,
                Response = Response,
                Request = Request,
                Successful = Successful,
                Exception = Exception
            };
        }
    }
}
