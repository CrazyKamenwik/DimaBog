using Microsoft.EntityFrameworkCore;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationContext _context;

        public InstructorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorEntity>> GetAllInstructors()
        {
            return await _context.Instructors.AsNoTracking().ToListAsync();
        }

        public async Task<InstructorEntity?> GetInstructorById(int id)
        {
            return await _context.Instructors.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<InstructorEntity> CreateInstructor(InstructorEntity instructorEntity)
        {
            _context.Instructors.Add(instructorEntity);
            await _context.SaveChangesAsync();

            return instructorEntity;
        }

        public async Task<InstructorEntity> UpdateInstructor(InstructorEntity instructorEntity)
        {
            _context.Instructors.Update(instructorEntity);
            await _context.SaveChangesAsync();

            return instructorEntity;
        }

        public async Task DeleteInstructor(InstructorEntity instructorEntity)
        {
            _context.Instructors.Remove(instructorEntity);
            await _context.SaveChangesAsync();
        }
    }
}
