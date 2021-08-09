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


    }
}
