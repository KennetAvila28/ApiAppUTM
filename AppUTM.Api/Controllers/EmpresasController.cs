using AppUTM.Api.DTOS.Empresas;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaReturn>>> Get()
        {
            var empresas = await _service.GetEmpresas();
            var empresasDto = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaReturn>>(empresas);
            return Ok(empresasDto);
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

        //No aplica
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