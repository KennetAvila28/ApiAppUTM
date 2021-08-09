using AppUTM.Api.DTOS.Modules;
using AppUTM.Client.Models.Roles;
using AppUTM.Client.Responses;
using AppUTM.Core.Models;
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
    public class ModuleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisicion;

        public ModuleController(IConfiguration configuration, ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _tokenAcquisicion = tokenAcquisition;
        }
        //_Configuration["CouponAdmin:CouponAdminBaseAddress"] + ""
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            var jsonModule = await httpClient.GetStringAsync("http://localhost:59131/api/Module");
            var jsonRoles = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonRoles);
            ViewBag.Roles = listRoles.Data;
            var listModule = JsonConvert.DeserializeObject<ApiResponse<List<ModuleReturn>>>(jsonModule);
            return View(listModule.Data);
        }
        // GET: UsuarioController

        // GET: UsuarioController/Details/5
        public async Task<ActionResult> Detalles(int id)
        {
            HttpClient httpClient = new HttpClient();
            var jsonModule = await httpClient.GetStringAsync("http://localhost:59131/api/Module/" + id);
            var listModule = JsonConvert.DeserializeObject<ApiResponse<ModuleReturn>>(jsonModule);
            return View(listModule.Data);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Module/");
            var jsonRoles = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonRoles);
            ViewBag.Roles = listRoles.Data;
            var crear = JsonConvert.DeserializeObject<ModuleCreate>(json);
            return View(crear);
        }

        [HttpPost]
        public IActionResult Create(ModuleCreate moduleCreate, int[] role)
        {
            HttpClient httpClient = new HttpClient();
            List<ModuleRole> listaRoles = new List<ModuleRole>();
            foreach (var item in role)
            {
                ModuleRole moduleRole = new ModuleRole();
                moduleRole.RoleId = item;
                listaRoles.Add(moduleRole);
            }
            moduleCreate.ModuleRoles = listaRoles;

            var CrearModulo = httpClient.PostAsJsonAsync<ModuleCreate>("http://localhost:59131/api/Module", moduleCreate);
            CrearModulo.Wait();

            var CreaResult = CrearModulo.Result;
            if (CreaResult.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }


        // GET: UsuarioController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Module/" + id);
            var jsonRoles = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonRoles);
            var crear = JsonConvert.DeserializeObject<ApiResponse<moduleToBeUpdated>>(json);

            foreach (var rol in crear.Data.ModuleRoles)
            {
                listRoles.Data.Remove(listRoles.Data.Single(r => rol.RoleId == r.Id));

            }

            ViewBag.Roles = listRoles.Data;

            return View(crear.Data);

        }

        // POST: UsuarioController/Edit/5
        [HttpPost]

        public async Task<ActionResult> Edit(int id, moduleToBeUpdated moduleToBeUpdated, int[] roleDelete, int[] roleAdd)
        {
            HttpClient httpClient = new HttpClient();
            //List<UserRole> listaRoles = new List<UserRole>();
            if (roleDelete != null)
            {
                moduleToBeUpdated.RolesToBeDelete = roleDelete;
            }

            if (roleAdd != null)
            {
                moduleToBeUpdated.ModuleRoles = new List<ModuleRole>();

                foreach (var item in roleAdd)
                {
                    ModuleRole moduleRole = new ModuleRole();
                    moduleRole.RoleId = item;
                    moduleToBeUpdated.ModuleRoles.Add(moduleRole);

                }

            }
            var updateModule = await httpClient.PutAsJsonAsync<moduleToBeUpdated>("http://localhost:59131/api/Module/" + moduleToBeUpdated.Id, moduleToBeUpdated);
            if (updateModule.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        // GET: UsuarioController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: UsuarioController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}