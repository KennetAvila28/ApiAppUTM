using AppUTM.Api.DTOS.Modules;
using AppUTM.Api.DTOS.ModulesRoles;
using AppUTM.Client.Models.Roles;
using AppUTM.Client.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class ModuleRoleController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            HttpClient httpClient = new HttpClient();

            var jsonModulos = await httpClient.GetStringAsync("http://localhost:59131/api/Roles");
            var listModulos = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonModulos);
            ViewBag.Roles = listModulos.Data;

            var jsonModuls = await httpClient.GetStringAsync("http://localhost:59131/api/ModuleRole/");

            var listaModulos = JsonConvert.DeserializeObject<ApiResponse<List<ModuleRoleReturn>>>(jsonModuls);

            

            return View(listaModulos.Data);
        }



        //CREATE
        [HttpGet]
        public async Task<IActionResult> CreateAccess()
        {
            ViewBag.Modulos = await GetModulesAsync();
            ViewBag.Roles = await GetRolesAsync();

            return View();
            
        }

        [HttpPost]
        public IActionResult CreateAccess(ModuleRoleCreate modules)
        {
            HttpClient httpClient = new HttpClient();

            var putTask = httpClient.PostAsJsonAsync<ModuleRoleCreate>("http://localhost:59131/api/ModuleRole/", modules);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }


        //UPDATE 
        [HttpGet]
        public async Task<ActionResult> Edit(int ModuleId, int RoleId)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/ModuleRole/" + ModuleId + "/" + RoleId);
            var permisos = JsonConvert.DeserializeObject<ApiResponse<ModuleRoleUpdate>>(json);
            return View(permisos.Data);
        }

        
        [HttpPost]
        public IActionResult Edit(ModuleRoleUpdate moduleToBeUpdated)
        {
            HttpClient httpClient = new HttpClient();

            var putTask = httpClient.PutAsJsonAsync("http://localhost:59131/api/ModuleRole/" + moduleToBeUpdated.ModuleId +"/"+moduleToBeUpdated.RoleId, moduleToBeUpdated); ;
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        //Delete
        [HttpGet]
        public async Task<ActionResult> Delete(int ModuleId, int RoleId)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/ModuleRole/" + ModuleId + "/" + RoleId);
            var permisos = JsonConvert.DeserializeObject<ApiResponse<ModuleRoleReturn>>(json);
            return View(permisos.Data);
        }
        


        [HttpDelete]
        public ActionResult Delete(ModuleRoleDelete moduleDelete, int ModuleId, int RoleId)
        {


            HttpClient httpClient = new HttpClient();

            var putTask = httpClient.DeleteAsync("http://localhost:59131/api/ModuleRole/" + ModuleId + "/" + RoleId);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();

        }


        //[HttpPost]
        //public IActionResult Delete(ModuleRoleReturn moduleToBeDelete, int ModuleId, int RoleId)
        //{
        //    HttpClient httpClient = new HttpClient();

        //    var putTask = httpClient.DeleteAsync("http://localhost:59131/api/ModuleRole/"+moduleToBeDelete.ModuleId+"/"+moduleToBeDelete.RoleId); 
        //    putTask.Wait();
        //    var result = putTask.Result;
        //    if (result.IsSuccessStatusCode)
        //        return RedirectToAction("Index");
        //    else
        //        return this.BadRequest();
        //}



        public async Task<List<SelectListItem>> GetRolesAsync()

        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);

            var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Roles");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonUser);

            List<SelectListItem> roles = listRoles.Data.ConvertAll(P =>

            {

                return new SelectListItem()

                {

                    Text = P.Nombre, 

                    Value = P.Id.ToString(),

                    Selected = false

                };

            });

            return roles;

        }



        public async Task<List<SelectListItem>> GetModulesAsync()

        {
            HttpClient httpClient = new HttpClient();
            
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            var jsonModulos = await httpClient.GetStringAsync("http://localhost:59131/api/Module");
            var listModulos = JsonConvert.DeserializeObject<ApiResponse<List<ModuleReturn>>>(jsonModulos);

            List<SelectListItem> modulos = listModulos.Data.ConvertAll(P =>

            {

                return new SelectListItem()

                {

                    Text = P.Nombre,

                    Value = P.Id.ToString(),

                    Selected = false

                };

            });

            return modulos;

        }

    }
}
