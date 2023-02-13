using Mapster;
using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Interfaces;
using SkyDrive.BLL.Models;
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

        public async Task<IEnumerable<EventModel>> GetAllEvents()
        {
            var eventEntities = await _eventRepository.GetAll();

            return eventEntities.Adapt<IEnumerable<EventModel>>();
        }

        public async Task<EventModel> GetEventById(int id)
        {
            var entity = await _eventRepository.GetById(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Event with id: {id} not found");
            }

            return entity.Adapt<EventModel>();
        }

        public async Task<EventModel> CreateEvent(EventModel eventModel)
        {
            var eventEntity = eventModel.Adapt<EventEntity>();

            var eventEntityResult = await _eventRepository.Create(eventEntity);

            return eventEntityResult.Adapt<EventModel>();
        }

        public async Task<EventModel> UpdateEvent(EventModel eventModel)
        {
            await GetEventById(eventModel.Id);

            var eventEntity = eventModel.Adapt<EventEntity>();

            var eventEntityResult = await _eventRepository.Update(eventEntity);

            return eventEntityResult.Adapt<EventModel>();
        }

        public async Task DeleteEvent(int id)
        {
            var eventModel = await GetEventById(id);

            await _eventRepository.Delete(eventModel.Adapt<EventEntity>());
        }
    }
}
