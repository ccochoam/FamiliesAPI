namespace FamiliesAPI.Entities.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string HashKey { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NumberId { get; set; }
        public int GenderId { get; set; }
        public string Relationship { get; set; }
        public int Age { get; set; }
        public bool UnderAge { get; set; }
        public string Birthdate { get; set; }
        public int FamilyGroupId { get; set; }
        public UserModel() { }
    }
}
