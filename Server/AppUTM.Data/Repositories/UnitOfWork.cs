using AppUTM.Core.Repositories;
using System.Threading.Tasks;

namespace AppUTM.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DataContext Context;
        private UserRepository _usersRepository;
        private RoleRepository _roleRepository;
        private ModuleRepository _ModuleRepository;
        private RoleModuleRepository _rolesModuleRepository;
        private UserRolesRepository _userRolesRepository;
        private EventRepository _eventRepository;
        private FavoriteRepository _favoriteRepository;
        private CoordinationRepository _coordinationRepository;

        public UnitOfWork(DataContext context) => Context = context;

        public IUserRepository Users => _usersRepository ??= new UserRepository(Context);
        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(Context);
        public IModuleRepository Modules => _ModuleRepository ??= new ModuleRepository(Context);
        public IRoleModuleRepository RoleModule => _rolesModuleRepository ??= new RoleModuleRepository(Context);
        public IUserRolesRepository UserRoles => _userRolesRepository ??= new UserRolesRepository(Context);
        public IEventRepository Events => _eventRepository ??= new EventRepository(Context);
        public IFavoriteRepository Favorites => _favoriteRepository ??= new FavoriteRepository(Context);
        public ICoordinationsRepository Coordinations => _coordinationRepository ??= new CoordinationRepository(Context);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() => Context.Dispose();

        public async Task<int> CommitAsync() => await Context.SaveChangesAsync();
    }
}