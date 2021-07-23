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

        public UnitOfWork(DataContext context)
        {
            Context = context;
        }

        public IUserRepository Users => _usersRepository ??= new UserRepository(Context);
        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(Context);
        public IPermissionRepository Permissions => _permissionRepository ??= new PermissionRepository(Context);

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}