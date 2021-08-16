using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IRoleModuleService
    {
        Task<RoleModule> GetRoleModuleById(int moduleId, int roleId);

        Task<List<RoleModule>> CreateRoleModule(List<RoleModule> newRoleModule);

        Task UpdateRoleModule(RoleModule roleModuleToBeUpdate);

        Task DeleteRoleModule(RoleModule roleModule);

        Task<List<RoleModule>> GetRoleModulesByRoleId(int roleId);
    }
}