using Mapster;
using SkyDrive.BLL.Exceptions;
using SkyDrive.BLL.Interfaces;
using SkyDrive.BLL.Models;
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

        public async Task<IEnumerable<MemberModel>> GetAllMembers()
        {
            var memberEntities = await _repository.GetAllMembers();

            return memberEntities.Adapt<IEnumerable<MemberModel>>();
        }

        public async Task<MemberModel> GetMemberById(int id)
        {
            return await GetEntity(id);
        }

        public async Task<MemberModel> CreateMember(MemberModel memberModel)
        {
            var memberEntity = memberModel.Adapt<MemberEntity>();
            var memberEntityResult = await _repository.CreateMember(memberEntity);

            return memberEntityResult.Adapt<MemberModel>();
        }

        public async Task<MemberModel> UpdateMember(MemberModel memberModel)
        {
            await GetEntity(memberModel.Id);

            var memberEntity = memberModel.Adapt<MemberEntity>();
            var memberEntityResult = await _repository.UpdateMember(memberEntity);

            return memberEntityResult.Adapt<MemberModel>();
        }

        public async Task DeleteMember(int id)
        {
            var memberEntity = await GetEntity(id);

            await _repository.DeleteMember(memberEntity.Adapt<MemberEntity>());
        }

        private async Task<MemberModel> GetEntity(int id)
        {
            var entity = await _repository.GetMemberById(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            return entity.Adapt<MemberModel>();
        }
    }
}
