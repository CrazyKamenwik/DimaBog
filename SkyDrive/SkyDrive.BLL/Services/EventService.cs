using Microsoft.EntityFrameworkCore;
using SkyDrive.BLL.Entities;
using SkyDrive.BLL.Interfaces;
using SkyDrive.Exceptions;

namespace SkyDrive.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationContext _context;

        public EventService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventEntity>> GetAllEvents()
        {
            return await _context.Events.AsNoTracking().ToListAsync();
        }

        public async Task<EventEntity> GetEventById(int id)
        {
            return await GetEntity(id);
        }

        public async Task<EventEntity> CreateEventEntity(EventEntity eventEntity)
        {
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            return eventEntity;
        }

        public async Task<EventEntity> UpdateEventEntity(EventEntity eventEntity)
        {
            await GetEntity(eventEntity.Id);

            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();

            return eventEntity;
        }

        public async Task DeleteEventEntity(int id)
        {
            var entity = await GetEntity(id);

            _context.Events.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private async Task<EventEntity> GetEntity(int id)
        {
            var entity = await _context.Events.FindAsync(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Event with id: {id} not found");
            }

            return entity;
        }
    }
}
