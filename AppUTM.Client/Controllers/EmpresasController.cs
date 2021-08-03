using AppUTM.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {   
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
            //Muestra las empresas registradas
            ListEmpresas listEmpresas = new ListEmpresas();
            var jsonEmpresas = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas");
            listEmpresas.empresasRegistradas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas);
            //Muestra las empresas que proporciona la API de la UTM
            var jsonEmpresasUTM = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/empresasUTM");
            listEmpresas.empresasUTM = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<EmpresasUTM>>(jsonEmpresasUTM);
            return View(listEmpresas);
        }


       
        public async Task<IActionResult> Update(int id)
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(json);
            var jsonCuponesGenericos = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/empresa/" + id);
            var jsonCuponesImagen = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/empresa/" + id);
            if (jsonCuponesGenericos != null || jsonCuponesImagen != null) empresa.Cupones = true;
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


        //It doesn't work well
        /*
        [HttpPost]
        private async Task<string> SendImage(IFormFile imagen)
        {
            HttpClient httpClient = new HttpClient();
            var fileName = imagen.FileName;
            var nombreArchivo = $"{Guid.NewGuid()}-{fileName}";
            string url =  _configuration["CouponAdmin:CouponAdminBaseAddress"]  + "Empresas/agregarFoto";
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