using System.Collections.Generic;
using System.Threading.Tasks;
using UserMngt.Core.Models;

namespace UserMngt.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUserByLogin(string login);
        Task<User> CreateUser(User newUser);
        Task UpdateUser(User UserToBeUpdated, User User);
        Task DeleteUser(User User);
        Task ActivateUser(User UserToActivate);
        Task DeactivateUser(User UserToDeactivate);
    }
}