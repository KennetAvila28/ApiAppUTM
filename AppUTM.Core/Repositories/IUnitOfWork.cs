using System;
using System.Threading.Tasks;

namespace AppUTM.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }

        Task<int> CommitAsync();
    }
}