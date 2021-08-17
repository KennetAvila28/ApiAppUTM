using AppUTM.Api.DTOS.HistorialCupones;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class HistorialCuponController : ControllerBase
    {
        private readonly IHistorialCuponesServices _service;
        private readonly ICuponGenericoServices _genericosService;
        private readonly ICuponImagenServices _cuponImagenService;
        private readonly IMapper _mapper;
        //private readonly string contenedor = "Empresas";

        public HistorialCuponController(IHistorialCuponesServices services, IMapper mapper, ICuponGenericoServices generico, ICuponImagenServices cuponImagen)
        {
            this._service = services;
            this._mapper = mapper;
            this._genericosService = generico;
            this._cuponImagenService = cuponImagen;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCuponesReturn>>> Get()
        {
            var historialCupones = await _service.GetHistorialCupones();
            var historialcuponesDto = _mapper.Map<IEnumerable<HistorialCupones>, IEnumerable<HistorialCuponesReturn>>(historialCupones);
            var historialcuponesJson = JsonSerializer.Serialize(historialcuponesDto);
            return Ok(historialcuponesJson);
        }

        [HttpGet("HistorialCupones")]
        public async Task<ActionResult<IEnumerable<HistorialCuponesReturn>>> GetAll()
        {
            var historialCupones = await _service.GetHistorialCupones();
            var historialcuponesDto = _mapper.Map<IEnumerable<HistorialCupones>, IEnumerable<HistorialCuponesReturn>>(historialCupones);
            var historialcuponesJson = JsonSerializer.Serialize(historialcuponesDto);
            return Ok(historialcuponesJson);
        }        

    }
}
