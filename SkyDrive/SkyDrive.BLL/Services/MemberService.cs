using Microsoft.EntityFrameworkCore;
using SkyDrive.BLL.Entities;
using SkyDrive.BLL.Interfaces;
using SkyDrive.Exceptions;

namespace SkyDrive.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationContext _context;

        public MemberService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberEntity>> GetAllMemberEntities()
        {
            return await _context.Members.AsNoTracking().ToListAsync();
        }

        public async Task<MemberEntity> GetMemberEntityById(int id)
        {
            return await GetEntity(id);
        }

        public async Task<MemberEntity> CreateMemberEntity(MemberEntity memberEntity)
        {
            _context.Members.Add(memberEntity);
            await _context.SaveChangesAsync();

            return memberEntity;
        }

        public async Task<MemberEntity> UpdateMemberEntity(MemberEntity memberEntity)
        {
            await GetEntity(memberEntity.Id);

            _context.Members.Update(memberEntity);
            await _context.SaveChangesAsync();

            return memberEntity;
        }

        public async Task DeleteMemberEntity(int id)
        {
            var entity = await GetEntity(id);

            _context.Members.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private async Task<MemberEntity> GetEntity(int id)
        {
            var entity = await _context.Members.FindAsync(id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Member with id: {id} not found");
            }

            return entity;
        }
    }
}
