﻿using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetAllPermissions();

        Task<Permission> GetPermissionById(int id);

        Task<Permission> CreatePermission(Permission newPermission);

        Task UpdatePermission(Permission permissionToBeUpdated, Permission permission);

        Task DeletePermission(Permission permission);
    }
}