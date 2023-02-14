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
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<MemberModel>> GetAllMembers(CancellationToken cancellationToken)
        {
            var memberEntities = await _memberRepository.GetAll(cancellationToken);

            return memberEntities.Adapt<IEnumerable<MemberModel>>();
        }

        public async Task<MemberModel> GetMemberById(int id, CancellationToken cancellationToken)
        {
            var entity = await _memberRepository.GetById(id, cancellationToken);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            return entity.Adapt<MemberModel>();
        }

        public async Task<MemberModel> CreateMember(MemberModel memberModel, CancellationToken cancellationToken)
        {
            var memberEntity = memberModel.Adapt<MemberEntity>();

            var memberEntityResult = await _memberRepository.Create(memberEntity, cancellationToken);

            return memberEntityResult.Adapt<MemberModel>();
        }

        public async Task<MemberModel> UpdateMember(MemberModel memberModel, CancellationToken cancellationToken)
        {
            await GetMemberById(memberModel.Id, cancellationToken);

            var memberEntity = memberModel.Adapt<MemberEntity>();

            var memberEntityResult = await _memberRepository.Update(memberEntity, cancellationToken);

            return memberEntityResult.Adapt<MemberModel>();
        }

        public async Task DeleteMember(int id, CancellationToken cancellationToken)
        {
            var memberModel = await GetMemberById(id, cancellationToken);

            await _memberRepository.Delete(memberModel.Adapt<MemberEntity>(), cancellationToken);
        }
    }
}
