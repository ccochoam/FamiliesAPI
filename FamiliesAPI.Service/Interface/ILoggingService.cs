namespace FamiliesAPI.Services.Interface
{
    public interface ILoggingService
    {
        Task Save(string Action, string Username, string Process, string Request, string Response, bool Successful, string Exception = null);
    }
}
