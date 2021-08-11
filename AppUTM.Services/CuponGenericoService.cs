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
    public class CuponGenericoService : ICuponGenericoServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CuponGenericoService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task AddCuponGenerico(CuponGenerico cuponGenerico)
        {
            await _unitOfWork.CuponGenericoRepository.Add(cuponGenerico);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCuponGenerico(CuponGenerico cuponGenerico)
        {
            _unitOfWork.CuponGenericoRepository.Remove(cuponGenerico);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CuponGenerico>> GetCuponesGenericos()
        {
            return await _unitOfWork.CuponGenericoRepository.GetAll();
        }

        public async Task<CuponGenerico> GetCuponGenerico(int id)
        {
            return await _unitOfWork.CuponGenericoRepository.GetById(id);
        }

        public IEnumerable<CuponGenerico> GetCuponGenericosEmpresa(int id)
        {
            return  _unitOfWork.CuponGenericoRepository.Find(e => e.EmpresaId == id);
        }

        public async Task UpdateCuponGenerico(CuponGenerico cuponGenerico)
        {
            _unitOfWork.CuponGenericoRepository.Update(cuponGenerico);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateRangeCupones(IEnumerable<CuponGenerico> cupones)
        {
            _unitOfWork.CuponGenericoRepository.UpdateRange(cupones);
            await _unitOfWork.CommitAsync();
        }
    }
}
