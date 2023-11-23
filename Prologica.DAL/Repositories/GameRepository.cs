using Prologica.DAL.imports;

namespace Prologica.DAL.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        private new readonly PrologicaContext _db;

        public GameRepository(PrologicaContext db) : base(db)
            => _db = db;
    }
}
