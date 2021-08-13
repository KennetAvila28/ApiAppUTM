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
   public class ModuleRoleService : IModuleRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;


        public ModuleRoleService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<ModuleRole>> GetAllRoles()
        {
            return await _unitOfWork.ModuleRoles.GetAll();
        }

        public async Task<IEnumerable<ModuleRole>> GetAllModulesRoles()
        {
            return await _unitOfWork.ModuleRoles.GetAll();
        }

        public async Task<ModuleRole> GetModuleRoleById(int ModuleId, int RoleId)
        {
            return await _context.ModuleRoles.SingleOrDefaultAsync(e=> e.RoleId.Equals(RoleId) && e.ModuleId.Equals(ModuleId));
        }

        public async Task<ModuleRole> CreateModuleRole(ModuleRole newModuleRole)
        {

            //var contador = _unitOfWork.ModuleRoles.Find(x => x.ModuleRoleId >= 0).OrderByDescending(x => x.ModuleRoleId);
            //var x1 = contador.First();
            //newModuleRole.ModuleRoleId = x1.ModuleRoleId + 1;
            await _unitOfWork.ModuleRoles.Add(newModuleRole);
            await _unitOfWork.CommitAsync();
            return newModuleRole;
        }

        public async Task UpdateModuleRole(ModuleRole moduleRoleToBeUpdated, ModuleRole moduleRole)
        {
       
            moduleRoleToBeUpdated.Lectura = moduleRole.Lectura;
            moduleRoleToBeUpdated.Escritura = moduleRole.Escritura;

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteModuleRole(ModuleRole moduleRoleToBeDeleted)
        {
            //_unitOfWork.ModuleRoles.RemoveRange();

            _unitOfWork.ModuleRoles.Remove(moduleRoleToBeDeleted);
            await _unitOfWork.CommitAsync();
        }
       
    }
}
