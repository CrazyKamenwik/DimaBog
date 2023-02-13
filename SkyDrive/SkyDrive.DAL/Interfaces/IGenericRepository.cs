namespace SkyDrive.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity
    {
        public Task<T?> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task Delete(T entity);
    }
}
