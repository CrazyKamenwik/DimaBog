using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Interfaces;
using SkyDrive.DAL.Entities;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _service;

        public EventController(ILogger<EventController> logger, IEventService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<EventEntity>> GetAllEvents()
        {
            return await _service.GetAllEvents();
        }

        [HttpGet("{id:int}")]
        public async Task<EventEntity> GetEventById(int id)
        {
            return await _service.GetEventById(id);
        }

        [HttpPost]
        public async Task<EventEntity> CreateEvent(EventEntity eventEntity)
        {
            return await _service.CreateEvent(eventEntity);
        }

        [HttpPut]
        public async Task<EventEntity> UpdateEvent(EventEntity eventEntity)
        {
            return await _service.UpdateEvent(eventEntity);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _service.DeleteEvent(id);
        }
    }
}
