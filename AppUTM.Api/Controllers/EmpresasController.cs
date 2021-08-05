using AppUTM.Api.DTOS.Empresas;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaServices _service;
        private readonly IMapper _mapper;
        public readonly IAlmacenarImagen _almacenarImagen;
        private readonly string contenedor = "Empresas";
        private readonly IWebHostEnvironment _env;

        public EmpresasController(IEmpresaServices services, IMapper mapper, IAlmacenarImagen almacenarImagen, IWebHostEnvironment env)
        {
            this._service = services;
            this._mapper = mapper;
            this._almacenarImagen = almacenarImagen;
            this._env = env;
        }

        //EliminarEmpresa
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var empresa = await _service.GetEmpresa(id);
            if (empresa == null) return NotFound();
            await _service.DeleteEmpresa(empresa);
            return Ok(true);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaReturn>>> Get()
        {
            var empresas = await _service.GetEmpresas();
            var empresasDto = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaReturn>>(empresas);
            var empresasJson = JsonSerializer.Serialize(empresasDto);
            return Ok(empresasJson);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empresa>> Details(int id)
        {
            string startupPath = Environment.CurrentDirectory;
            var empresa = await _service.GetEmpresa(id);
            var empresaDto = _mapper.Map<Empresa, EmpresaReturn>(empresa);
            if (empresa != null) empresaDto.Domain = startupPath;
            return Ok(empresaDto);
        }     

        [HttpPost]
        public async Task<ActionResult> Post(EmpresaCreate empresaDto)
        {
            var empresa = _mapper.Map<EmpresaCreate, Empresa>(empresaDto);
            await _service.AddEmpresa(empresa);
            var empresaResponseDto = _mapper.Map<Empresa, EmpresaReturn>(empresa);
            return Ok(empresaResponseDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, EmpresaCreate empresaDto)
        {
            var empresaData = await _service.GetEmpresa(id);
            if (empresaData == null) return NotFound();
            if (empresaData.ImagenEmpresa != empresaDto.ImagenEmpresa)
                await _almacenarImagen.BorraArchivo(empresaData.ImagenEmpresa, contenedor);
            var empresa = _mapper.Map(empresaDto, empresaData);
            await _service.UpdateEmpresa(empresa);
            return Ok(empresa);
        }

        //Regresa la imagen de la empresa
        [HttpGet]
        [Route("image")]
        public IActionResult ReturnImage([FromQuery] string nombreArchivo)
        {
            try
            {
                var image = System.IO.File.OpenRead(_env.WebRootPath + "/" + contenedor + "/" + nombreArchivo);
                return File(image, "image/jpeg");
            }
            catch
            {
                var image = System.IO.File.OpenRead(_env.WebRootPath + "/" + "not-available.jpg");
                return File(image, "image/jpeg");
            }
        }


        //Devuelve los registros de la api externa
        //De ser posible Eliminar las comillas con una expresión regular.
        //  La expresión inferior selecciona los valores de los campos, sería util si seleccionará unicamente los campos 
        //  Expresión: (?<=PersonaResponsable\u0022:\u0022).*?(?=\u0022})|(?<=:\u0022).*?(?=\u0022,) 

        [HttpGet("empresasUTM")]
        public async Task<ActionResult> GetEmpresasUTM()
        {
            HttpClient client = new HttpClient();
            var jsonEmpresas = await client.GetStringAsync("https://api.utmetropolitana.edu.mx/api/Empresas/Get");            
            var empresas = Regex.Replace(jsonEmpresas, @"\s{2,}|//", " ").Substring(1); //Elimina caracteres // y []
            //Elimina las comillas dobles en los campos.            
            empresas = empresas.Remove(empresas.Length - 1).Replace(@"\", "").Replace("\u0022", "");
            var empresasUTM = empresas.Replace("IdEmpresa:", "\u0022IdEmpresa\u0022:\u0022").Replace(",NombreEmpresa:", "\u0022,\u0022NombreEmpresa\u0022:\u0022").Replace(",Direccion:", "\u0022,\u0022Direccion\u0022:\u0022").Replace(",Telefono:", "\u0022,\u0022Telefono\u0022:\u0022")
                .Replace(",Celular:", "\u0022,\u0022Celular\u0022:\u0022").Replace(",CorreoEmpresa:", "\u0022,\u0022CorreoEmpresa\u0022:\u0022").Replace(",RFC:", "\u0022,\u0022RFC\u0022:\u0022").Replace(",PersonaResponsable:", "\u0022,\u0022PersonaResponsable\u0022:\u0022").Replace("}", "\u0022}");                       
            return Ok(empresasUTM);         
        }

        [HttpGet("domain")]
        public ActionResult<string> Dominio()
        {
            string startupPath = Environment.CurrentDirectory;
            var Domain = startupPath;
            return Ok(Domain);
        }


        //No aplica, Subir imagenes  multipart/form-data
        /*
        [HttpPost("agregarFoto")]
        public async Task<ActionResult> PostImage([FromForm] IFormFile imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    await _almacenarImagen.EditarArchivo(contenido, contenedor, imagen.FileName);
                }
            }
            return Ok(true);
        }

        */

    }
}