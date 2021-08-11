using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IHistorialCuponesServices
    {
        public Task<IEnumerable<HistorialCupones>> GetHistorialCupones();
        public Task<HistorialCupones> GetHistorialCupon(int id);
        public Task AddHistorialCupon(HistorialCupones registro);
        public Task UpdateHistorialCupon(HistorialCupones registro);
    }
}
