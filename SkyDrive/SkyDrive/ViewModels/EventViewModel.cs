namespace SkyDrive.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public DateTime DateTimeOfEvent { get; set; }

        public InstructorViewModel? Instructor { get; set; }
        public ICollection<MemberViewModel>? Members { get; set; }
    }
}
