using AppUTM.Api.DTOS.Modules;
using AppUTM.Client.Models;
using AppUTM.Client.Models.Roles;
using AppUTM.Client.Responses;
using AppUTM.Core.Models;
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
    public class ModuleController : Controller
    {

        private readonly ILogger<ModuleController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string _scope;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ModuleController(ILogger<ModuleController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
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
            Console.WriteLine(User.Identity.Name);
            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);



            var jsonModuls = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module");

            var listModulos = JsonConvert.DeserializeObject<ApiResponse<List<ModuleReturn>>>(jsonModuls);

            return View(listModulos.Data);
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);


            var jsonUser = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module/" + id);
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<ModuleReturn>>(jsonUser);

            return View(listRoles.Data);
        }

        //CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            var jsonMod = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module");
            var permiso = JsonConvert.DeserializeObject<ModuleCreate>(jsonMod);
            return View(permiso);
        }

        [HttpPost]
        public IActionResult Create(ModuleCreate modules)
        {
            HttpClient httpClient = new HttpClient();
            // httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            

            var putTask = httpClient.PostAsJsonAsync<ModuleCreate>(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module", modules);
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
            //httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            var jsonMod = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module/" + id);
            var permisos = JsonConvert.DeserializeObject<ApiResponse<moduleToBeUpdated>>(jsonMod);
            return View(permisos.Data);
        }

        [HttpPost]
        public IActionResult Edit(moduleToBeUpdated moduleToBeUpdated)
        {
            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);


            var putTask = httpClient.PutAsJsonAsync<moduleToBeUpdated>(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module/" + moduleToBeUpdated.Id, moduleToBeUpdated);
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
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            // httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            var jsonMod = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module/" + id);

            var modules = JsonConvert.DeserializeObject<ApiResponse<ModuleDelete>>(jsonMod);
            return View(modules.Data);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ModuleDelete moduleDelete, int id)
        {


            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("UserUTM", User.Identity.Name);
            var putTask = httpClient.DeleteAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Module/" + id); ;

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