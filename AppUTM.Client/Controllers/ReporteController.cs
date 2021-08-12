using AppUTM.Client.Models;
using AppUTM.Client.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
   
    [Authorize]
    public class ReporteController : Controller
    {
        private readonly ILogger<ReporteController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string _scope;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ReporteController(ILogger<ReporteController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
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
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            HttpClient httpClient = new HttpClient();
            //http://api.utmetropolitana.edu.mx/api/Empresas/Get
            //http://localhost:59131/api/Empresas
            ListEmpresas listEmpresas = new ListEmpresas();
            var jsonEmpresas = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas");
            listEmpresas.empresasRegistradas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas);
            var jsonEmpresasUTM = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/empresasUTM");
            listEmpresas.empresasUTM = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<EmpresasUTM>>(jsonEmpresasUTM);



            return View(listEmpresas);
        }
        [HttpGet]
        public async Task<IActionResult> Reporte1()
        {
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            //ViewBag.image = await GetPhoto(_httpClient);

            HttpClient httpClient = new HttpClient();
            //http://api.utmetropolitana.edu.mx/api/Empresas/Get
            //http://localhost:59131/api/Empresas

            ListEmpresas listEmpresas = new ListEmpresas();
            var jsonEmpresas = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas");
            listEmpresas.empresasRegistradas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas);
            var jsonEmpresasUTM = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/empresasUTM");
            listEmpresas.empresasUTM = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<EmpresasUTM>>(jsonEmpresasUTM);



            return View(listEmpresas);
        }

        public async Task<IActionResult> Update(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(json);
            return View(empresa);
        }

        [HttpPost]
        public IActionResult Update(int id, Empresa empresa)
        {
            HttpClient httpClient = new HttpClient();

            if (empresa.Foto != null)
            {
                string imagen = UploadImage(empresa);
                empresa.ImagenEmpresa = imagen;
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

        private string UploadImage(Empresa empresa)
        {
            string fileName = null, filePath = null;
            if (empresa.Foto != null)
            {
                string path = empresa.Domain + @"\wwwroot\Empresas\";
                fileName = Guid.NewGuid().ToString() + "-" + empresa.Foto.FileName;
                filePath = Path.Combine(path, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    empresa.Foto.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public IActionResult Privacy()
        {
            return View();
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
