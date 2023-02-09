using SkyDrive.DAL.Entities;

namespace SkyDrive.BLL.Interfaces
{
    public interface IEventService
    {
        public Task<IEnumerable<EventEntity>> GetAllEvents();
        public Task<EventEntity> GetEventById(int id);
        public Task<EventEntity> CreateEvent(EventEntity eventEntity);
        public Task<EventEntity> UpdateEvent(EventEntity eventEntity);
        public Task DeleteEvent(int id);
    }
}
