using Microsoft.EntityFrameworkCore;
using SkyDrive.BLL.Entities;
using SkyDrive.BLL.Interfaces;
using SkyDrive.Exceptions;

namespace SkyDrive.BLL.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly ApplicationContext _context;

        public InstructorService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorEntity>> GetAllInstructorEntities()
        {
            return await _context.Instructors.AsNoTracking().ToListAsync();
        }

        public async Task<InstructorEntity> GetInstructorEntityById(int id)
        {
            return await GetEntity(id);
        }

        public async Task<InstructorEntity> CreateInstructorEntity(InstructorEntity instructorEntity)
        {
            _context.Instructors.Add(instructorEntity);
            await _context.SaveChangesAsync();

            return instructorEntity;
        }

        public async Task<InstructorEntity> UpdateInstructorEntity(InstructorEntity instructorEntity)
        {
            await GetEntity(instructorEntity.Id);

            _context.Instructors.Update(instructorEntity);
            await _context.SaveChangesAsync();

            return instructorEntity;
        }

        public async Task DeleteInstructorEntity(int id)
        {
            var entity = await GetEntity(id);

            _context.Instructors.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private async Task<InstructorEntity> GetEntity(int id)
        {
            var entity = await _context.Instructors.FindAsync(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Instructor with id: {id} not found");
            }

            return entity;
        }
    }
}
