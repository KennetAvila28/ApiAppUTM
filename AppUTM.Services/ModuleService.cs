using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppUTM.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        // private readonly DataContext _context;

        public ModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await _unitOfWork.Module.GetAll();
        }

        public async Task<Module> GetModuleById(int id)
        {
            return await _unitOfWork.Module.GetById(id);
        }

        public async Task<Module> CreateModule(Module newModule)
        {
            await _unitOfWork.Module.Add(newModule);
            await _unitOfWork.CommitAsync();
            return newModule;
        }

        public async Task UpdateModule(Module moduleToBeUpdated, Module module)
        {
            moduleToBeUpdated.Nombre = module.Nombre;
            // moduleToBeUpdated.UpdateAt = DateTime.Now;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteModule(Module module)
        {
            // _unitOfWork.RolePermission.RemoveRange(permission.RolePermissions);
            _unitOfWork.Module.Remove(module);
            await _unitOfWork.CommitAsync();
        }
    }
}
