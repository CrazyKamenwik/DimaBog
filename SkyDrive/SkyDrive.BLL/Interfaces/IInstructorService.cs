using SkyDrive.BLL.Models;

namespace SkyDrive.BLL.Interfaces
{
    public interface IInstructorService
    {
        public Task<IEnumerable<InstructorModel>> GetAllInstructors(CancellationToken cancellationToken);
        public Task<InstructorModel> GetInstructorById(int id, CancellationToken cancellationToken);
        public Task<InstructorModel> CreateInstructor(InstructorModel instructorModel, CancellationToken cancellationToken);
        public Task<InstructorModel> UpdateInstructor(InstructorModel instructorModel, CancellationToken cancellationToken);
        public Task DeleteInstructor(int id, CancellationToken cancellationToken);
    }
}
