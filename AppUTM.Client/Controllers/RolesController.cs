using AppUTM.Api.DTOS.Permissions;
using AppUTM.Api.DTOS.Roles;
using AppUTM.Client.Models;
using AppUTM.Client.Models.Roles;
using AppUTM.Client.Responses;
using AppUTM.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _scope;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly HttpClient _httpClient;

        public RolesController(ILogger<RolesController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _httpClient = httpClient;
            _configuration = configuration;
            _scope = "user.read";
        }
        // GET: RolesController

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            var jsonUser = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles");
            var jsonPermissions = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/");
            var listPermissions = JsonConvert.DeserializeObject<ApiResponse<List<PermissionReturn>>>(jsonPermissions);
            ViewBag.Permisos = listPermissions.Data;
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonUser);
            return View(listRoles.Data);
        }

        // GET: RolesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            var jsonUser = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles/" + id);
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<RoleReturn>>(jsonUser);

            return View(listRoles.Data);
        }

        // GET: RolesController/Create

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            var jsonRol = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles");
            var jsonPermissions = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/");
            var listPermissions = JsonConvert.DeserializeObject<ApiResponse<List<PermissionReturn>>>(jsonPermissions);
            ViewBag.Permisos = listPermissions.Data;
            var crear = JsonConvert.DeserializeObject<RolesCreate>(jsonRol);
            return View(crear);
        }

        // POST: RolesController/Create
        [HttpPost]

        public IActionResult Create(RolesCreate rolesCreate, int[] permission)
        {

            HttpClient httpClient = new HttpClient();
            List<RolePermission> listaPermisos = new List<RolePermission>();
            foreach (var item in permission)
            {
                RolePermission rolePermission = new RolePermission();
                rolePermission.PermissionId = item;
                listaPermisos.Add(rolePermission);
            }
            rolesCreate.RolePermissions = listaPermisos;

            var CrearRol = httpClient.PostAsJsonAsync<RolesCreate>(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles", rolesCreate);
            CrearRol.Wait();

            var CreaResult = CrearRol.Result;
            if (CreaResult.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        // GET: RolesController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);
            var jsonRol = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles/" + id);
            //var jsonPer = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/");
            //var listpermisos = JsonConvert.DeserializeObject<ApiResponse<List<PermissionReturn>>>(jsonPer);
            var crear = JsonConvert.DeserializeObject<ApiResponse<RoleForUpdateDto>>(jsonRol);

            //foreach (var permi in crear.Data.RolePermissions)
            //{
            //    listpermisos.Data.Remove(listpermisos.Data.Single(perm => perm.Id == permi.PermissionId));

            //}

            //ViewBag.Permisos = listpermisos.Data;

            return View(crear.Data);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RoleForUpdateDto roleForUpdateDto, int[] permissionDelete, int[] permissionAdd)
        {
            HttpClient httpClient = new HttpClient();
            //List<UserRole> listaRoles = new List<UserRole>();
            if (permissionDelete != null)
            {
                roleForUpdateDto.PermissionsToBeDelete = permissionDelete;
            }

            if (permissionAdd != null)
            {
                roleForUpdateDto.RolePermissions = new List<RolePermission>();

                foreach (var item in permissionAdd)
                {
                    RolePermission rolePermission = new RolePermission();
                    rolePermission.PermissionId = item;
                    roleForUpdateDto.RolePermissions.Add(rolePermission);

                }

            }
            var updateClient = await httpClient.PutAsJsonAsync<RoleForUpdateDto>(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles/" + roleForUpdateDto.Id, roleForUpdateDto);
            if (updateClient.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        // GET: RolesController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);
            var jsonRol = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles/"+ id);
            var roles = JsonConvert.DeserializeObject<ApiResponse<RoleDelete>>(jsonRol);
            return View(roles.Data);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RoleDelete roleDelete, int id)
        {


            HttpClient httpClient = new HttpClient();

            var putTask = httpClient.DeleteAsync("http://localhost:59131/api/Roles/" + id); ;
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _scope });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<string> GetPhoto(HttpClient client)
        {
            var resp = await client.GetAsync(_configuration["photouser"]);
            var buffer = await resp.Content.ReadAsByteArrayAsync();
            var byteArray = buffer.ToArray();

            string base64String = Convert.ToBase64String(byteArray);

            return base64String;
        }
    }
}
