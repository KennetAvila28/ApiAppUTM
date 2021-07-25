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
        private IRepository<Empresa> _empresaRepository;
        private IRepository<CuponGenerico> _cuponGenerico;
        private IRepository<CuponImagen> _cuponImagen;

        public UnitOfWork(DataContext context)
        {
            Context = context;
        }

        public IUserRepository Users => _usersRepository ??= new UserRepository(Context);
        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(Context);
        public IPermissionRepository Permissions => _permissionRepository ??= new PermissionRepository(Context);
        public IRepository<Empresa> EmpresaRepository => _empresaRepository ?? new Repository<Empresa>(Context);
        public IRepository<CuponGenerico> CuponGenericoRepository => _cuponGenerico ?? new Repository<CuponGenerico>(Context);
        public IRepository<CuponImagen> CuponImagenRepository => _cuponImagen ?? new Repository<CuponImagen>(Context);

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