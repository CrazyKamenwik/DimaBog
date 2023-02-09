using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Interfaces;
using SkyDrive.DAL.Entities;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly ILogger<InstructorController> _logger;
        private readonly IInstructorService _service;

        public InstructorController(ILogger<InstructorController> logger, IInstructorService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<InstructorEntity>> GetAllInstructors()
        {
            return await _service.GetAllInstructors();
        }

        [HttpGet("{id:int}")]
        public async Task<InstructorEntity> GetInstructorById(int id)
        {
            return await _service.GetInstructorById(id);
        }

        [HttpPost]
        public async Task<InstructorEntity> CreateInstructor(InstructorEntity instructorEntity)
        {
            return await _service.CreateInstructor(instructorEntity);
        }

        [HttpPut]
        public async Task<InstructorEntity> UpdateInstructor(InstructorEntity instructorEntity)
        {
            return await _service.UpdateInstructor(instructorEntity);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _service.DeleteInstructor(id);
        }
    }
}
