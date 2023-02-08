using Microsoft.EntityFrameworkCore;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationContext _context;

        public MemberRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberEntity>> GetAllMembers()
        {
            return await _context.Members.AsNoTracking().ToListAsync();
        }

        public async Task<MemberEntity?> GetMemberById(int id)
        {
            return await _context.Members.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<MemberEntity> CreateMember(MemberEntity memberEntity)
        {
            _context.Members.Add(memberEntity);
            await _context.SaveChangesAsync();

            return memberEntity;
        }

        public async Task<MemberEntity> UpdateMember(MemberEntity memberEntity)
        {
            _context.Members.Update(memberEntity);
            await _context.SaveChangesAsync();

            return memberEntity;
        }

        public async Task DeleteMember(MemberEntity memberEntity)
        {
            _context.Members.Remove(memberEntity);
            await _context.SaveChangesAsync();
        }
    }
}
