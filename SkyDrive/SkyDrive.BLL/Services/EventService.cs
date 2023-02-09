using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Interfaces;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository repository)
        {
            _eventRepository = repository;
        }

        public async Task<IEnumerable<EventEntity>> GetAllEvents()
        {
            return await _eventRepository.GetAllEvents();
        }

        public async Task<EventEntity> GetEventById(int id)
        {
            return await GetEntity(id);
        }

        public async Task<EventEntity> CreateEvent(EventEntity eventEntity)
        {
            return await _eventRepository.CreateEvent(eventEntity);
        }

        public async Task<EventEntity> UpdateEvent(EventEntity eventEntity)
        {
            await GetEntity(eventEntity.Id);

            return await _eventRepository.UpdateEvent(eventEntity);
        }

        public async Task DeleteEvent(int id)
        {
            var eventEntity = await GetEntity(id);

            await _eventRepository.DeleteEvent(eventEntity);
        }

        private async Task<EventEntity> GetEntity(int id)
        {
            var entity = await _eventRepository.GetEventById(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Event with id: {id} not found");
            }

            return entity;
        }
    }
}
