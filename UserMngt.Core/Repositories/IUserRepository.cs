using System.Collections.Generic;
using System.Threading.Tasks;
using UserMngt.Core.Models;

namespace UserMngt.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetByLoginAsync(string login);
        int GetNewId();
    }
}