using AppUTM.Core.Models;
using System;
using System.Threading.Tasks;

namespace AppUTM.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        IRolePermissionRepository RolePermission { get; }
        IUserRolesRepository UserRoles { get; }

        Task<int> CommitAsync();
    }
}