using AppUTM.Api.DTOS.Empresas;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICuponGenericoServices _genericosService;
        private readonly ICuponImagenServices _cuponImagenService;
        private readonly IMapper _mapper;       
        //private readonly string contenedor = "Empresas";

        public EmpresasController(IEmpresaServices services, IMapper mapper, ICuponGenericoServices generico, ICuponImagenServices cuponImagen)
        {
            this._service = services;            
            this._mapper = mapper;
            this._genericosService = generico;
            this._cuponImagenService = cuponImagen;
        }

        //Eliminar empresa
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var empresa = await _service.GetEmpresa(id);
            if (empresa == null) return NotFound();
            await _service.DeleteEmpresa(empresa);
            return Ok(true);
        }

        //Desactiva empresa
        [HttpGet("Desactivar/{id:int}")]
        public async Task<ActionResult> Desactivar(int id)
        {
            var empresa = await _service.GetEmpresa(id);
            if (empresa == null) return NotFound();
            empresa.Activa = false;
            await _service.UpdateEmpresa(empresa);         
            return Ok(empresa);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaReturn>>> Get()
        {
            var empresas = await _service.GetEmpresas();
            empresas = empresas.Where(e => e.Activa == true);
            var empresasDto = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaReturn>>(empresas);
            var empresasJson = JsonSerializer.Serialize(empresasDto);
            return Ok(empresasJson);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<EmpresaReturn>>> GetAll()
        {
            var empresas = await _service.GetEmpresas();      
            var empresasDto = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaReturn>>(empresas);
            var empresasJson = JsonSerializer.Serialize(empresasDto);
            return Ok(empresasJson);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empresa>> Details(int id)
        {      
            var empresa = await _service.GetEmpresa(id);
            var empresaDto = _mapper.Map<Empresa, EmpresaReturn>(empresa);     
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
            var empresa = _mapper.Map(empresaDto, empresaData);
            await _service.UpdateEmpresa(empresa);
            return Ok(empresa);
        }
       

        //Devuelve los registros de la api externa con formato correcto   

        [HttpGet("empresasUTM")]
        public async Task<ActionResult> GetEmpresasUTM()
        {
            HttpClient client = new HttpClient();
            var jsonEmpresas = await client.GetStringAsync("https://api.utmetropolitana.edu.mx/api/Empresas/Get");            
            var empresas = Regex.Replace(jsonEmpresas, @"\s{2,}|//", " ").Substring(1); //Elimina caracteres // y []
            //Elimina las comillas dobles en los campos.            
            empresas = empresas.Remove(empresas.Length - 1).Replace(@"\", "").Replace("\u0022", "");
            var empresasUTM = empresas.Replace("IdEmpresa:", "\u0022IdEmpresa\u0022:\u0022").Replace(",NombreEmpresa:", "\u0022,\u0022NombreEmpresa\u0022:\u0022").Replace(",Direccion:", "\u0022,\u0022Direccion\u0022:\u0022").Replace(",Telefono:", "\u0022,\u0022Telefono\u0022:\u0022")
                .Replace(",Celular:", "\u0022,\u0022Celular\u0022:\u0022").Replace(",CorreoEmpresa:", "\u0022,\u0022CorreoEmpresa\u0022:\u0022").Replace(",RFC:", "\u0022,\u0022RFC\u0022:\u0022").Replace(",PersonaResponsable:", "\u0022,\u0022PersonaResponsable\u0022:\u0022").Replace("}", "\u0022}").Replace("\u0022\u0022", "null");
            return Ok(empresasUTM);         
        }

        /*
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

        */
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