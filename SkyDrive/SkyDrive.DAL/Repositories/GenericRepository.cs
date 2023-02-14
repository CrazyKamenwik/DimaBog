using Microsoft.EntityFrameworkCore;
using SkyDrive.DAL.Entities;
using SkyDrive.DAL.Interfaces;

namespace SkyDrive.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T> Create(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task Delete(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
