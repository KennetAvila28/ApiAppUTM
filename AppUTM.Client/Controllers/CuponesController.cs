﻿using AppUTM.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class CuponesController : Controller
    {
        HttpClient httpClient = new HttpClient();

        public CuponesController()
        { }

        [HttpGet]
        public async Task<IActionResult> Index (int id)
        {
            Cupones cupones = new Cupones();
            var jsonEmpresa = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
            cupones.Empresa = empresa;
            var jsonCuponesGenerico = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesGenericos/empresa/" + id);
            var listCupones = JsonConvert.DeserializeObject<IEnumerable<CuponGenerico>>(jsonCuponesGenerico);            
            cupones.cuponesGenericos = listCupones;
            var jsonCuponesImagen = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesImagen/empresa/" + id);
            var listCuponesImagen = JsonConvert.DeserializeObject<IEnumerable<CuponImagen>>(jsonCuponesImagen);           
            cupones.cuponesImagen = listCuponesImagen;          
            return View(cupones);
        }

        //  CUPONES GÉNERICOS
        [HttpGet]
        public async Task<ActionResult> Generico (int id)
        {
            var jsonCupon = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesGenericos/" + id);
            var cupon = JsonConvert.DeserializeObject<CuponGenerico>(jsonCupon);
            return View(cupon);
        }

        public async Task<IActionResult> CreateGenerico(int id)
        {
            CuponGenerico cupon = new CuponGenerico();
            var jsonEmpresa = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
            cupon.EmpresaId = empresa.EmpresaId;
            cupon.NombreEmpresa = empresa.Nombre;
            return View(cupon);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenerico (CuponGenerico cupon)
        {
            var json = await httpClient.PostAsJsonAsync("http://localhost:59131/api/CuponesGenericos/", cupon);
            if (json.IsSuccessStatusCode)
                return RedirectToAction ("Index", new { id = cupon.EmpresaId });
            else
                return RedirectToAction ("Error", "Home");
        }

        public async Task<IActionResult> UpdateGenerico (int id)
        {
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesGenericos/ " + id);
            var cupon = JsonConvert.DeserializeObject<CuponGenerico>(json);           
            return View(cupon);
        }

        [HttpPost]
        public IActionResult UpdateGenerico(int id, CuponGenerico cupon)
        {
            httpClient.BaseAddress = new Uri("http://localhost:59131/api/CuponesGenericos/");
            var putTask = httpClient.PutAsJsonAsync<CuponGenerico>("?id=" + id, cupon);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
                return View (cupon);
            else
                return RedirectToAction ("Error", "Home");
        }

        public IActionResult DeleteGenerico (int empresaId, int id)
        {
            var deleteTask = httpClient.DeleteAsync("http://localhost:59131/api/CuponesGenericos/" + id);
            deleteTask.Wait();
            if (deleteTask.Result.IsSuccessStatusCode)
                return RedirectToAction("Index", new { id = empresaId });
            else
                return RedirectToAction("Error", "Home");
        }



        //  ----------  CUPONES CON IMAGEN ----------//

        [HttpGet]
        public async Task<ActionResult> CuponImagen(int id)
        {
            var jsonCupon = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesImagen/" + id);
            var cupon = JsonConvert.DeserializeObject<CuponImagen>(jsonCupon);
            return View(cupon);
        }


        public async Task<IActionResult> CreateCuponImagen(int id)
        {
            CuponImagen cupon = new CuponImagen();
            var jsonEmpresa = await httpClient.GetStringAsync("http://localhost:59131/api/Empresas/" + id);
            var empresa = JsonConvert.DeserializeObject<Empresa>(jsonEmpresa);
            cupon.EmpresaId = empresa.EmpresaId;
            cupon.NombreEmpresa = empresa.Nombre;
            cupon.Domain = empresa.Domain;
            return View(cupon);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCuponImagen(CuponImagen cupon)
        {
            cupon.Imagen = UploadImage(cupon);
            var json = await httpClient.PostAsJsonAsync("http://localhost:59131/api/CuponesImagen/", cupon);
            if (json.IsSuccessStatusCode)
                return RedirectToAction("Index", new { id = cupon.EmpresaId });
            else
                return RedirectToAction("Error", "Home");
        }      


        public async Task<IActionResult> UpdateCuponImagen(int id)
        {            
            var json = await httpClient.GetStringAsync("http://localhost:59131/api/CuponesImagen/ " + id);
            var cupon = JsonConvert.DeserializeObject<CuponImagen>(json);
            return View(cupon);
        } 

        [HttpPost]
        public IActionResult UpdateCuponImagen(int id, CuponImagen cupon)
        {
            if (cupon.Foto != null)
                cupon.Imagen = UploadImage(cupon);
            httpClient.BaseAddress = new Uri("http://localhost:59131/api/CuponesImagen/");
            var putTask = httpClient.PutAsJsonAsync<CuponImagen>("?id=" + id, cupon);
            putTask.Wait();
            if (putTask.Result.IsSuccessStatusCode)
                return View(cupon);
            else
                return RedirectToAction("Error", "Home");
        }

        public IActionResult DeleteCuponImagen(int empresaId, int id)
        {
            var deleteTask = httpClient.DeleteAsync("http://localhost:59131/api/CuponesImagen/" + id);
            deleteTask.Wait();
            if (deleteTask.Result.IsSuccessStatusCode)
                return RedirectToAction("Index", new { id = empresaId });
            else
                return RedirectToAction("Error", "Home");
        }


        //Subir imagen
        private string UploadImage(CuponImagen cupon)
        {
            string fileName = null, filePath = null;
            if (cupon.Foto != null)
            {
                string path = cupon.Domain + @"\wwwroot\Cupones\";
                fileName = Guid.NewGuid().ToString() + "-" + cupon.Foto.FileName;
                filePath = Path.Combine(path, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    cupon.Foto.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
