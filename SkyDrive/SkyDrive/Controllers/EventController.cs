using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Exceptions;
using SkyDrive.DAL;
using SkyDrive.DAL.Entities;

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
        public EventEntity GetById(int id)
        {
            var entity = GetEntity(id);

            return entity;
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
            GetEntity(eventEntity.Id);

            _context.Events.Update(eventEntity);
            _context.SaveChanges();

            return eventEntity;
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            var entity = GetEntity(id);

            _context.Events.Remove(entity!);
            _context.SaveChanges();
        }

        private EventEntity GetEntity(int id)
        {
            var entity = _context.Events.Find(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Event with id: {id} not found");
            }

            return entity;
        }
    }
}
