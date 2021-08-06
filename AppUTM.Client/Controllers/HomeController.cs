using System;
using AppUTM.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppUTM.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string _scope;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _httpClient = httpClient;
            _configuration = configuration;
            _scope = "user.read";
        }

        public async Task<IActionResult> Index()
        {
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);
            HttpClient httpClient = new HttpClient();
            Dashboard listempresa = new Dashboard();
            
            //Para empresas
            var jsonEmpresas = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas");
            var jsonResult = JsonConvert.DeserializeObject(jsonEmpresas).ToString();
            var result = JsonConvert.DeserializeObject<List<Empresa>>(jsonResult);
            listempresa.Empresas = result;
            //Para cupones genericos
            var jsonCuponGene = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesGenericos");
            var jsonResult1 = JsonConvert.DeserializeObject(jsonCuponGene).ToString();
            var result1 = JsonConvert.DeserializeObject<List<CuponGenerico>>(jsonResult1);
            listempresa.CuponesGenericos = result1;
            //Para cupones de imagen
            var jsonCuponImag = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesImagen");
            var jsonResult3 = JsonConvert.DeserializeObject(jsonCuponImag).ToString();
            var result3 = JsonConvert.DeserializeObject<List<CuponImagen>>(jsonResult3);
            listempresa.CuponesImagenes = result3;
            //Para cupones totales

            int Lista1 = result1.Count();
            int Lista2 = result3.Count();

            int CuponesTotales = Lista1 + Lista2;

            listempresa.CuponesTotales = CuponesTotales;



            return View(listempresa);

            //var jsonEmpresas = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas");
            //var jsonResult = JsonConvert.DeserializeObject(jsonEmpresas).ToString();
            //var result = JsonConvert.DeserializeObject<List<Empresa>>(jsonResult);

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