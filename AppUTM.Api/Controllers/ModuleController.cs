using AppUTM.Api.DTOS.Modules;
using AppUTM.Core.Models;
using AppUTM.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Api.Responses;
using Microsoft.AspNetCore.Authorization;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _Moduleservice;

        private readonly IMapper _mapper;

        public ModuleController(IMapper mapper, IModuleService ModuleService)
        {
            _mapper = mapper;
            _Moduleservice = ModuleService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleReturn>>> Get()
        {
            try
            {
                var Module = await _Moduleservice.GetAllModules();
                var ModuleList = _mapper.Map<IEnumerable<Module>, IEnumerable<ModuleReturn>>(Module);
                var response = new ApiResponse<IEnumerable<ModuleReturn>>(ModuleList);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var module = await _Moduleservice.GetModuleById(id);
                var Moduledto = _mapper.Map<Module, ModuleReturn>(module);
                var response = new ApiResponse<ModuleReturn>(Moduledto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post(ModuleCreate moduleCreate)
        {
            try
            {
                var module = _mapper.Map<ModuleCreate, Module>(moduleCreate);
                await _Moduleservice.CreateModule(module);
                var moduleReturn = _mapper.Map<Module, ModuleReturn>(module);
                var response = new ApiResponse<ModuleReturn>(moduleReturn);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, moduleToBeUpdated moduleToBeUpdated)
        {
            try
            {
                var moduleToBeUpdate = await _Moduleservice.GetModuleById(id);
                var moduleForUpdate = _mapper.Map<moduleToBeUpdated, Module>(moduleToBeUpdated);
                if (moduleToBeUpdate == null)
                    return NotFound();
                await _Moduleservice.UpdateModule(moduleToBeUpdate, moduleForUpdate);
                var result = new ApiResponse<bool>(true);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
