namespace SkyDrive.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity
    {
        public Task<T?> GetById(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        public Task<T> Create(T entity, CancellationToken cancellationToken);
        public Task<T> Update(T entity, CancellationToken cancellationToken);
        public Task Delete(T entity, CancellationToken cancellationToken);
    }
}
