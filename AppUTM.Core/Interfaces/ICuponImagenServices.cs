using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface ICuponImagenServices
    {
        public Task<IEnumerable<CuponImagen>> GetCuponesImagen();
        public IEnumerable<CuponImagen> GetCuponesImagenEmpresa(int id);
        public Task<CuponImagen> GetCuponImagen(int id);
        public Task AddCuponImagen(CuponImagen cuponImagen);
        public Task UpdateCuponImagen(CuponImagen cuponImagen);
        public Task UpdateRangeCupones(IEnumerable<CuponImagen> cupones);
        public Task DeleteCuponImagen(CuponImagen cuponImagen);
    }   
}
