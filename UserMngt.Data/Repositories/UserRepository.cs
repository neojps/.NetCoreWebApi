using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserMngt.Core.Models;
using UserMngt.Core.Repositories;

namespace UserMngt.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserMngtDbContext context) 
            : base(context)
        { }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await UserMngtDbContext.Users
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await UserMngtDbContext.Users
                .SingleOrDefaultAsync(m => m.Id == id);;
        }

        public async Task<IEnumerable<User>> GetByLoginAsync(string login)
        {
            return await UserMngtDbContext.Users
                .Where(m => m.Login == login).ToListAsync();
        }

        public int GetNewId(){
            var newId = (UserMngtDbContext.Users.Max(u => u.Id)) + 1;
            return newId;
        }
        
        private UserMngtDbContext UserMngtDbContext
        {
            get { return Context as UserMngtDbContext; }
        }
    }
}