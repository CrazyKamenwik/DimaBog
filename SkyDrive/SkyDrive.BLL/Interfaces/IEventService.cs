using SkyDrive.BLL.Entities;

namespace SkyDrive.BLL.Interfaces
{
    public interface IEventService
    {
        public Task<IEnumerable<EventEntity>> GetAllEvents();
        public Task<EventEntity> GetEventById(int id);
        public Task<EventEntity> CreateEventEntity(EventEntity eventEntity);
        public Task<EventEntity> UpdateEventEntity(EventEntity eventEntity);
        public Task DeleteEventEntity(int id);
    }
}
