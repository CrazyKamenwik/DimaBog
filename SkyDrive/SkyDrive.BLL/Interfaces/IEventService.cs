using SkyDrive.BLL.Models;

namespace SkyDrive.BLL.Interfaces
{
    public interface IEventService
    {
        public Task<IEnumerable<EventModel>> GetAllEvents(CancellationToken cancellationToken);
        public Task<EventModel> GetEventById(int id, CancellationToken cancellationToken);
        public Task<EventModel> CreateEvent(EventModel eventModel, CancellationToken cancellationToken);
        public Task<EventModel> UpdateEvent(EventModel eventModel, CancellationToken cancellationToken);
        public Task DeleteEvent(int id, CancellationToken cancellationToken);
    }
}
