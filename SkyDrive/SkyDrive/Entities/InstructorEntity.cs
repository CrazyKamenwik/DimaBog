namespace SkyDrive.Entities
{
    public class InstructorEntity : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Experience { get; set; }

        public ICollection<EventEntity>? Events { get; set; }
    }
}
