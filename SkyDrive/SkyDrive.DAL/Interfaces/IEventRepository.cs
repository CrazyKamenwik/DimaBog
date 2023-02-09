using SkyDrive.DAL.Entities;

namespace SkyDrive.DAL.Interfaces
{
    public interface IEventRepository
    {
        public Task<IEnumerable<EventEntity>> GetAllEvents();
        public Task<EventEntity?> GetEventById(int id);
        public Task<EventEntity> CreateEvent(EventEntity eventEntity);
        public Task<EventEntity> UpdateEvent(EventEntity eventEntity);
        public Task DeleteEvent(EventEntity eventEntity);
    }
}
