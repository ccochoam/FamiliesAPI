namespace FamiliesAPI.Entities.Models
{
    public class FamilyGroupModel
    {
        public int FamilyGroupId { get; set; }
        public string Name { get; set; }
        public string NumberId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
