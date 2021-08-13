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
using AppUTM.Data.Repositories;
using AppUTM.Data;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _Moduleservice;

        private readonly IMapper _mapper;

        private readonly IAuthorizationServices _authorization;


        public ModuleController(IMapper mapper, IModuleService ModuleService, IAuthorizationServices authorization)
        {
            _mapper = mapper;
            _Moduleservice = ModuleService;
            _authorization = authorization;

        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleReturn>>> Get()
        {
            Request.Headers.TryGetValue("UserUTM", out var userEmail);

            Console.WriteLine(userEmail);

            var result = _authorization.ValidateUser(userEmail, "Seguridad", false);
            if (!result)
            {
                return NotFound();
            }

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
            Request.Headers.TryGetValue("UserUTM", out var userEmail);

            Console.WriteLine(userEmail);

            var result = _authorization.ValidateUser(userEmail, "Seguridad", false);
            if (!result)
            {
                return NotFound();
            }

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

            Request.Headers.TryGetValue("UserUTM", out var userEmail);

            Console.WriteLine(userEmail);

            var result = _authorization.ValidateUser(userEmail, "Seguridad", true);
            if (!result)
            {
                return NotFound();
            }

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
            Request.Headers.TryGetValue("UserUTM", out var userEmail);

            Console.WriteLine(userEmail);

            var result = _authorization.ValidateUser(userEmail, "Seguridad", true);
            if (!result)
            {
                return NotFound();
            }

            try
            {
                var moduleToBeUpdate = await _Moduleservice.GetModuleById(id);
                var moduleForUpdate = _mapper.Map<moduleToBeUpdated, Module>(moduleToBeUpdated);
                if (moduleToBeUpdate == null)
                    return NotFound();
                await _Moduleservice.UpdateModule(moduleToBeUpdate, moduleForUpdate);
                var result2 = new ApiResponse<bool>(true);
                return Ok(result2);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Request.Headers.TryGetValue("UserUTM", out var userEmail);

            Console.WriteLine(userEmail);

            var result = _authorization.ValidateUser(userEmail, "Seguridad", true);
            if (!result)
            {
                return NotFound();
            }

            var module = await _Moduleservice.GetModuleById(id);
            if (module == null) return NotFound();
            await _Moduleservice.DeleteModule(module);
            return Ok(true);
        }
    }
}
