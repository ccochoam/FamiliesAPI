namespace FamiliesAPI.Entities.Models
{
    public class LoggerModel
    {
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
