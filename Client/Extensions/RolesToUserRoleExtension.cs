using AppUTM.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppUTM.Extensions
{
    internal static class RolesToUserRoleExtension
    {
        public static async Task<IList<UserRole>> Convert(HttpClient http)
        {
            var roles = await GetExtensions.GetAllRoles(http);
            return roles.Select(role => new UserRole { Nombre = role.Nombre, RoleId = role.Id }).ToList();
        }
    }
}