using AppUTM.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
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
 
    public class ReporteEmpresaController : Controller
    {
        private readonly ILogger<ReporteEmpresaController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string _scope;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ReporteEmpresaController(ILogger<ReporteEmpresaController> logger, ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _httpClient = httpClient;
            _configuration = configuration;
            _scope = "user.read";
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, string sortOrder)
        {
            await PrepareAuthenticatedClient();
            string json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
            ViewBag.image = await GetPhoto(_httpClient);

            HttpClient httpClient = new HttpClient();
            Cupones cupones = new Cupones();
            CuponGenerico CuponG = new CuponGenerico();
            var jsonEmpresa = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
            cupones.Empresa = empresa;
            var jsonCuponesGenerico = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/empresa/" + id);
            var listCupones = JsonConvert.DeserializeObject<List<CuponGenerico>>(jsonCuponesGenerico);
            cupones.cuponesGenericos = listCupones;
            var jsonCuponesImagen = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/empresa/" + id);
            var listCuponesImagen = JsonConvert.DeserializeObject<List<CuponImagen>>(jsonCuponesImagen);
            cupones.cuponesImagen = listCuponesImagen;

            CuponG.CuponesDisponibles = CuponG.NumeroPorPersona - CuponG.CuponesUsados;
            //ViewData["NombreSortParm"] = string.IsNullOrEmpty(sortOrder) ? "NOMBRE_DESC":"";
            //ViewData["DescripcionSortParm"] = sortOrder == "descripcion_asc" ? "descripcion_desc" : "descripcion_asc";
            //var categorias = from s in cupones.cuponesGenericos
            
            return View(cupones);
           
         
            
        }

        ////  CUPONES GÉNERICOS
        //[HttpGet]
        //public async Task<ActionResult> Generico(int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var jsonCupon = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/" + id);
        //    var cupon = JsonConvert.DeserializeObject<CuponGenerico>(jsonCupon);
        //    return View(cupon);
        //}

        //public async Task<IActionResult> CreateGenerico(int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    CuponGenerico cupon = new CuponGenerico();
        //    var jsonEmpresa = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
        //    var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
        //    cupon.EmpresaId = empresa.EmpresaId;
        //    cupon.NombreEmpresa = empresa.Nombre;
        //    return View(cupon);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateGenerico(CuponGenerico cupon)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var json = await httpClient.PostAsJsonAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/", cupon);
        //    if (json.IsSuccessStatusCode)
        //        return RedirectToAction("Index", new { id = cupon.EmpresaId });
        //    else
        //        return RedirectToAction("Error", "Home");
        //}

        //public async Task<IActionResult> UpdateGenerico(int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var json = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/ " + id);
        //    var cupon = JsonConvert.DeserializeObject<CuponGenerico>(json);
        //    return View(cupon);
        //}

        //[HttpPost]
        //public IActionResult UpdateGenerico(int id, CuponGenerico cupon)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/");
        //    var putTask = httpClient.PutAsJsonAsync<CuponGenerico>("?id=" + id, cupon);
        //    putTask.Wait();
        //    var result = putTask.Result;
        //    if (result.IsSuccessStatusCode)
        //        return View(cupon);
        //    else
        //        return RedirectToAction("Error", "Home");
        //}

        //public IActionResult DeleteGenerico(int empresaId, int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var deleteTask = httpClient.DeleteAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/" + id);
        //    deleteTask.Wait();
        //    if (deleteTask.Result.IsSuccessStatusCode)
        //        return RedirectToAction("Index", new { id = empresaId });
        //    else
        //        return RedirectToAction("Error", "Home");
        //}



        ////  ----------  CUPONES CON IMAGEN ----------//

        //[HttpGet]
        //public async Task<ActionResult> CuponImagen(int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var jsonCupon = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/" + id);
        //    var cupon = JsonConvert.DeserializeObject<CuponImagen>(jsonCupon);
        //    return View(cupon);
        //}


        //public async Task<IActionResult> CreateCuponImagen(int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    CuponImagen cupon = new CuponImagen();
        //    var jsonEmpresa = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
        //    var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
        //    cupon.EmpresaId = empresa.EmpresaId;
        //    cupon.NombreEmpresa = empresa.Nombre;
        //    cupon.Domain = empresa.Domain;
        //    return View(cupon);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateCuponImagen(CuponImagen cupon)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    cupon.Imagen = UploadImage(cupon);
        //    var json = await httpClient.PostAsJsonAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/", cupon);
        //    if (json.IsSuccessStatusCode)
        //        return RedirectToAction("Index", new { id = cupon.EmpresaId });
        //    else
        //        return RedirectToAction("Error", "Home");
        //}


        //public async Task<IActionResult> UpdateCuponImagen(int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var json = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/ " + id);
        //    var cupon = JsonConvert.DeserializeObject<CuponImagen>(json);
        //    return View(cupon);
        //}

        //[HttpPost]
        //public IActionResult UpdateCuponImagen(int id, CuponImagen cupon)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    if (cupon.Foto != null)
        //        cupon.Imagen = UploadImage(cupon);
        //    httpClient.BaseAddress = new Uri(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/");
        //    var putTask = httpClient.PutAsJsonAsync<CuponImagen>("?id=" + id, cupon);
        //    putTask.Wait();
        //    if (putTask.Result.IsSuccessStatusCode)
        //        return View(cupon);
        //    else
        //        return RedirectToAction("Error", "Home");
        //}

        //public IActionResult DeleteCuponImagen(int empresaId, int id)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var deleteTask = httpClient.DeleteAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/" + id);
        //    deleteTask.Wait();
        //    if (deleteTask.Result.IsSuccessStatusCode)
        //        return RedirectToAction("Index", new { id = empresaId });
        //    else
        //        return RedirectToAction("Error", "Home");
        //}


        ////Subir imagen
        //private string UploadImage(CuponImagen cupon)
        //{
        //    string fileName = null, filePath = null;
        //    if (cupon.Foto != null)
        //    {
        //        string path = cupon.Domain + @"\wwwroot\Cupones\";
        //        fileName = Guid.NewGuid().ToString() + "-" + cupon.Foto.FileName;
        //        filePath = Path.Combine(path, fileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            cupon.Foto.CopyTo(fileStream);
        //        }
        //    }
        //    return fileName;
        //}
        //public ActionResult ExportarExcel()
        //{

        //    return View();
        //}
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


