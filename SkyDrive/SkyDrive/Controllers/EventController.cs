using Microsoft.AspNetCore.Mvc;
using SkyDrive.Entities;
using SkyDrive.Exceptions;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly ApplicationContext _context;

        public EventController(ILogger<EventController> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public List<EventEntity> GetAll()
        {
            return _context.Events.ToList();
        }

        [HttpGet("{id:int}")]
        public EventEntity? GetById(int id)
        {
            var entity = _context.Events.Find(id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Event with id: {id} not found");
            }

            return _context.Events.Find(id);
        }

        [HttpPost]
        public EventEntity Post(EventEntity eventEntity)
        {
            _context.Events.Add(eventEntity);
            _context.SaveChanges();

            return eventEntity;
        }

        [HttpPut]
        public EventEntity Put(EventEntity eventEntity)
        {
            var entity = _context.Events.Find(eventEntity.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Event with id: {eventEntity.Id} not found");
            }

            _context.Events.Update(eventEntity);
            _context.SaveChanges();

            return eventEntity;
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            var entity = _context.Events.Find(id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Event with id: {id} not found");
            }

            _context.Events.Remove(entity);
            _context.SaveChanges();
        }
    }
}
