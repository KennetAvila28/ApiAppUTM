using System;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using System.Threading.Tasks;

namespace AppUTM.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DataContext Context;
        private UserRepository _usersRepository;
        private RoleRepository _roleRepository;
        private PermissionRepository _permissionRepository;
        private RolePermissionRepository _rolesPermissionRepository;
        private UserRolesRepository _userRolesRepository;

        public UnitOfWork(DataContext context) => Context = context;

        public IUserRepository Users => _usersRepository ??= new UserRepository(Context);
        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(Context);
        public IPermissionRepository Permissions => _permissionRepository ??= new PermissionRepository(Context);
        public IRolePermissionRepository RolePermission => _rolesPermissionRepository ??= new RolePermissionRepository(Context);
        public IUserRolesRepository UserRoles => _userRolesRepository ??= new UserRolesRepository(Context);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() => Context.Dispose();

        public async Task<int> CommitAsync() => await Context.SaveChangesAsync();
    }
}