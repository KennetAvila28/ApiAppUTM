using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface ICuponGenericoServices
    {
        public Task<IEnumerable<CuponGenerico>> GetCuponesGenericos();
        public IEnumerable<CuponGenerico> GetCuponGenericosEmpresa(int id);
        public Task<CuponGenerico> GetCuponGenerico(int id);        
        public Task AddCuponGenerico(CuponGenerico cuponGenerico);
        public Task UpdateCuponGenerico(CuponGenerico cuponGenerico);
        public Task DeleteCuponGenerico(CuponGenerico cuponGenerico);
    }
}
