using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppUTM.Models.Roles;

namespace AppUTM.Extensions
{
    internal static class ModulesToRoleModuleExtensions
    {
        public static async Task<IList<RoleModule>> Convert(HttpClient http)
        {
            var permissions = await GetExtensions.GetAllModules(http);

            return permissions.Select(per => new RoleModule() { Name = per.Name, ModuleId = per.Id, }).ToList();
        }
    }
}