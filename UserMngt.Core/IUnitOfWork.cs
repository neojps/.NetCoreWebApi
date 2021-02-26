using System;
using System.Threading.Tasks;
using UserMngt.Core.Repositories;

namespace UserMngt.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        Task<int> CommitAsync();
    }
}