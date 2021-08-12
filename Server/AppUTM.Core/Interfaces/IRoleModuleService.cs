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
        Task<IEnumerable<RoleModule>> GetAllRoleModule();

        Task<RoleModule> GetRoleModuleById(int moduleId, int roleId);

        Task<RoleModule> CreateRoleModule(RoleModule newRoleModule);

        Task DeleteRoleModule(RoleModule roleModule);
    }
}