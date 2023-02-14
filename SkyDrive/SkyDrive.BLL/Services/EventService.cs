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

        public async Task<IEnumerable<EventModel>> GetAllEvents(CancellationToken cancellationToken)
        {
            var eventEntities = await _eventRepository.GetAll(cancellationToken);

            return eventEntities.Adapt<IEnumerable<EventModel>>();
        }

        public async Task<EventModel> GetEventById(int id, CancellationToken cancellationToken)
        {
            var entity = await _eventRepository.GetById(id, cancellationToken);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Event with id: {id} not found");
            }

            return entity.Adapt<EventModel>();
        }

        public async Task<EventModel> CreateEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            var eventEntity = eventModel.Adapt<EventEntity>();

            var eventEntityResult = await _eventRepository.Create(eventEntity, cancellationToken);

            return eventEntityResult.Adapt<EventModel>();
        }

        public async Task<EventModel> UpdateEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            await GetEventById(eventModel.Id, cancellationToken);

            var eventEntity = eventModel.Adapt<EventEntity>();

            var eventEntityResult = await _eventRepository.Update(eventEntity, cancellationToken);

            return eventEntityResult.Adapt<EventModel>();
        }

        public async Task DeleteEvent(int id, CancellationToken cancellationToken)
        {
            var eventModel = await GetEventById(id, cancellationToken);

            await _eventRepository.Delete(eventModel.Adapt<EventEntity>(), cancellationToken);
        }
    }
}
