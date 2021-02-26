using System.Threading.Tasks;
using UserMngt.Core;
using UserMngt.Core.Repositories;
using UserMngt.Data.Repositories;

namespace UserMngt.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserMngtDbContext _context;
        private UserRepository _UserRepository;

        public UnitOfWork(UserMngtDbContext context)
        {
            this._context = context;
        }

        public IUserRepository Users => _UserRepository = _UserRepository ?? new UserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}