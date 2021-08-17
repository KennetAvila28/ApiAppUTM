using AppUTM.Api.DTOS.Modules;
using AppUTM.Api.DTOS.ModulesRoles;
using AppUTM.Client.Models;
using AppUTM.Client.Models.Roles;
using AppUTM.Client.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ModuleRoleController : Controller
    {

        
            private readonly ILogger<ModuleRoleController> _logger;
            private readonly ITokenAcquisition _tokenAcquisition;
            private readonly string _scope;
            private readonly IConfiguration _configuration;
            private readonly HttpClient _httpClient;

            public ModuleRoleController(ILogger<ModuleRoleController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
            {
                _logger = logger;
                _tokenAcquisition = tokenAcquisition;
                _httpClient = httpClient;
                _configuration = configuration;
                _scope = "user.read";
            }


            [HttpGet]
            public async Task<IActionResult> Index()
            {

                HttpClient httpClient = new HttpClient();

                    await PrepareAuthenticatedClient();
                    string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
                    ViewBag.image = await GetPhoto(_httpClient);


            var jsonModulos = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles");
            var listModulos = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonModulos);
                ViewBag.Roles = listModulos.Data;

                var jsonModuls = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "ModuleRole");

            var listaModulos = JsonConvert.DeserializeObject<ApiResponse<List<ModuleRoleReturn>>>(jsonModuls);



                return View(listaModulos.Data);
            }



            //CREATE
            [HttpGet]
            public async Task<IActionResult> CreateAccess()
            {
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);




            ViewBag.Modulos = await GetModulesAsync();
                ViewBag.Roles = await GetRolesAsync();

                return View();

            }

            [HttpPost]
            public IActionResult CreateAccess(ModuleRoleCreate modules)
            {
                HttpClient httpClient = new HttpClient();

                var putTask = httpClient.PostAsJsonAsync<ModuleRoleCreate>(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "ModuleRole", modules);
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
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            HttpClient httpClient = new HttpClient();
                var jsonModule = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "ModuleRole/" + ModuleId + "/" + RoleId);
                var permisos = JsonConvert.DeserializeObject<ApiResponse<ModuleRoleUpdate>>(jsonModule);
                return View(permisos.Data);
            }


            [HttpPost]
            public IActionResult Edit(ModuleRoleUpdate moduleToBeUpdated)
            {
                HttpClient httpClient = new HttpClient();

                var putTask = httpClient.PutAsJsonAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "ModuleRole/" + moduleToBeUpdated.ModuleId + "/" + moduleToBeUpdated.RoleId, moduleToBeUpdated); ;
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
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            HttpClient httpClient = new HttpClient();
                var jsonModule = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "ModuleRole/" + ModuleId + "/" + RoleId);
                var permisos = JsonConvert.DeserializeObject<ApiResponse<ModuleRoleDelete>>(jsonModule);
                return View(permisos.Data);
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(ModuleRoleDelete moduleRoleDelete, int ModuleId, int RoleId)
            {


                HttpClient httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
                var putTask = httpClient.DeleteAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "ModuleRole/"+ ModuleId + "/" + RoleId);

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


