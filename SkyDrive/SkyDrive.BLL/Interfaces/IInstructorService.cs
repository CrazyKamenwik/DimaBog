using SkyDrive.BLL.Models;

namespace SkyDrive.BLL.Interfaces
{
    public interface IInstructorService
    {
        public Task<IEnumerable<InstructorModel>> GetAllInstructors();
        public Task<InstructorModel> GetInstructorById(int id);
        public Task<InstructorModel> CreateInstructor(InstructorModel instructorModel);
        public Task<InstructorModel> UpdateInstructor(InstructorModel instructorModel);
        public Task DeleteInstructor(int id);
    }
}
