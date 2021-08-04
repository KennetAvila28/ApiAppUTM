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
        private EventFavoriteRepository _eventFavoriteRepository;
        private EventRepository _eventRepository;
        private FavoriteRepository _favoriteRepository;
        private CoordinationRepository _coordinationRepository;

        public UnitOfWork(DataContext context) => Context = context;

        public IUserRepository Users => _usersRepository ??= new UserRepository(Context);
        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(Context);
        public IPermissionRepository Permissions => _permissionRepository ??= new PermissionRepository(Context);
        public IRolePermissionRepository RolePermission => _rolesPermissionRepository ??= new RolePermissionRepository(Context);
        public IUserRolesRepository UserRoles => _userRolesRepository ??= new UserRolesRepository(Context);
        public IEventFavoriteRepository EventFavorite => _eventFavoriteRepository ??= new EventFavoriteRepository(Context);
        public IEventRepository Events => _eventRepository ??= new EventRepository(Context);
        public IFavoriteRepository Favorites => _favoriteRepository ??= new FavoriteRepository(Context);
        public ICoordinationsRepository Coordinations => _coordinationRepository ??= new CoordinationRepository(Context);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() => Context.Dispose();

        public async Task<int> CommitAsync() => await Context.SaveChangesAsync();
    }
}