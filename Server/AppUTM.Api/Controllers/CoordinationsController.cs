using AppUTM.Api.DTOS.Coordinations;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICoordinationService _CoordinationService;

        public CoordinationsController(IMapper mapper, ICoordinationService CoordinationService)
        {
            _mapper = mapper;
            _CoordinationService = CoordinationService;
        }

        // GET: api/<CoordinationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coordination>>> Get()
        {
            try
            {
                var Coordinations = await _CoordinationService.GetAllCoordinations();
                var CoordinationList = _mapper.Map<IEnumerable<Coordination>, IEnumerable<CoordinationReturn>>(Coordinations);
                var response = new ApiResponse<IEnumerable<CoordinationReturn>>(CoordinationList);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<CoordinationController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Coordination>> Get(int id)
        {
            try
            {
                var selectedCoordination = await _CoordinationService.GetCoordinationById(id);
                var CoordinationDto = _mapper.Map<Coordination, CoordinationReturn>(selectedCoordination);
                var response = new ApiResponse<CoordinationReturn>(CoordinationDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<CoordinationController>
        [HttpPost]
        public async Task<ActionResult> Post(CoordinationCreate CoordinationCreate)
        {
            try
            {
                var CoordinationEntity = _mapper.Map<CoordinationCreate, Coordination>(CoordinationCreate);
                await _CoordinationService.CreateCoordination(CoordinationEntity);
                var CoordinationResponse = _mapper.Map<Coordination, CoordinationReturn>(CoordinationEntity);
                var response = new ApiResponse<CoordinationReturn>(CoordinationResponse);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CoordinationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CoordinationForUpdateDto CoordinationForUpdateDto)
        {
            try
            {
                var CoordinationForUpdate = _mapper.Map<CoordinationForUpdateDto, Coordination>(CoordinationForUpdateDto);

                CoordinationForUpdate.Id = id;
                await _CoordinationService.UpdateCoordination(CoordinationForUpdate);
                var CoordinationResponse = _mapper.Map<Coordination, CoordinationReturn>(CoordinationForUpdate);
                var response = new ApiResponse<CoordinationReturn>(CoordinationResponse);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<CoordinationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var CoordinationForDelete = await _CoordinationService.GetCoordinationById(id);
                await _CoordinationService.DeleteCoordination(CoordinationForDelete);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}