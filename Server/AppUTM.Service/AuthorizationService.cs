using System;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AppUTM.Core.Models;

namespace AppUTM.Services
{
    public class AuthorizationServices : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public AuthorizationServices(IUnitOfWork unitOfWork, DataContext dataContext)
        {
            _unitOfWork = unitOfWork;
            _context = dataContext;
        }

        public bool ValidateUser(string userName, int moduleId, bool isWrite)
        {
            var user = _unitOfWork.Users.Find(x => x.Correo == userName).First();

            if (user == null)
                return false;

            var rol = _unitOfWork.UserRoles.Find(e => e.UserId == user.Id).First();
            if (rol == null)
                return false;

            foreach (var moduleRole in _context.RoleModules.Include(x => x.Module).Where(y => y.RoleId == rol.RoleId))
            {
                if (moduleRole.Module.Id == moduleId)
                {
                    return isWrite switch
                    {
                        true => moduleRole.Write,
                        false => moduleRole.Read
                    };
                }
            }

            return false;
        }

        public bool Login(string email)
        {
            var user = _unitOfWork.Users.Find(x => x.Correo == email).FirstOrDefault();
            return user != null;
        }

        public User GetUserByEmail(string email) => _context.Users.Include(x => x.UserRoles).ThenInclude(y => y.Role).ThenInclude(z => z.RoleModules).ThenInclude(w => w.Module).SingleOrDefault(x => x.Correo == email);
    }
}