namespace Prologica.DAL.Repositories
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        Task<T> Add(T obj);
        void Commit();
        void DetachAllEntities();
        void Dispose();
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Remove(T obj);
        void Rollback();
        Task<T> Update(T obj);
    }
}