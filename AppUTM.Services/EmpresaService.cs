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
    public class EmpresaService : IEmpresaServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmpresaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddEmpresa(Empresa empresa)
        {
            await _unitOfWork.EmpresaRepository.Add(empresa);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEmpresa(Empresa empresa)
        {
            _unitOfWork.EmpresaRepository.Remove(empresa);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Empresa> GetEmpresa(int id)
        {
            return await _unitOfWork.EmpresaRepository.GetById(id);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresas()
        {
            return await _unitOfWork.EmpresaRepository.GetAll();
        }

        public async Task UpdateEmpresa(Empresa empresa)
        {
            _unitOfWork.EmpresaRepository.Update(empresa);
            await _unitOfWork.CommitAsync();
        }
    }
}
