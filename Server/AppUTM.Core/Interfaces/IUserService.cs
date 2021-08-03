using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> CreateUser(User newUser);

        Task UpdateUser(User userToBeUpdated);

        Task DeleteUser(User user);
    }
}