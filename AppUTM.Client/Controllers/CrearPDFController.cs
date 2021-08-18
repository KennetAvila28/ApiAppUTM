using AppUTM.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace AppUTM.Client.Controllers
{
    public class CrearPDFController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisition;


        public CrearPDFController(IConfiguration configuration, ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _tokenAcquisition = tokenAcquisition;
        }
        HttpClient httpClient = new HttpClient();
        public async Task<IActionResult> Index(int id)
        {
            //// Define la URL de la Cabecera 
            string _headerUrl = Url.Action("HeaderPDF", "CrearPDF", null, "https");
            //////// Define la URL del Pie de página
            string _footerUrl = Url.Action("FooterPDF", "CrearPDF", null, "https");


            Cupones cupones = new Cupones();
            var jsonEmpresa = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
            cupones.Empresa = empresa;
            var jsonCuponesGenerico = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/empresa/" + id);
            var listCupones = JsonConvert.DeserializeObject<IEnumerable<CuponGenerico>>(jsonCuponesGenerico);
            cupones.cuponesGenericos = listCupones;
            var jsonCuponesImagen = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/empresa/" + id);
            var listCuponesImagen = JsonConvert.DeserializeObject<IEnumerable<CuponImagen>>(jsonCuponesImagen);
            cupones.cuponesImagen = listCuponesImagen;

            return new ViewAsPdf("Index", cupones)
            {
                //    // Establece la Cabecera y el Pie de página
                CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 0 " +
                             "--footer-html " + _footerUrl + " --footer-spacing 0"
                //,
                //    PageMargins = new Margins(50, 10, 12, 10)

            };



        }
        [AllowAnonymous]
        public ActionResult HeaderPDF()
        {
            return View("HeaderPDF");
        }
        [AllowAnonymous]
        public ActionResult FooterPDF()
        {
            return View("FooterPDF");
        }
        [AllowAnonymous]
        public ActionResult HeaderPDFEmpresas()
        {
            return View("HeaderPDFEmpresas");
        }
        [AllowAnonymous]
        public ActionResult HeaderPDFHistorial()
        {
            return View("HeaderPDFHistorial");
        }
        [HttpGet]
        public async Task<IActionResult> EmpresasSinCupones()
        {
            //// Define la URL de la Cabecera 
            string _headerUrl = Url.Action("HeaderPDFEmpresas", "CrearPDF", null, "https");
            //////// Define la URL del Pie de página
            string _footerUrl = Url.Action("FooterPDF", "CrearPDF", null, "https");


            HttpClient httpClient = new HttpClient();
            ListEmpresas listEmpresas = new ListEmpresas();

            var jsonEmpresas = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/all");
            listEmpresas.empresasRegistradas = JsonConvert.DeserializeObject<List<Empresa>>(jsonEmpresas).OrderByDescending(e => e.EmpresaId);
            //Muestra las empresas que proporciona la API de la UTM
            var jsonEmpresasUTM = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/empresasUTM");
            listEmpresas.empresasUTM = System.Text.Json.JsonSerializer.Deserialize<List<EmpresasUTM>>(jsonEmpresasUTM).Where(e => e.RFC != null);
            List<EmpresasUTM> empresasUTM = new List<EmpresasUTM>();
            foreach (var item in listEmpresas.empresasUTM)
            {
                if (!listEmpresas.empresasRegistradas.Any(e => e.RFC == item.RFC))
                    empresasUTM.Add(item);
            }
            listEmpresas.empresasUTM = empresasUTM;
            return new ViewAsPdf("EmpresasSinCupones", listEmpresas)
            {
                //    // Establece la Cabecera y el Pie de página
                CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 0 " +
                  "--footer-html " + _footerUrl + " --footer-spacing 0"
                //,
                //    PageMargins = new Margins(50, 10, 12, 10)

            };
        }
        //PDF DE PRUEBA
        public async Task<IActionResult> Prueba(int id)
        {
            //// Define la URL de la Cabecera 
            string _headerUrl = Url.Action("HeaderPDF", "CrearPDF", null, "https");
            //////// Define la URL del Pie de página
            string _footerUrl = Url.Action("FooterPDF", "CrearPDF", null, "https");


            Cupones cupones = new Cupones();
            var jsonEmpresa = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
            cupones.Empresa = empresa;
            var jsonCuponesGenerico = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesGenericos/empresa/" + id);
            var listCupones = JsonConvert.DeserializeObject<IEnumerable<CuponGenerico>>(jsonCuponesGenerico).OrderByDescending(e => e.FechaExpiracion);
            cupones.cuponesGenericos = listCupones;
            var jsonCuponesImagen = await httpClient.GetStringAsync(_configuration["CouponAdmin:CouponAdminBaseAddress"] + "CuponesImagen/empresa/" + id);
            var listCuponesImagen = JsonConvert.DeserializeObject<IEnumerable<CuponImagen>>(jsonCuponesImagen).OrderByDescending(e => e.FechaExpiracion);
            cupones.cuponesImagen = listCuponesImagen;

            return new ViewAsPdf("Prueba", cupones)
            {
                //    // Establece la Cabecera y el Pie de página
                CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 0 " +
                             "--footer-html " + _footerUrl + " --footer-spacing 0"
                //,
                //    PageMargins = new Margins(50, 10, 12, 10)

            };



        }
        [HttpGet]
        public async Task<IActionResult> HistorialCupon()
        {
            //// Define la URL de la Cabecera 
            string _headerUrl = Url.Action("HeaderPDFHistorial", "CrearPDF", null, "https");
            //////// Define la URL del Pie de página
            string _footerUrl = Url.Action("FooterPDF", "CrearPDF", null, "https");
            HttpClient httpClient = new HttpClient();

            Dashboard listempresa = new Dashboard();

            Historial historial = new Historial();
            var jsonEmpresas = await httpClient.GetStringAsync("http://localhost:59131/api/HistorialCupon");
            var jsonResult = JsonConvert.DeserializeObject(jsonEmpresas).ToString();
            var result = JsonConvert.DeserializeObject<IEnumerable<Historial>>(jsonResult);

            return new ViewAsPdf("HistorialCupon", result)
            {
                //    // Establece la Cabecera y el Pie de página
                CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 0 " +
                             "--footer-html " + _footerUrl + " --footer-spacing 0"
                //,
                //    PageMargins = new Margins(50, 10, 12, 10)

            };

        }
    }
}
