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
    public class CuponImagenService : ICuponImagenServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CuponImagenService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task AddCuponImagen(CuponImagen cuponImagen)
        {
            await _unitOfWork.CuponImagenRepository.Add(cuponImagen);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCuponImagen(CuponImagen cuponImagen)
        {
            _unitOfWork.CuponImagenRepository.Remove(cuponImagen);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CuponImagen>> GetCuponesImagen()
        {
            return await _unitOfWork.CuponImagenRepository.GetAll();
        }

        public IEnumerable<CuponImagen> GetCuponesImagenEmpresa(int id)
        {
            return _unitOfWork.CuponImagenRepository.Find(e => e.EmpresaId == id);
        }

        public async Task<CuponImagen> GetCuponImagen(int id)
        {
            return await _unitOfWork.CuponImagenRepository.GetById(id);
        }

        public async Task UpdateCuponImagen(CuponImagen cuponImagen)
        {
            _unitOfWork.CuponImagenRepository.Update(cuponImagen);
            await _unitOfWork.CommitAsync();
        }
    }
}