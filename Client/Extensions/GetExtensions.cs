using System;
using AppUTM.Models.Coordinations;
using AppUTM.Models.Events;
using AppUTM.Models.Modules;
using AppUTM.Models.RoleModules;
using AppUTM.Models.Roles;
using AppUTM.Models.Users;
using AppUTM.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace AppUTM.Extensions
{
    internal class GetExtensions
    {
        public static async Task<IList<RoleReturn>> GetAllRoles(HttpClient http)
        {
            var rolesData = await http.GetFromJsonAsync<ApiResponse<IList<RoleReturn>>>("Roles");
            return rolesData?.Data;
        }

        public static async Task<IList<ModuleReturn>> GetAllModules(HttpClient http)
        {
            var moduleData = await http.GetFromJsonAsync<ApiResponse<IList<ModuleReturn>>>("Module");
            return moduleData?.Data;
        }

        public static async Task<IList<UserReturn>> GetAllUsers(HttpClient http)
        {
            var userData = await http.GetFromJsonAsync<ApiResponse<IList<UserReturn>>>("Users");
            return userData?.Data;
        }

        public static async Task<string> GetWorkers(HttpClient http, string email)
        {
            var workers = await http.GetFromJsonAsync<string>("Users/worker/" + email);
            return workers;
        }

        public static async Task<UserReturn> GetUserByEmail(HttpClient http, string email)
        {
            var user = await http.GetFromJsonAsync<ApiResponse<UserReturn>>("Users/ByEmail/" + email);
            return user?.Data;
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

        public static async Task<ModuleForUpdateDto> GetModuleById(HttpClient http, int id)
        {
            var moduleData = await http.GetFromJsonAsync<ApiResponse<ModuleForUpdateDto>>("Module/" + id);
            return moduleData?.Data;
        }

        public static async Task<IEnumerable<EventReturn>> GetAllEvents(HttpClient http)
        {
            var events = await http.GetFromJsonAsync<ApiResponse<IEnumerable<EventReturn>>>("Event");
            return events?.Data;
        }

        public static async Task<IEnumerable<EventReturn>> GetAllEventsPublished(HttpClient http)
        {
            var events = await http.GetFromJsonAsync<ApiResponse<IEnumerable<EventReturn>>>("Event/GetPublished");
            return events?.Data;
        }

        public static async Task<IEnumerable<EventReturn>> GetAllEventsRechazed(HttpClient http)
        {
            var events = await http.GetFromJsonAsync<ApiResponse<IEnumerable<EventReturn>>>("Event/GetRechazed");
            return events?.Data;
        }

        public static async Task<IEnumerable<EventReturn>> GetAllEventsPassed(HttpClient http)
        {
            var events = await http.GetFromJsonAsync<ApiResponse<IEnumerable<EventReturn>>>("Event/GetPassed");
            return events?.Data;
        }

        public static async Task<IEnumerable<EventReturn>> GetAllEventsRevised(HttpClient http)
        {
            var events = await http.GetFromJsonAsync<ApiResponse<IEnumerable<EventReturn>>>("Event/GetRevised");
            return events?.Data;
        }

        public static async Task<EventForUpdateDto> GetEventById(HttpClient http, int id)
        {
            var events = await http.GetFromJsonAsync<ApiResponse<EventForUpdateDto>>("Event/" + id);
            return events?.Data;
        }

        public static async Task<IEnumerable<CoordinationReturn>> GetAllCoordinations(HttpClient http)
        {
            var coordinations = await http.GetFromJsonAsync<ApiResponse<IEnumerable<CoordinationReturn>>>("Coordinations");
            return coordinations?.Data;
        }

        public static async Task<CoordinationForUpdateDto> GetCoordinationById(HttpClient http, int id)
        {
            var coordination = await http.GetFromJsonAsync<ApiResponse<CoordinationForUpdateDto>>("Coordinations/" + id);
            return coordination?.Data;
        }

        public static async Task<IList<RoleModuleForUpdateDto>> GetRoleModuleById(HttpClient http, int moduleId, int roleId)
        {
            var roleModules = await http.GetFromJsonAsync<ApiResponse<IList<RoleModuleForUpdateDto>>>("RoleModule/" + moduleId + "/" + roleId);
            return roleModules?.Data;
        }

        public static async Task<IList<RoleModuleReturn>> GetRoleModuleByRoleId(HttpClient http, int id)
        {
            var roleModule = await http.GetFromJsonAsync<ApiResponse<IList<RoleModuleReturn>>>("RoleModule/" + id);
            return roleModule?.Data;
        }
    }
}