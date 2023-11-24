using Microsoft.EntityFrameworkCore;
using Prologica.DAL.imports;

namespace Prologica.DAL.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        private new readonly PrologicaContext _db;

        public GameRepository(PrologicaContext db) : base(db)
            => _db = db;

        public async override Task<IEnumerable<Game>> GetAll()
            => await _db.Games.Include(x => x.Console).AsNoTracking().ToListAsync();

        public async override Task<Game> GetById(int id)
           => await _db.Games.Include(x => x.Console).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    }
}
