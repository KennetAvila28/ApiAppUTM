using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Services
{
    public class HistorialCuponesService : IHistorialCuponesServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HistorialCuponesService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task AddHistorialCupon(HistorialCupones registro)
        {
            await _unitOfWork.HistorialCuponesRepository.Add(registro);
            await _unitOfWork.CommitAsync();
        }

        public async Task<HistorialCupones> GetHistorialCupon(int id)
        {
            return await _unitOfWork.HistorialCuponesRepository.GetById(id);
        }

        public async Task<IEnumerable<HistorialCupones>> GetHistorialCupones()
        {
            return await _unitOfWork.HistorialCuponesRepository.GetAll();
        }

        public async Task UpdateHistorialCupon(HistorialCupones registro)
        {
            _unitOfWork.HistorialCuponesRepository.Update(registro);
            await _unitOfWork.CommitAsync();
        }
    }
}
