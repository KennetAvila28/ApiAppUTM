using AppUTM.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
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

        HttpClient httpClient = new HttpClient();

        public CrearPDFController(IConfiguration configuration, ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _tokenAcquisition = tokenAcquisition;
        }
        public async Task<IActionResult> Index(int id)
        {
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

            };
        }
    }
}
