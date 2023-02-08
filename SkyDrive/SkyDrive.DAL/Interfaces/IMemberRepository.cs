using SkyDrive.DAL.Entities;

namespace SkyDrive.DAL.Interfaces
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<MemberEntity>> GetAllMembers();
        public Task<MemberEntity?> GetMemberById(int id);
        public Task<MemberEntity> CreateMember(MemberEntity memberEntity);
        public Task<MemberEntity> UpdateMember(MemberEntity memberEntity);
        public Task DeleteMember(MemberEntity memberEntity);
    }
}
