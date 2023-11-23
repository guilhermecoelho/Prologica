using Prologica.DAL.imports;

namespace Prologica.DAL.Repositories
{
    public class ConsoleRepository : RepositoryBase<imports.Console>, IConsoleRepository
    {
        private new readonly PrologicaContext _db;

        public ConsoleRepository(PrologicaContext db) : base(db)
            => _db = db;
    }
}
