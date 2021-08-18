using AppUTM.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        private readonly ILogger<EmpresasController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly HttpClient _httpClient;
        private readonly string _scope;

        public EmpresasController(ILogger<EmpresasController> logger, IConfiguration configuration, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IWebHostEnvironment environment)
        {
            _logger = logger;
            _configuration = configuration;
            _tokenAcquisition = tokenAcquisition;
            _httpClient = httpClient;
            _environment = environment;
            _scope = "user.read";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            ListEmpresas listEmpresas = new ListEmpresas();
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);
            var jsonEmpresas = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/all");
            listEmpresas.empresasRegistradas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas).OrderByDescending(e => e.EmpresaId);
            //Muestra las empresas que proporciona la API de la UTM
            var jsonEmpresasUTM = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/empresasUTM");
            listEmpresas.empresasUTM = System.Text.Json.JsonSerializer.Deserialize<List<EmpresasUTM>>(jsonEmpresasUTM);
            List<EmpresasUTM> empresasUTM = new List<EmpresasUTM>();
            foreach (var item in listEmpresas.empresasUTM)
            {
                if (!listEmpresas.empresasRegistradas.Any(e => e.RFC == item.RFC))
                    empresasUTM.Add(item);
            }
            listEmpresas.empresasUTM = empresasUTM;
            return View(listEmpresas);
        }

        public IActionResult Create(string RFC, string Nombre, string Direccion, string Telefono)
        {   
            Empresa empresa = new Empresa();          
            empresa.RFC = RFC;
            empresa.Nombre = Nombre;
            empresa.Direccion = Direccion;
            empresa.Telefono = Telefono;    
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            HttpClient httpClient = new HttpClient();           
            if (empresa.Foto != null)
            {
                using (var sm = new MemoryStream())
                {
                    await empresa.Foto.CopyToAsync(sm);
                    var fileBytes = sm.ToArray();
                    empresa.ImagenEmpresa = Convert.ToBase64String(fileBytes);
                }
            }
            else
            {
                using (var sm = new MemoryStream())
                {
                    var path = _environment.WebRootPath + "/img/xcompany.jpg";
                    Byte[] bytes = System.IO.File.ReadAllBytes(path);
                    empresa.ImagenEmpresa = Convert.ToBase64String(bytes);                
                }
            }
            var json = await httpClient.PostAsJsonAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas", empresa);
            if (json.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Error", "Home");
        }
       
        public async Task<IActionResult> Update(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(json);
            var jsonCuponesGenericos = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/empresa/" + id);
            var jsonCuponesImagen = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/empresa/" + id);
            if (jsonCuponesGenericos.Length > 5 || jsonCuponesImagen.Length > 5) empresa.Cupones = true;
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Empresa empresa)
        {
            HttpClient httpClient = new HttpClient();

            if (empresa.Foto != null)
            {
                using (var sm = new MemoryStream())
                {
                    await empresa.Foto.CopyToAsync(sm);
                    var fileBytes = sm.ToArray();
                    empresa.ImagenEmpresa = Convert.ToBase64String(fileBytes);
                }
            }
            httpClient.BaseAddress = new Uri(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/");
            var putTask = httpClient.PutAsJsonAsync<Empresa>("?id=" + id, empresa);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return View(empresa);
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPut]
        public async Task<IActionResult> Desactivar(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/Desactivar/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(json);
            return View(empresa);
        }

        public IActionResult DeleteEmpresas(int empresaId, int id)
        {
            HttpClient httpClient = new HttpClient();

            var deleteTask = httpClient.DeleteAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
            deleteTask.Wait();
            if (deleteTask.Result.IsSuccessStatusCode)
                return RedirectToAction("Index", new { id = empresaId });
            else
                return RedirectToAction("Error", "Home");
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