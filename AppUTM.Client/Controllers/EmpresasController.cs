using AppUTM.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class EmpresasController : Controller
    {
        public EmpresasController()
        { }
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            //http://api.utmetropolitana.edu.mx/api/Empresas/Get
            //http://localhost:59131/api/Empresas
            var jsonEmpresas = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas");
            var listEmpresas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas);
            int numeroempresas = listEmpresas.Count();

            return View(listEmpresas);
        }
    }
}
