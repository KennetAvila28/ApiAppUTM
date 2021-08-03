using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppUTM.Client.Models.Permissions;
using AppUTM.Models.Permissions;
using AppUTM.Models.Roles;
using AppUTM.Models.Users;
using AppUTM.Responses;

namespace AppUTM.Extensions
{
    internal static class GetExtensions
    {
        public static async Task<IList<RoleReturn>> GetAllRoles(HttpClient http)
        {
            var rolesData = await http.GetFromJsonAsync<ApiResponse<IList<RoleReturn>>>("Roles");
            return rolesData?.Data;
        }

        public static async Task<IList<PermissionReturn>> GetAllPermissions(HttpClient http)
        {
            var permissionData = await http.GetFromJsonAsync<ApiResponse<IList<PermissionReturn>>>("Permissions");
            return permissionData?.Data;
        }

        public static async Task<IList<UserReturn>> GetAllUsers(HttpClient http)
        {
            var userData = await http.GetFromJsonAsync<ApiResponse<IList<UserReturn>>>("Users");
            return userData?.Data;
        }

        public static async Task<string> GetWorkers(HttpClient http, string email)
        {
            var workers = await http.GetFromJsonAsync<string>("Users/Empleado/" + email);
            return workers;
        }

        public static async Task<UserForUpdateDto> GetUserById(HttpClient http, int id)
        {
            var user = await http.GetFromJsonAsync<ApiResponse<UserForUpdateDto>>("Users/" + id);
            return user?.Data;
        }

        public static async Task<RoleForUpdateDto> GetRoleById(HttpClient http, int id)
        {
            var role = await http.GetFromJsonAsync<ApiResponse<RoleForUpdateDto>>("Roles/" + id);
            return role?.Data;
        }

        public static async Task<PermissionForUpdateDto> GetPermissionById(HttpClient http, int id)
        {
            var permission = await http.GetFromJsonAsync<ApiResponse<PermissionForUpdateDto>>("Permissions/" + id);
            return permission?.Data;
        }
    }
}