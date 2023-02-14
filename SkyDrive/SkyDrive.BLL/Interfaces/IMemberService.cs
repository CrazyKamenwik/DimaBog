using SkyDrive.BLL.Models;

namespace SkyDrive.BLL.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberModel>> GetAllMembers(CancellationToken cancellationToken);
        public Task<MemberModel> GetMemberById(int id, CancellationToken cancellationToken);
        public Task<MemberModel> CreateMember(MemberModel memberModel, CancellationToken cancellationToken);
        public Task<MemberModel> UpdateMember(MemberModel memberModel, CancellationToken cancellationToken);
        public Task DeleteMember(int id, CancellationToken cancellationToken);
    }
}
