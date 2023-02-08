using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Entities
{
    public class MemberEntity : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<EventEntity>? Events { get; set; }
    }
}
