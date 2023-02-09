using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Interfaces;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.BLL.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository repository)
        {
            _instructorRepository = repository;
        }

        public async Task<IEnumerable<InstructorEntity>> GetAllInstructors()
        {
            return await _instructorRepository.GetAllInstructors();
        }

        public async Task<InstructorEntity> GetInstructorById(int id)
        {
            return await GetEntity(id);
        }

        public async Task<InstructorEntity> CreateInstructor(InstructorEntity instructorEntity)
        {
            return await _instructorRepository.CreateInstructor(instructorEntity);
        }

        public async Task<InstructorEntity> UpdateInstructor(InstructorEntity instructorEntity)
        {
            await GetEntity(instructorEntity.Id);

            return await _instructorRepository.UpdateInstructor(instructorEntity);
        }

        public async Task DeleteInstructor(int id)
        {
            var instructorEntity = await GetEntity(id);

            await _instructorRepository.DeleteInstructor(instructorEntity);
        }

        private async Task<InstructorEntity> GetEntity(int id)
        {
            var entity = await _instructorRepository.GetInstructorById(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Instructor with id: {id} not found");
            }

            return entity;
        }
    }
}
