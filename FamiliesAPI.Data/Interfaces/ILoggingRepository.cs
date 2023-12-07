using FamiliesAPI.Entities.Models;

namespace FamiliesAPI.Data.Interfaces
{
    public interface ILoggingRepository
    {
        Task Save(LoggerModel loggerModel);
    }
}
