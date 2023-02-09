using SkyDrive.DAL.Entities;

namespace SkyDrive.DAL.Interfaces
{
    public interface IInstructorRepository
    {
        public Task<IEnumerable<InstructorEntity>> GetAllInstructors();
        public Task<InstructorEntity?> GetInstructorById(int id);
        public Task<InstructorEntity> CreateInstructor(InstructorEntity instructorEntity);
        public Task<InstructorEntity> UpdateInstructor(InstructorEntity instructorEntity);
        public Task DeleteInstructor(InstructorEntity instructorEntity);
    }
}
