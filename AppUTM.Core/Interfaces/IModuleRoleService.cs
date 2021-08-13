using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IModuleRoleService
    {

        Task<IEnumerable<ModuleRole>> GetAllRoles();
        Task<IEnumerable<ModuleRole>> GetAllModulesRoles();

        Task<ModuleRole> GetModuleRoleById(int ModuleId, int RoleId);

        Task<ModuleRole> CreateModuleRole(ModuleRole newModuleRole);

        Task UpdateModuleRole(ModuleRole ModuleRoleToBeUpdated, ModuleRole moduleRole);

        Task DeleteModuleRole(ModuleRole moduleRoleToBeDelete);
    }
}
