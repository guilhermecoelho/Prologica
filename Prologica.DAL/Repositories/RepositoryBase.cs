using Microsoft.EntityFrameworkCore;
using Prologica.DAL.imports;

namespace Prologica.DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected PrologicaContext _db;

        public RepositoryBase(PrologicaContext db)
        {
            _db = db;
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public virtual async Task<T> Add(T obj)
        {
            await _db.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> Remove(T obj)
        {
            _db.Remove(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<T> Update(T obj)
        {
            _db.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = _db.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public void Commit()
        { _db.SaveChanges(); }

        public void Rollback()
        { _db.Dispose(); }
    }
}
