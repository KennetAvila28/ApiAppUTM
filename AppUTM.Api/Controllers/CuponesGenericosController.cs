using AppUTM.Api.DTOS.Cupones;
using AppUTM.Api.DTOS.HistorialCupones;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponesGenericosController : ControllerBase
    {
        private readonly ICuponGenericoServices _service;
        private readonly IHistorialCuponesServices _historialService;
        private readonly IMapper _mapper;

        public CuponesGenericosController(ICuponGenericoServices services, IHistorialCuponesServices historial, IMapper mapper)
        {
            this._service = services;
            this._historialService = historial;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuponGenerico>>> Get()
        {
            var cupones = await _service.GetCuponesGenericos();
            var cuponesDto = _mapper.Map<IEnumerable<CuponGenerico>, IEnumerable<CuponGenericoReturn>>(cupones);
            return Ok(cuponesDto);
        }

        [HttpGet("empresa/{id:int}")]
        public ActionResult<IEnumerable<CuponGenerico>> GetCuponEmpresa(int id)
        {
            var cupones = _service.GetCuponGenericosEmpresa(id);
            var cuponesDto = _mapper.Map<IEnumerable<CuponGenerico>, IEnumerable<CuponGenericoReturn>>(cupones);
            return Ok(cuponesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CuponGenerico>> Details(int id)
        {
            var cupon = await _service.GetCuponGenerico(id);
            var cuponDto = _mapper.Map<CuponGenerico, CuponGenericoReturn>(cupon);
            return Ok(cuponDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CuponGenericoCreate cuponDto)
        {
            var cupon = _mapper.Map<CuponGenericoCreate, CuponGenerico>(cuponDto);
            await _service.AddCuponGenerico(cupon);
            var cuponResponseDto = _mapper.Map<CuponGenerico, CuponGenericoReturn>(cupon);
            return Ok(cuponResponseDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, CuponGenericoCreate cuponGenerico)
        {
            var cuponData = await _service.GetCuponGenerico(id);
            if (cuponData == null) return NotFound();
            var cupon = _mapper.Map(cuponGenerico, cuponData);
            await _service.UpdateCuponGenerico(cupon);
            return Ok(cupon);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cupon = await _service.GetCuponGenerico(id);
            if (cupon == null) return NotFound();
            await _service.DeleteCuponGenerico(cupon);
            return Ok(true);
        }


        //Métodos para la aplicación móvil

        //Contabiliza las visitas de todos los cupones de una empresa
        [HttpGet("apply/empresa/{id:int}")]
        public async Task<ActionResult<IEnumerable<CuponGenerico>>> ViewCoupons(int id)
        {
            var cupones = _service.GetCuponGenericosEmpresa(id);
            if (cupones != null)
            {
                for (int i = 0; i < cupones.Count(); i++)
                    cupones.ElementAt(i).CuponesVisitados++;
                await _service.UpdateRangeCupones(cupones);
            }
            var cuponesDto = _mapper.Map<IEnumerable<CuponGenerico>, IEnumerable<CuponGenericoReturn>>(cupones);
            return Ok(cuponesDto);
        }

        [HttpPut("apply")]
        public async Task<ActionResult> AplicarCupon(int id, HistorialCuponesCreate registroCupon)
        {
            var cuponData = await _service.GetCuponGenerico(id);
            if (cuponData == null) return NotFound();
            var registroCuponDto = _mapper.Map<HistorialCuponesCreate, HistorialCupones>(registroCupon);
            await _historialService.AddHistorialCupon(registroCuponDto);
            cuponData.CuponesUsados++;
            await _service.UpdateCuponGenerico(cuponData);
            return Ok(cuponData);
        }


        /*
        Contabiliza visitas por cupón
        [HttpGet("apply/{id:int}")]
        public async Task<ActionResult<CuponGenerico>> VerCupon(int id)
        {
            var cupon = await _service.GetCuponGenerico(id);
            if (cupon == null) return NotFound();
            cupon.CuponesVisitados++;
            await _service.UpdateCuponGenerico(cupon);
            var cuponDto = _mapper.Map<CuponGenerico, CuponGenericoReturn>(cupon);
            return Ok(cuponDto);
        }
        
        */
    }
}