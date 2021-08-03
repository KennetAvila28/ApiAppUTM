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

        public UserService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<User> CreateUser(User newUser)
        {
            await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.UserRoles.AddRange(newUser.UserRoles);
            await _unitOfWork.CommitAsync();
            return newUser;
        }

        /// <summary>Return all users</summary>
        /// <returns> <c>Array</c> of users</returns>
        public async Task<IEnumerable<User>> GetAllUsers() => await _context.Users.Include(x => x.UserRoles).ToListAsync();

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
            foreach (var u in user.UserRoles)
            {
                if (!userToBeUpdated.UserRoles.Any(us => us.RoleId == u.RoleId))
                {
                    u.UserId = userToBeUpdated.Id;
                    await _unitOfWork.UserRoles.Add(u);
                }
            }
            foreach (var item in user.RolesToBeDelete)
            {
                foreach (var role in userToBeUpdated.UserRoles.Where(role => role.RoleId == item))
                {
                    _unitOfWork.UserRoles.Remove(role);
                }
            }
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