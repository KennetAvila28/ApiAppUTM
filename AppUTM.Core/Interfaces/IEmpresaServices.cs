using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IEmpresaServices
    {
        public Task<IEnumerable<Empresa>> GetEmpresas();
        public Task<Empresa> GetEmpresa(int id);
        public Task AddEmpresa(Empresa empresa);
        public Task UpdateEmpresa(Empresa empresa);
        public Task DeleteEmpresa(Empresa empresa);
    }
}
