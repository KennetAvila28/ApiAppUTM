using AppUTM.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Identity.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppUTM.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            var jsonEmpresas = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas");
            var jsonResult = JsonConvert.DeserializeObject(jsonEmpresas).ToString();
            var result = JsonConvert.DeserializeObject<List<Empresa>>(jsonResult);

            return View(result);
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
    }
}