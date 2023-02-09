using SkyDrive.DAL.Entities;

namespace SkyDrive.BLL.Interfaces
{
    public interface IInstructorService
    {
        public Task<IEnumerable<InstructorEntity>> GetAllInstructors();
        public Task<InstructorEntity> GetInstructorById(int id);
        public Task<InstructorEntity> CreateInstructor(InstructorEntity instructorEntity);
        public Task<InstructorEntity> UpdateInstructor(InstructorEntity instructorEntity);
        public Task DeleteInstructor(int id);
    }
}
