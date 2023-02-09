namespace SkyDrive.BLL.Models
{
    public class InstructorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Experience { get; set; }

        public ICollection<EventModel>? Events { get; set; }
    }
}
