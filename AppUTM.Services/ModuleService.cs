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
        private readonly DataContext _context;

        public ModuleService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Module> CreateModule(Module newModule)
        {
            await _unitOfWork.Module.Add(newModule);
            await _unitOfWork.ModuleRoles.AddRange(newModule.ModuleRoles);
            await _unitOfWork.CommitAsync();
            return newModule;
        }

        /// <summary>Return all users</summary>
        /// <returns> <c>Array</c> of users</returns>
        public async Task<IEnumerable<Module>> GetAllModules() => await _context.Modules.Include(x => x.ModuleRoles).ToListAsync();

        public async Task<Module> GetModuleById(int id) =>
            await _context.Modules.Include(x => x.ModuleRoles).ThenInclude(y => y.Role)
                .SingleOrDefaultAsync(z => z.Id == id);

        public async Task UpdateModule(Module moduleToBeUpdated, Module module)
        {
            moduleToBeUpdated.Nombre = module.Nombre;
            
            foreach (var u in module.ModuleRoles)
            {
                if (!moduleToBeUpdated.ModuleRoles.Any(us => us.RoleId == u.RoleId))
                {
                    u.ModuleId = moduleToBeUpdated.Id;
                    await _unitOfWork.ModuleRoles.Add(u);
                }
            }
            foreach (var item in module.RolesToBeDelete)
            {
                foreach (var role in moduleToBeUpdated.ModuleRoles.Where(role => role.RoleId == item))
                {
                    _unitOfWork.ModuleRoles.Remove(role);
                }
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteModule(Module module)
        {
            _unitOfWork.ModuleRoles.RemoveRange(module.ModuleRoles);
            _unitOfWork.Module.Remove(module);
            await _unitOfWork.CommitAsync();
        }
    }
}
