using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        //Todo: create summary documentation for the all methods
        public UserService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        /// <summary>Return all users with their roles</summary>
        /// <param name="newUser"></param>
        /// <returns>Array of users</returns>
        public async Task<User> CreateUser(User newUser)
        {
            await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.UserRoles.AddRange(newUser.UserRoles);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        /// <summary>Return all users with their roles</summary>
        /// <returns>Array of users</returns>
        public async Task<IEnumerable<User>> GetAllUsers() => await _context.Users.Include(x => x.UserRoles).ToListAsync();

        /// <summary>Get a user with his roles</summary>
        /// <param name="id"></param>
        /// <returns>A user</returns>
        public async Task<User> GetUserById(int id) =>
            await _context.Users.Include(x => x.UserRoles).ThenInclude(y => y.Role)
                .SingleOrDefaultAsync(z => z.Id == id);

        public async Task UpdateUser(User userToBeUpdated, User user)
        {
            userToBeUpdated.ApellidoMaterno = user.ApellidoMaterno;
            userToBeUpdated.ApellidoPaterno = user.ApellidoPaterno;
            userToBeUpdated.ClaveEmpleado = user.ClaveEmpleado;
            userToBeUpdated.Nombres = user.Nombres;
            userToBeUpdated.UpdateAt = DateTime.Now;
            userToBeUpdated.Status = user.Status;

            foreach (var item in user.RolesToBeDelete)
            {
                foreach (var role in userToBeUpdated.UserRoles.Where(role => role.RoleId == item))
                {
                    _unitOfWork.UserRoles.Remove(role);
                }
            }
            _unitOfWork.Users.Update(userToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUser(User user)
        {
            _unitOfWork.UserRoles.RemoveRange(user.UserRoles);
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }
    }
}