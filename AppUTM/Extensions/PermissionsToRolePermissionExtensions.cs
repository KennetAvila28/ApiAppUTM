using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppUTM.Models.Roles;

namespace AppUTM.Extensions
{
    internal static class PermissionsToRolePermissionExtensions
    {
        public static async Task<IList<RolePermission>> Convert(HttpClient http)
        {
            var permissions = await GetExtensions.GetAllPermissions(http);

            return permissions.Select(per => new RolePermission() { Module = per.Module, PermissionId = per.Id, }).ToList();
        }
    }
}