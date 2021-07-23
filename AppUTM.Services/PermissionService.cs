using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Services
{
    public class PermissionService : IPermissionService
    {
        //todo:Implement unitofwork
        public Task<IEnumerable<Permission>> GetAllPermissions()
        {
            throw new NotImplementedException();
        }

        public Task<Permission> GetPermissionById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> CreatePermission(Permission newPermission)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePermission(Permission PermissionToBeUpdated, Permission Permission)
        {
            throw new NotImplementedException();
        }

        public Task DeletePermission(Permission Permission)
        {
            throw new NotImplementedException();
        }
    }
}