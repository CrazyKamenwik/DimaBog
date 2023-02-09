using Microsoft.EntityFrameworkCore;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private readonly ApplicationContext _context;

        public EventRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventEntity>> GetAllEvents()
        {
            return await _context.Events.AsNoTracking().ToListAsync();
        }

        public async Task<EventEntity?> GetEventById(int id)
        {
            return await _context.Events.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EventEntity> CreateEvent(EventEntity eventEntity)
        {
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            return eventEntity;
        }

        public async Task<EventEntity> UpdateEvent(EventEntity eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();

            return eventEntity;
        }

        public async Task DeleteEvent(EventEntity eventEntity)
        {
            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }
    }
}
