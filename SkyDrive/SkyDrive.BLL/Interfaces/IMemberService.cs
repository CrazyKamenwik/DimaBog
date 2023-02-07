using SkyDrive.BLL.Entities;

namespace SkyDrive.BLL.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberEntity>> GetAllMemberEntities();
        public Task<MemberEntity> GetMemberEntityById(int id);
        public Task<MemberEntity> CreateMemberEntity(MemberEntity memberEntity);
        public Task<MemberEntity> UpdateMemberEntity(MemberEntity memberEntity);
        public Task DeleteMemberEntity(int id);
    }
}
