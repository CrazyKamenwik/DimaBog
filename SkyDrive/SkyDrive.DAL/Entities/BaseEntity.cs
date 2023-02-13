using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Entities
{
    public abstract class BaseEntity : IEntity

    {
        public int Id { get; set; }
    }
}
