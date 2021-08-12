using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;

namespace AppUTM.Services
{
    //TODO: add IUnitOfWork
    public class RoleModuleService : IRoleModuleService
    {
        public Task<IEnumerable<RoleModule>> GetAllRoleModule()
        {
            throw new NotImplementedException();
        }

        public Task<RoleModule> GetRoleModuleById(int moduleId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<RoleModule> CreateRoleModule(RoleModule newRoleModule)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleModule(RoleModule roleModule)
        {
            throw new NotImplementedException();
        }
    }
}