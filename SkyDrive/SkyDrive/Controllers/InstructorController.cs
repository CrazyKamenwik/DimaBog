using Microsoft.AspNetCore.Mvc;
using SkyDrive.BLL.Entities;
using SkyDrive.BLL.Interfaces;

namespace SkyDrive.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly ILogger<InstructorController> _logger;
        private readonly IInstructorService _instructorService;

        public InstructorController(ILogger<InstructorController> logger, IInstructorService instructorService)
        {
            _instructorService = instructorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<InstructorEntity>> GetAll()
        {
            return await _instructorService.GetAllInstructorEntities();
        }

        [HttpGet("{id:int}")]
        public async Task<InstructorEntity?> GetById(int id)
        {
            var entity = await _instructorService.GetInstructorEntityById(id);

            return entity;
        }

        [HttpPost]
        public async Task<InstructorEntity> Post(InstructorEntity instructorEntity)
        {
            var entity = await _instructorService.CreateInstructorEntity(instructorEntity);

            return entity;
        }

        [HttpPut]
        public async Task<InstructorEntity> Put(InstructorEntity instructorEntity)
        {
            var result = await _instructorService.UpdateInstructorEntity(instructorEntity);

            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _instructorService.DeleteInstructorEntity(id);
        }
    }
}

