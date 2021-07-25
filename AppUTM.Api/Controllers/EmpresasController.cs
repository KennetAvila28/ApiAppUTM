using AppUTM.Api.DTOS.Empresas;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public EmpresasController(IEmpresaServices services, IMapper mapper, IAlmacenarImagen almacenarImagen)
        {
            this._service = services;
            this._mapper = mapper;
            this._almacenarImagen = almacenarImagen;
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
            var empresa = await _service.GetEmpresa(id);
            var empresaDto = _mapper.Map<Empresa, EmpresaReturn>(empresa);
            return Ok(empresaDto);
        }
    
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]EmpresaCreate empresaDto)
        {
            var empresa = _mapper.Map<EmpresaCreate, Empresa>(empresaDto);
            if (empresaDto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await empresaDto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    empresa.ImagenEmpresa = await _almacenarImagen.GuardarArchivo(contenido, contenedor, empresaDto.Foto.FileName);
                }
            }
            await _service.AddEmpresa(empresa);
            var empresaResponseDto = _mapper.Map<Empresa, EmpresaReturn>(empresa);
            return Ok(empresaResponseDto);          
        }
 
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromForm]EmpresaCreate empresaDto)
        {
            var empresaData = await _service.GetEmpresa(id);
            if (empresaData == null) return NotFound();
            if (empresaData.ImagenEmpresa != null)
                await _almacenarImagen.BorraArchivo(empresaData.ImagenEmpresa, contenedor);
            var empresa = _mapper.Map(empresaDto, empresaData);
            if (empresaDto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await empresaDto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    empresaData.ImagenEmpresa = await _almacenarImagen.EditarArchivo(contenido, contenedor, empresaData.ImagenEmpresa);
                }
            }
            await _service.UpdateEmpresa(empresa);
            return Ok(empresa);
        }
    }
}
