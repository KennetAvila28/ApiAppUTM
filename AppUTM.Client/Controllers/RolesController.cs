using AppUTM.Api.DTOS.Permissions;
using AppUTM.Api.DTOS.Roles;
using AppUTM.Client.Models.Roles;
using AppUTM.Client.Responses;
using AppUTM.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class RolesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisicion;

        public RolesController(IConfiguration configuration, ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _tokenAcquisicion = tokenAcquisition;
        }
        // GET: RolesController
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Roles");
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
            var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/" + id);
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<RoleReturn>>(jsonUser);

            return View(listRoles.Data);
        }

        // GET: RolesController/Create

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var jsonPermissions = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/");
            var listPermissions = JsonConvert.DeserializeObject<ApiResponse<List<PermissionReturn>>>(jsonPermissions);
            ViewBag.Permisos = listPermissions.Data;
            var crear = JsonConvert.DeserializeObject<RolesCreate>(json);
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

            var CrearRol = httpClient.PostAsJsonAsync<RolesCreate>("http://localhost:59131/api/Roles", rolesCreate);
            CrearRol.Wait();

            var CreaResult = CrearRol.Result;
            if (CreaResult.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        // GET: RolesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/" + id);
            //var jsonPer = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/");
            //var listpermisos = JsonConvert.DeserializeObject<ApiResponse<List<PermissionReturn>>>(jsonPer);
            var crear = JsonConvert.DeserializeObject<ApiResponse<RoleForUpdateDto>>(json);

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
            var updateClient = await httpClient.PutAsJsonAsync<RoleForUpdateDto>("http://localhost:59131/api/Roles/" + roleForUpdateDto.Id, roleForUpdateDto);
            if (updateClient.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        // GET: RolesController/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/" + id);
            var roles = JsonConvert.DeserializeObject<ApiResponse<RoleDelete>>(json);
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
    }
}
