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

        public async Task<IEnumerable<InstructorModel>> GetAllInstructors(CancellationToken cancellationToken)
        {
            var instructorEntities = await _instructorRepository.GetAll(cancellationToken);

            return instructorEntities.Adapt<IEnumerable<InstructorModel>>();
        }

        public async Task<InstructorModel> GetInstructorById(int id, CancellationToken cancellationToken)
        {
            var entity = await _instructorRepository.GetById(id, cancellationToken);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Instructor with id: {id} not found");
            }

            return entity.Adapt<InstructorModel>();
        }

        public async Task<InstructorModel> CreateInstructor(InstructorModel instructorModel, CancellationToken cancellationToken)
        {
            var instructorEntity = instructorModel.Adapt<InstructorEntity>();

            var instructorEntityResult = await _instructorRepository.Create(instructorEntity, cancellationToken);

            return instructorEntityResult.Adapt<InstructorModel>();
        }

        public async Task<InstructorModel> UpdateInstructor(InstructorModel instructorModel, CancellationToken cancellationToken)
        {
            await GetInstructorById(instructorModel.Id, cancellationToken);

            var instructorEntity = instructorModel.Adapt<InstructorEntity>();

            var instructorEntityResult = await _instructorRepository.Update(instructorEntity, cancellationToken);

            return instructorEntityResult.Adapt<InstructorModel>();
        }

        public async Task DeleteInstructor(int id, CancellationToken cancellationToken)
        {
            var instructorModel = await GetInstructorById(id, cancellationToken);

            await _instructorRepository.Delete(instructorModel.Adapt<InstructorEntity>(), cancellationToken);
        }
    }
}
