using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppUTM.Core.Repositories;

namespace AppUTM.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await _unitOfWork.Modules.GetAll();
        }

        public async Task<Module> GetModuleById(int id)
        {
            return await _unitOfWork.Modules.GetById(id);
        }

        public async Task<Module> CreateModule(Module newModule)
        {
            await _unitOfWork.Modules.Add(newModule);
            await _unitOfWork.CommitAsync();
            return newModule;
        }

        public async Task UpdateModule(Module ModuleToBeUpdated)
        {
            ModuleToBeUpdated.UpdateAt = DateTime.Now;
            _unitOfWork.Modules.Update(ModuleToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteModule(Module Module)
        {
            _unitOfWork.RoleModule.RemoveRange(Module.RoleModules);
            _unitOfWork.Modules.Remove(Module);
            await _unitOfWork.CommitAsync();
        }
    }
}