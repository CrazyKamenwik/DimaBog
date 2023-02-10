using SkyDrive.BLL.Models;

namespace SkyDrive.BLL.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberModel>> GetAllMembers();
        public Task<MemberModel> GetMemberById(int id);
        public Task<MemberModel> CreateMember(MemberModel memberModel);
        public Task<MemberModel> UpdateMember(MemberModel memberModel);
        public Task DeleteMember(int id);
    }
}
