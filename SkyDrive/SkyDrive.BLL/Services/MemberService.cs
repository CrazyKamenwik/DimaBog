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

        public async Task<IEnumerable<MemberModel>> GetAllMembers()
        {
            var memberEntities = await _memberRepository.GetAllMembers();

            return memberEntities.Adapt<IEnumerable<MemberModel>>();
        }

        public async Task<MemberModel> GetMemberById(int id)
        {
            var entity = await _memberRepository.GetMemberById(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            return entity.Adapt<MemberModel>();
        }

        public async Task<MemberModel> CreateMember(MemberModel memberModel)
        {
            var memberEntity = memberModel.Adapt<MemberEntity>();
            var memberEntityResult = await _memberRepository.CreateMember(memberEntity);

            return memberEntityResult.Adapt<MemberModel>();
        }

        public async Task<MemberModel> UpdateMember(MemberModel memberModel)
        {
            await GetMemberById(memberModel.Id);

            var memberEntity = memberModel.Adapt<MemberEntity>();

            var memberEntityResult = await _memberRepository.UpdateMember(memberEntity);

            return memberEntityResult.Adapt<MemberModel>();
        }

        public async Task DeleteMember(int id)
        {
            var memberModel = await GetMemberById(id);

            await _memberRepository.DeleteMember(memberModel.Adapt<MemberEntity>());
        }
    }
}
