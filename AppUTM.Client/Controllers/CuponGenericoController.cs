using AppUTM.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

namespace AppUTM.Client.Controllers
{
    public class CuponGenericoController : Controller
    {
        HttpClient httpClient = new HttpClient();

        public CuponGenericoController()
        { }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            Cupones cupones = new Cupones();
            var jsonCuponesGenerico = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesGenericos/empresa/" + id);
            var listCupones = JsonConvert.DeserializeObject<IEnumerable<CuponGenerico>>(jsonCuponesGenerico);
            cupones.cuponesGenericos = listCupones;
            var jsonCuponesImagen = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesImagen/empresa/" + id);
            var listCuponesImagen = JsonConvert.DeserializeObject<IEnumerable<CuponImagen>>(jsonCuponesImagen);
            cupones.cuponesImagen = listCuponesImagen;
            return View(cupones);            
        }

        [HttpGet]
        public async Task<ActionResult> Generico(int id)
        {
            var jsonCupon = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesGenericos/" + id);
            var cupon = JsonConvert.DeserializeObject<CuponGenerico>(jsonCupon);
            return View(cupon);
        }

        [HttpGet]
        public async Task<ActionResult> Imagen(int id)
        {
            var jsonCupon = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesImagen/" + id);
            var cupon = JsonConvert.DeserializeObject<CuponImagen>(jsonCupon);
            return View(cupon);
        }
    }
}