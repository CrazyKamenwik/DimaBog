using Mapster;
using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Interfaces;
using SkyDrive.BLL.Models;
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

        public async Task<IEnumerable<InstructorModel>> GetAllInstructors()
        {
            var instructorEntities = await _instructorRepository.GetAllInstructors();

            return instructorEntities.Adapt<IEnumerable<InstructorModel>>();
        }

        public async Task<InstructorModel> GetInstructorById(int id)
        {
            var entity = await _instructorRepository.GetInstructorById(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Instructor with id: {id} not found");
            }

            return entity.Adapt<InstructorModel>();
        }

        public async Task<InstructorModel> CreateInstructor(InstructorModel instructorModel)
        {
            var instructorEntity = instructorModel.Adapt<InstructorEntity>();
            var instructorEntityResult = await _instructorRepository.CreateInstructor(instructorEntity);

            return instructorEntityResult.Adapt<InstructorModel>();
        }

        public async Task<InstructorModel> UpdateInstructor(InstructorModel instructorModel)
        {
            await GetInstructorById(instructorModel.Id);

            var instructorEntity = instructorModel.Adapt<InstructorEntity>();

            var instructorEntityResult = await _instructorRepository.UpdateInstructor(instructorEntity);

            return instructorEntityResult.Adapt<InstructorModel>();
        }

        public async Task DeleteInstructor(int id)
        {
            var instructorModel = await GetInstructorById(id);

            await _instructorRepository.DeleteInstructor(instructorModel.Adapt<InstructorEntity>());
        }
    }
}
