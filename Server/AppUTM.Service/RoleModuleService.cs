using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Services
{
    public class RoleModuleService : IRoleModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public RoleModuleService(DataContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoleModule>> GetRoleModulesByRoleId(int roleId) => await _context.RoleModules.Include(y => y.Module).Where(x => roleId.Equals(x.RoleId)).ToListAsync();

        public async Task<RoleModule> GetRoleModuleById(int moduleId, int roleId) => await _context.RoleModules.Include(y => y.Module).SingleOrDefaultAsync(x => x.RoleId.Equals(roleId) & x.ModuleId.Equals(moduleId));

        public async Task<List<RoleModule>> CreateRoleModule(List<RoleModule> newRoleModule)
        {
            await _unitOfWork.RoleModule.AddRange(newRoleModule);
            await _unitOfWork.CommitAsync();
            return newRoleModule;
        }

        public async Task UpdateRoleModule(List<RoleModule> roleModuleToBeUpdate)
        {
            _context.RoleModules.UpdateRange(roleModuleToBeUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRoleModule(RoleModule roleModule)
        {
            _unitOfWork.RoleModule.Remove(roleModule);
            await _unitOfWork.CommitAsync();
        }
    }
}