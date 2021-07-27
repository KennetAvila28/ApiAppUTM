using AppUTM.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.IO;

namespace AppUTM.Client.Controllers
{
    public class EmpresasController : Controller
    {
        public EmpresasController()
        { }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            //http://api.utmetropolitana.edu.mx/api/Empresas/Get
            //http://localhost:59131/api/Empresas
            var jsonEmpresas = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas");
            var listEmpresas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas);         
            return View(listEmpresas);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(json);
            return View(empresa);
        }
  
        [HttpPost]
        public IActionResult Update(Empresa empresa)
        {
            HttpClient httpClient = new HttpClient();
            
            var putTask = httpClient.PutAsJsonAsync<Empresa>("?id=" + empresa.EmpresaId, empresa);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return this.BadRequest();
        }        
               
    }
}