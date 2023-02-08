using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Interfaces;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MemberEntity>> GetAllMember()
        {
            return await _repository.GetAllMembers();
        }

        public async Task<MemberEntity> GetMemberById(int id)
        {
            return await GetEntity(id);
        }

        public async Task<MemberEntity> CreateMember(MemberEntity memberEntity)
        {
            return await _repository.CreateMember(memberEntity);
        }

        public async Task<MemberEntity> UpdateMember(MemberEntity memberEntity)
        {
            await GetEntity(memberEntity.Id);

            return await _repository.UpdateMember(memberEntity);
        }

        public async Task DeleteMember(int id)
        {
            var memberEntity = await GetEntity(id);

            await _repository.DeleteMember(memberEntity);
        }

        private async Task<MemberEntity> GetEntity(int id)
        {
            var entity = await _repository.GetMemberById(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            return entity;
        }
    }
}
