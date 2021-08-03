using AppUTM.Api.DTOS.Permissions;
using AppUTM.Client.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class PermissionsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            var jsonPermisos = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/");

            var listPermisos = JsonConvert.DeserializeObject<ApiResponse<List<PermissionReturn>>>(jsonPermisos);

            return View(listPermisos.Data);
        }



        //CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/");
            var permiso = JsonConvert.DeserializeObject<PermissionCreate>(json);
            return View(permiso);
        }

        [HttpPost]
        public IActionResult Create(PermissionCreate permissions)
        {
            HttpClient httpClient = new HttpClient();

            var putTask = httpClient.PostAsJsonAsync<PermissionCreate>("http://localhost:59131/api/Permissions/", permissions);
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
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Permissions/" + id);
            var permisos = JsonConvert.DeserializeObject<ApiResponse<PermissionForUpdateDto>>(json);
            return View(permisos.Data);
        }

        [HttpPost]
        public IActionResult Edit(PermissionForUpdateDto permissions)
        {
            HttpClient httpClient = new HttpClient();

            var putTask = httpClient.PutAsJsonAsync<PermissionForUpdateDto>("http://localhost:59131/api/Permissions/" + permissions.Id, permissions);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

    }
}

