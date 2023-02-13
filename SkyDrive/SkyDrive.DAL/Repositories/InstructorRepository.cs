using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Repositories
{
    public class InstructorRepository : GenericRepository<InstructorEntity>, IInstructorRepository
    {
        public InstructorRepository(ApplicationContext context)
            : base(context)
        { }
    }
}
