using System.Collections.Generic;
using System.Threading.Tasks;
using UserMngt.Core;
using UserMngt.Core.Models;
using UserMngt.Core.Services;

namespace UserMngt.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUser(User newUser)
        {
            newUser.Id = _unitOfWork.Users.GetNewId();
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        public async Task DeleteUser(User User)
        {
            _unitOfWork.Users.Remove(User);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Users
                .GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users
                .GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetUserByLogin(string login)
        {
            return await _unitOfWork.Users.GetByLoginAsync(login);
        }

        public async Task UpdateUser(User UserToBeUpdated, User User)
        {
            if(User.Name != string.Empty){
                UserToBeUpdated.Name = User.Name;
            }
            
            if(User.Pass != string.Empty){
                UserToBeUpdated.Pass = User.Pass;
            }

            UserToBeUpdated.Active = User.Active;

            await _unitOfWork.CommitAsync();
        }

        public async Task ActivateUser(User UserToActivate)
        {
            UserToActivate.Active = true;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeactivateUser(User UserToDeactivate)
        {
            UserToDeactivate.Active = false;

            await _unitOfWork.CommitAsync();
        }
    }
}