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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Console.WriteLine(User.Identity.Name);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);



            var jsonModuls = await httpClient.GetStringAsync("http://localhost:59131/api/Module/");

            var listModulos = JsonConvert.DeserializeObject<ApiResponse<List<ModuleReturn>>>(jsonModuls);

            return View(listModulos.Data);
        }



        //CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);

            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Module/");
            var permiso = JsonConvert.DeserializeObject<ModuleCreate>(json);
            return View(permiso);
        }

        [HttpPost]
        public IActionResult Create(ModuleCreate modules)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);

            var putTask = httpClient.PostAsJsonAsync<ModuleCreate>("http://localhost:59131/api/Module/", modules);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }


        //UPDATE 
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);

            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Module/" + id);
            var permisos = JsonConvert.DeserializeObject<ApiResponse<moduleToBeUpdated>>(json);
            return View(permisos.Data);
        }

        [HttpPost]
        public IActionResult Edit(moduleToBeUpdated moduleToBeUpdated)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);


            var putTask = httpClient.PutAsJsonAsync<moduleToBeUpdated>("http://localhost:59131/api/Module/" + moduleToBeUpdated.Id, moduleToBeUpdated);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }



        public async Task<ActionResult> Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Module/" + id);

            var modules = JsonConvert.DeserializeObject<ApiResponse<ModuleDelete>>(json);
            return View(modules.Data);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ModuleDelete moduleDelete, int id)
        {


            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            var putTask = httpClient.DeleteAsync("http://localhost:59131/api/Module/" + id); ;

            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();

        }
    }
}