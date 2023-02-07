using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Entities;
using SkyDrive.BLL.Interfaces;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _eventService;


        public EventController(ILogger<EventController> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IEnumerable<EventEntity>> GetAll()
        {
            return await _eventService.GetAllEvents();
        }

        [HttpGet("{id:int}")]
        public async Task<EventEntity> GetById(int id)
        {
            var entity = await _eventService.GetEventById(id);

            return entity;
        }

        [HttpPost]
        public async Task<EventEntity> Post(EventEntity eventEntity)
        {
            var result = await _eventService.CreateEventEntity(eventEntity);

            return result;
        }

        [HttpPut]
        public async Task<EventEntity> Put(EventEntity eventEntity)
        {
            var result = await _eventService.UpdateEventEntity(eventEntity);

            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _eventService.DeleteEventEntity(id);
        }
    }
}
