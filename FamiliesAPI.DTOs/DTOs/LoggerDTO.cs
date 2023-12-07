using System.Runtime.Serialization;

namespace FamiliesAPI.Entities.DTOs
{
    public class LoggerDTO
    {
        [IgnoreDataMember]
        public string Action { get; set; }
        public long LogId { get; set; }
        public DateTime Created { get; set; }
        public string Username { get; set; }
        public string Process { get; set; }
        public string Response { get; set; }
        public string Request { get; set; }
        public bool Successful { get; set; }
        public string Exception { get; set; }
    }
}
