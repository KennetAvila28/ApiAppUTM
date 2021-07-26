#region Using

using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion Using

namespace AppUTM.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> CreateRole(Role newRole)
        {
            await _unitOfWork.Roles.Add(newRole);
            await _unitOfWork.CommitAsync();
            return newRole;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _unitOfWork.Roles.GetAll();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _unitOfWork.Roles.GetById(id);
        }

        public async Task UpdateRole(Role RoleToBeUpdated, Role Role)
        {
            RoleToBeUpdated.Nombre = Role.Nombre;
            RoleToBeUpdated.UpdateAt = DateTime.Now;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRole(Role Role)
        {
            _unitOfWork.Roles.Remove(Role);
            await _unitOfWork.CommitAsync();
        }
    }
}