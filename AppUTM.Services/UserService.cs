using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUser(User newUser)
        {
            await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        /// <summary>Return all users</summary>
        /// <returns> <c>Array</c> of users</returns>
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetById(id);
        }

        public async Task UpdateUser(User UserToBeUpdated, User User)
        {
            UserToBeUpdated.ApellidoMaterno = User.ApellidoMaterno;
            UserToBeUpdated.ApellidoPaterno = User.ApellidoPaterno;
            UserToBeUpdated.ClaveEmpleado = User.ClaveEmpleado;
            UserToBeUpdated.Nombres = User.Nombres;
            UserToBeUpdated.UpdateAt = DateTime.Now;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUser(User User)
        {
            _unitOfWork.Users.Remove(User);
            await _unitOfWork.CommitAsync();
        }
    }
}