using SkyDrive.BLL.Entities;

namespace SkyDrive.BLL.Interfaces
{
    public interface IInstructorService
    {
        public Task<IEnumerable<InstructorEntity>> GetAllInstructorEntities();
        public Task<InstructorEntity> GetInstructorEntityById(int id);
        public Task<InstructorEntity> CreateInstructorEntity(InstructorEntity instructorEntity);
        public Task<InstructorEntity> UpdateInstructorEntity(InstructorEntity instructorEntity);
        public Task DeleteInstructorEntity(int id);
    }
}
