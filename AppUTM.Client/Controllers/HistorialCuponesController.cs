using AppUTM.Client.Models;
using AppUTM.Client.Responses;
using AppUTM.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class HistorialCuponesController : Controller
    {
        private readonly ILogger<HistorialCuponesController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string _scope;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HistorialCuponesController(ILogger<HistorialCuponesController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
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

            Dashboard listempresa = new Dashboard();

            Historial historial = new Historial();
            var jsonEmpresas = await httpClient.GetStringAsync("http://localhost:59131/api/HistorialCupon");
            var jsonResult = JsonConvert.DeserializeObject(jsonEmpresas).ToString();
            var result = JsonConvert.DeserializeObject<IEnumerable<Historial>> (jsonResult);
            

            return View(result);
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
