using SkyDrive.DAL.Entities;

namespace SkyDrive.BLL.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberEntity>> GetAllMember();
        public Task<MemberEntity> GetMemberById(int id);
        public Task<MemberEntity> CreateMember(MemberEntity memberEntity);
        public Task<MemberEntity> UpdateMember(MemberEntity memberEntity);
        public Task DeleteMember(int id);
    }
}
