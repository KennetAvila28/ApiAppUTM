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
        public IRepository<Empresa> EmpresaRepository { get; }
        public IRepository<CuponGenerico> CuponGenericoRepository { get; }
        public IRepository<CuponImagen> CuponImagenRepository { get; }

        Task<int> CommitAsync();
    }
}