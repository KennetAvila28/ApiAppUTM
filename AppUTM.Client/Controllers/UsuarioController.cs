using AppUTM.Client.Models.Roles;
using AppUTM.Client.Models.Users;
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
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisicion;

        public UsuarioController(IConfiguration configuration, ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _tokenAcquisicion = tokenAcquisition;
        }
        //_Configuration["CouponAdmin:CouponAdminBaseAddress"] + ""
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Users");
            var listUsuario = JsonConvert.DeserializeObject<ApiResponse<List<UserReturn>>>(jsonUser);
            return View(listUsuario.Data);
        }
        // GET: UsuarioController

        // GET: UsuarioController/Details/5
        public async Task<ActionResult> Detalles(int id)
        {
            HttpClient httpClient = new HttpClient();
            var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Users/" + id);
            var listUsuario = JsonConvert.DeserializeObject<ApiResponse<UserReturn>>(jsonUser);
            return View(listUsuario.Data);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Users/");
            var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonUser);
            ViewBag.Roles = listRoles.Data;
            var crear = JsonConvert.DeserializeObject<UserCreate>(json);
            return View(crear);
        }

        [HttpPost]
        public IActionResult Create(UserCreate userCreate, int[] role)
        {
            HttpClient httpClient = new HttpClient();
            List<UserRole> listaRoles = new List<UserRole>();
            foreach (var item in role)
            {
                UserRole roleUser = new UserRole();
                roleUser.RoleId = item;
                listaRoles.Add(roleUser);
            }
            userCreate.UserRoles = listaRoles;

            var CrearCliente = httpClient.PostAsJsonAsync<UserCreate>("http://localhost:59131/api/Users", userCreate);
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
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Users/" + id);
            var jsonUser = await httpClient.GetStringAsync("http://localhost:59131/api/Roles/");
            var listRoles = JsonConvert.DeserializeObject<ApiResponse<List<RoleReturn>>>(jsonUser);
            var crear = JsonConvert.DeserializeObject<ApiResponse<UserForUpdateDto>>(json);

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
            var updateClient = await httpClient.PutAsJsonAsync<UserForUpdateDto>("http://localhost:59131/api/Users/" + userForUpdateDto.Id, userForUpdateDto);
            if (updateClient.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}