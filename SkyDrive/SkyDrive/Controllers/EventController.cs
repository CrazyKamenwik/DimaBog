using Microsoft.AspNetCore.Mvc;
using SkyDrive.Entities;

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

        [HttpPost]
        public EventEntity Post(EventEntity eventEntity)
        {
            _context.Events.Add(eventEntity);
            _context.SaveChanges();

            return eventEntity;
        }
    }
}
