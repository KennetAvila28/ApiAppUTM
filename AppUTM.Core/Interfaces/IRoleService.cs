using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();

        Task<Role> GetRoleById(int id);

        Task<Role> CreateRole(Role newRole);

        Task UpdateRole(Role RoleToBeUpdated, Role Role);

        Task DeleteRole(Role Role);
    }
}