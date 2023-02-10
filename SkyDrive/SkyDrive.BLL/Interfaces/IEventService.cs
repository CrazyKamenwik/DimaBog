using SkyDrive.BLL.Models;

namespace SkyDrive.BLL.Interfaces
{
    public interface IEventService
    {
        public Task<IEnumerable<EventModel>> GetAllEvents();
        public Task<EventModel> GetEventById(int id);
        public Task<EventModel> CreateEvent(EventModel eventModel);
        public Task<EventModel> UpdateEvent(EventModel eventModel);
        public Task DeleteEvent(int id);
    }
}
