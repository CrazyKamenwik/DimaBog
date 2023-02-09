namespace SkyDrive.BLL.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public DateTime DateTimeOfEvent { get; set; }

        public InstructorModel? Instructor { get; set; }
        public ICollection<MemberModel>? Members { get; set; }
    }
}
