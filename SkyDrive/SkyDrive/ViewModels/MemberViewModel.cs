namespace SkyDrive.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<EventViewModel>? Events { get; set; }
    }
}
