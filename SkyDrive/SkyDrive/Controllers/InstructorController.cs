using Microsoft.AspNetCore.Mvc;
using SkyDrive.DAL;
using SkyDrive.DAL.Entities;
using SkyDrive.Exceptions;

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

        [HttpGet("{id:int}")]
        public InstructorEntity? GetById(int id)
        {
            var entity = GetEntity(id);

            return entity;
        }

        [HttpPost]
        public InstructorEntity Post(InstructorEntity instructorEntity)
        {
            _context.Instructors.Add(instructorEntity);
            _context.SaveChanges();

            return instructorEntity;
        }

        [HttpPut]
        public InstructorEntity Put(InstructorEntity instructorEntity)
        {
            GetEntity(instructorEntity.Id);

            _context.Instructors.Update(instructorEntity);
            _context.SaveChanges();

            return instructorEntity;
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            var entity = GetEntity(id);

            _context.Instructors.Remove(entity);
            _context.SaveChanges();
        }

        private InstructorEntity GetEntity(int id)
        {
            var entity = _context.Instructors.Find(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Instructor with id: {id} not found");
            }

            return entity;
        }
    }
}
