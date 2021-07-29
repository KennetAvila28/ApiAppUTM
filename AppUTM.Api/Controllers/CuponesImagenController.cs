using AppUTM.Api.DTOS.Cupones;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IWebHostEnvironment _env;

        public CuponesImagenController(ICuponImagenServices services, IMapper mapper, IAlmacenarImagen almacenarImagen, IWebHostEnvironment environment)
        {
            this._service = services;
            this._mapper = mapper;
            this._almacenarImagen = almacenarImagen;
            this._env = environment;
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
            string startupPath = Environment.CurrentDirectory;
            var cupon = await _service.GetCuponImagen(id);
            var cuponDto = _mapper.Map<CuponImagen, CuponImagenReturn>(cupon);
            if (cupon != null) cuponDto.Domain = startupPath;
            return Ok(cuponDto);
        }

        [HttpGet("apply/{id:int}")]
        public async Task<ActionResult<CuponImagen>> VerCupon(int id)
        {
            var cupon = await _service.GetCuponImagen(id);
            if (cupon == null) return NotFound();
            cupon.CuponesVisitados++;
            await _service.UpdateCuponImagen(cupon);
            var cuponDto = _mapper.Map<CuponImagen, CuponImagenReturn>(cupon);
            return Ok(cuponDto);
        }

        [HttpPut("apply")]
        public async Task<ActionResult> AplicarCupon(int id, CuponImagen cupon)
        {
            var cuponData = await _service.GetCuponImagen(id);
            if (cuponData == null) return NotFound();
            cuponData.CuponesUsados++;
            await _service.UpdateCuponImagen(cuponData);
            return Ok(cuponData);
        }

        [HttpPost]
        public async Task<ActionResult> Post (CuponImagenCreate cuponDto)
        {
            var cupon = _mapper.Map<CuponImagenCreate, CuponImagen>(cuponDto);           
            await _service.AddCuponImagen(cupon);
            var cuponResponseDto = _mapper.Map<CuponImagen, CuponImagenReturn>(cupon);
            return Ok(cuponResponseDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, CuponImagenCreate cuponDto)
        {
            var cuponData = await _service.GetCuponImagen(id);
            if (cuponData == null) return NotFound();

            if (cuponData.Imagen != cuponDto.Imagen)
                await _almacenarImagen.BorraArchivo(cuponData.Imagen, contenedor);

            var cupon = _mapper.Map(cuponDto, cuponData);   
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
    }
}