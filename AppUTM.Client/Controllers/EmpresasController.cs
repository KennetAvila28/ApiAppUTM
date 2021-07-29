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
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using AppUTM.Client.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;


namespace AppUTM.Client.Controllers
{
    public class EmpresasController : Controller
    {

        IWebHostEnvironment _env;
        public EmpresasController(IWebHostEnvironment environment)
        {
            this._env = environment;
        }

        private readonly IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisition;

        public EmpresasController(IConfiguration configuration, ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _tokenAcquisition = tokenAcquisition;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            //http://api.utmetropolitana.edu.mx/api/Empresas/Get
            //http://localhost:59131/api/Empresas
            var jsonEmpresas = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas");
            var listEmpresas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas);
            return View(listEmpresas);
        }
       
        public async Task<IActionResult> Update(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas/" + id);
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
            httpClient.BaseAddress = new Uri("http://localhost:59131/api/Empresas/");
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


        //It doesn't work well
        /*
        [HttpPost]
        private async Task<string> SendImage(IFormFile imagen)
        {
            HttpClient httpClient = new HttpClient();
            var fileName = imagen.FileName;
            var nombreArchivo = $"{Guid.NewGuid()}-{fileName}";
            string url = "http://localhost:59131/api/Empresas/agregarFoto";
            using (var memoryStream = new MemoryStream())
            {
                var path = Path.GetTempPath();           
                var ruta = Path.Combine(path, fileName);                
                await imagen.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray();

                var archivo = Path.GetFileName(ruta);
                await System.IO.File.WriteAllBytesAsync(ruta, contenido);
                using var requestContent = new MultipartFormDataContent();
                using var fileStream = System.IO.File.OpenRead(ruta);
                requestContent.Add(new StreamContent(fileStream), "imagen", archivo);
                await httpClient.PostAsync(url, requestContent);
            }
            return fileName;
        }

        */        
    }
}