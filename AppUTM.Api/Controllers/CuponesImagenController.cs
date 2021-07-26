using AppUTM.Api.DTOS.Cupones;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponesImagenController : ControllerBase
    {
        private readonly ICuponImagenServices _service;
        private readonly IMapper _mapper;
        public readonly IAlmacenarImagen _almacenarImagen;
        private readonly string contenedor = "Cupones";

        public CuponesImagenController(ICuponImagenServices services, IMapper mapper, IAlmacenarImagen almacenarImagen)
        {
            this._service = services;
            this._mapper = mapper;
            this._almacenarImagen = almacenarImagen;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuponImagen>>> Get()
        {
            var cupones = await _service.GetCuponesImagen();
            var cuponesDto = _mapper.Map<IEnumerable<CuponImagen>, IEnumerable<CuponImagenReturn>>(cupones);
            return Ok(cuponesDto);
        }

        [HttpGet("empresa/{id:int}")]
        public ActionResult<IEnumerable<CuponImagen>> GetCuponEmpresa(int id)
        {
            var cupones = _service.GetCuponesImagenEmpresa(id);
            var cuponesDto = _mapper.Map<IEnumerable<CuponImagen>, IEnumerable<CuponImagenReturn>>(cupones);
            return Ok(cuponesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CuponImagen>> Details(int id)
        {
            var cupon = await _service.GetCuponImagen(id);
            var cuponDto = _mapper.Map<CuponImagen, CuponImagenReturn>(cupon);
            return Ok(cuponDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CuponImagenCreate cuponDto)
        {
            var cupon = _mapper.Map<CuponImagenCreate, CuponImagen>(cuponDto);
            if (cuponDto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await cuponDto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    cupon.Imagen = await _almacenarImagen.GuardarArchivo(contenido, contenedor, cuponDto.Foto.FileName);
                }
            }
            await _service.AddCuponImagen(cupon);
            var cuponResponseDto = _mapper.Map<CuponImagen, CuponImagenReturn>(cupon);
            return Ok(cuponResponseDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromForm] CuponImagenCreate cuponDto)
        {
            var cuponData = await _service.GetCuponImagen(id);
            if (cuponData == null) return NotFound();
            if (cuponData.Imagen != null)
                await _almacenarImagen.BorraArchivo(cuponData.Imagen, contenedor);
            var cupon = _mapper.Map(cuponDto, cuponData);
            if (cuponDto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await cuponDto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    cuponData.Imagen = await _almacenarImagen.EditarArchivo(contenido, contenedor, cuponData.Imagen);
                }
            }
            await _service.UpdateCuponImagen(cupon);
            return Ok(cupon);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cuponData = await _service.GetCuponImagen(id);
            if (cuponData == null) return NotFound();

            if (cuponData.Imagen != null)
                await _almacenarImagen.BorraArchivo(cuponData.Imagen, contenedor);

            await _service.DeleteCuponImagen(cuponData);
            return NoContent();
        }
    }
}