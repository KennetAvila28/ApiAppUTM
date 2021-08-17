
using AppUTM.Api.DTOS.Users;
using AppUTM.Client.Models;
using AppUTM.Client.Models.Roles;
using AppUTM.Client.Models.Users;
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
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string _scope;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UsuarioController(ILogger<UsuarioController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _httpClient = httpClient;
            _configuration = configuration;
            _scope = "user.read";
        }


        //_Configuration["CouponAdmin:CouponAdminBaseAddress"] + ""
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);


            var jsonUser = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users");
            var jsonRoles = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles");

            //var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Users");
            // var jsonRoles = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonRoles);
            ViewBag.Roles = listRoles.Data;
            var listUsuario = JsonConvert.DeserializeObject<ApiResponse<List<UserReturn>>>(jsonUser);
            return View(listUsuario.Data);
        }

        [HttpGet] //Vista para busqueda
        public IActionResult Empleados()
        {

            return View();
        }

        // GET: Empleado
        [HttpGet]
        public async Task<IActionResult> EmpleadoConsulta(string correo)
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            List<EmpleadoUTM> listemp = new List<EmpleadoUTM>();
            //string extension = "@utmetropolitana.edu.mx";
            //Muestra las empresas que proporciona la API de la UTM

            var jsonEmpleado = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users/" + "empleado/" + correo);


            // var jsonEmpleado = await httpClient.GetStringAsync("http://localhost:59131/api/Users/empleado/" + correo);
            var jsonlist = JsonConvert.DeserializeObject<List<EmpleadoUTM>>(jsonEmpleado);
            ViewBag.listemp = jsonlist;
            return View(jsonlist);
        }
        // GET: UsuarioController

        // GET: UsuarioController/Details/5
        public async Task<ActionResult> Detalles(int id)
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);


            var jsonUser = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users/" + id);


            //var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Users/" + id);
            var listUsuario = JsonConvert.DeserializeObject<ApiResponse<UserReturn>>(jsonUser);
            return View(listUsuario.Data);
        }
        [HttpGet]
        public async Task<ActionResult> Create(int ClaveEmpleado, string Nombres, string ApellidoPaterno, string ApellidoMaterno, string Correo)
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            User empleado = new User();
            empleado.ClaveEmpleado = ClaveEmpleado;
            empleado.Nombres = Nombres;
            empleado.ApellidoPaterno = ApellidoPaterno;
            empleado.ApellidoMaterno = ApellidoMaterno;
            empleado.Correo = Correo;

            var json2 = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users");

            var jsonUser = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles");



            //var json = await httpClient.GetStringAsync("http://localhost:59131/api/Users/");
            //var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonUser);
            ViewBag.Roles = listRoles.Data;
            var crear = JsonConvert.DeserializeObject<UserCreate>(json2);
            return View(crear);
        }

        [HttpPost]
        public async Task <IActionResult> Create(UserCreate userCreate, int[] role)
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);


            List<UserRole> listaRoles = new List<UserRole>();
            foreach (var item in role)
            {
                UserRole roleUser = new UserRole();
                roleUser.RoleId = item;
                listaRoles.Add(roleUser);
            }
            userCreate.UserRoles = listaRoles;


            var CrearCliente = httpClient.PostAsJsonAsync<UserCreate>(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users", userCreate);
            CrearCliente.Wait();

            var CreaResult = CrearCliente.Result;
            if (CreaResult.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }


        // GET: UsuarioController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);


            var json2 = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users/" + id);
            var jsonUser = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Roles");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonUser);
            var crear = JsonConvert.DeserializeObject<ApiResponse<UserForUpdateDto>>(json2);

            foreach (var rol in crear.Data.UserRoles)
            {
                listRoles.Data.Remove(listRoles.Data.Single(r => rol.RoleId == r.Id));

            }

            ViewBag.Roles = listRoles.Data;

            return View(crear.Data);

        }

        // POST: UsuarioController/Edit/5
        [HttpPost]

        public async Task<ActionResult> Edit(int id, UserForUpdateDto userForUpdateDto, int[] roleDelete, int[] roleAdd)
        {
            HttpClient httpClient = new HttpClient();
            //List<UserRole> listaRoles = new List<UserRole>();
            if (roleDelete != null)
            {
                userForUpdateDto.RolesToBeDelete = roleDelete;
            }

            if (roleAdd != null)
            {
                userForUpdateDto.UserRoles = new List<UserRole>();

                foreach (var item in roleAdd)
                {
                    UserRole roleUser = new UserRole();
                    roleUser.RoleId = item;
                    userForUpdateDto.UserRoles.Add(roleUser);

                }

            }
            var updateClient = await httpClient.PutAsJsonAsync<UserForUpdateDto>(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users/" + userForUpdateDto.Id, userForUpdateDto);
            if (updateClient.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        // GET: UsuarioController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {


            HttpClient httpClient = new HttpClient();

            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            var json2 = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users/" + id);
            var permisos = JsonConvert.DeserializeObject<ApiResponse<UserDelete>>(json2);
            return View(permisos.Data);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserDelete userDelete, int id)
        {


            HttpClient httpClient = new HttpClient();

            var putTask = httpClient.DeleteAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Users/" + id); 
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