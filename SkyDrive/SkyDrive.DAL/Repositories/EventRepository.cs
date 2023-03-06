using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Repositories
{
    internal class EventRepository : GenericRepository<EventEntity>, IEventRepository
    {
        public EventRepository(ApplicationContext context)
            : base(context)
        { }
    }
}
