using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Core.Interfaces
{
    public interface IRoleModuleService
    {
        Task<RoleModule> GetRoleModuleById(int moduleId, int roleId);

        Task<List<RoleModule>> CreateRoleModule(List<RoleModule> newRoleModule);

        Task UpdateRoleModule(List<RoleModule> roleModuleToBeUpdate);

        Task DeleteRoleModule(RoleModule roleModule);

        Task<List<RoleModule>> GetRoleModulesByRoleId(int roleId);
    }
}