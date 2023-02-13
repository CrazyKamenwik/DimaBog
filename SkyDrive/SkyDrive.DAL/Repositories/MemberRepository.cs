using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Repositories
{
    public class MemberRepository : GenericRepository<MemberEntity>, IMemberRepository
    {
        public MemberRepository(ApplicationContext context)
            : base(context)
        { }
    }
}
