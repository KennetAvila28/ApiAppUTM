using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Services
{
    public class UserService : IUserService
    {
        //todo:Implement unitofwork
        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUser(User newUser)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User UserToBeUpdated, User User)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(User User)
        {
            throw new NotImplementedException();
        }
    }
}