using Microsoft.AspNetCore.Mvc;
using SkyDrive.Entities;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly ILogger<InstructorController> _logger;
        private readonly ApplicationContext _context;

        public InstructorController(ILogger<InstructorController> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public List<InstructorEntity> GetAll()
        {
            return _context.Instructors.ToList();
        }

        [HttpPost]
        public InstructorEntity Post(InstructorEntity instructorEntity)
        {
            _context.Instructors.Add(instructorEntity);
            _context.SaveChanges();

            return instructorEntity;
        }
    }
}
