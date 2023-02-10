namespace SkyDrive.BLL.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<EventModel>? Events { get; set; }
    }
}
