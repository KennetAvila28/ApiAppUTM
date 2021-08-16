using System;
using System.Threading.Tasks;

namespace AppUTM.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IModuleRepository Modules { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        IRoleModuleRepository RoleModule { get; }
        IUserRolesRepository UserRoles { get; }
        IEventRepository Events { get; }
        IFavoriteRepository Favorites { get; }
        ICoordinationsRepository Coordinations { get; }

        Task<int> CommitAsync();
    }
}