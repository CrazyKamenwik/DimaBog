namespace SkyDrive.DAL.Entities
{
    public class EventEntity : BaseEntity
    {
        public int InstructorId { get; set; }
        public DateTime DateTimeOfEvent { get; set; }

        public InstructorEntity? Instructor { get; set; }
        public ICollection<MemberEntity>? Members { get; set; }
    }
}
