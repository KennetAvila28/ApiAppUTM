using AppUTM.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppUTM.Data;
using AppUTM.Core.Interfaces;

namespace AppUTM.Services
{
   public class AuthorizationServices : IAuthorizationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;
        

        public AuthorizationServices(IUnitOfWork unitOfWork, DataContext dataContext)
        {
            _unitOfWork = unitOfWork;
            _context = dataContext;
        }


        public bool ValidateUser(string UserName, string moduleName, bool isWrite)
        {
            //var user = _unitOfWork.Users.Find(x => x.Correo == UserName).First();
            var user = _context.Users.Where(x => x.Correo == UserName).First();

            if(user == null)
            {
                return false;
            }


            //var rol = _unitOfWork.UserRoles.Find(e => e.UserId == user.Id).First();
            var rol = _context.UserRoles.Where(e => e.UserId == user.Id).First();
            if(rol == null)
            {
                return false;
            }


            //var moduleRoleQuery = _unitOfWork.ModuleRoles.Find(y => y.RoleId == rol.RoleId);
            var moduleRoleQuery = _context.ModuleRoles.Include(x=>x.Module).Where(y => y.RoleId == rol.RoleId);
            foreach(var moduleRole in moduleRoleQuery)
            {
                if(moduleRole.Module.Nombre == moduleName)
                {
                    if(isWrite)
                    {
                        return moduleRole.Escritura;

                    }
                    if(!isWrite)
                    {
                        return moduleRole.Lectura;
                    }
                }
            }

            return false;
        }
    }
}
