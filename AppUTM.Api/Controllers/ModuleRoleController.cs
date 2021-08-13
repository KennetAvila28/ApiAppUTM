using AppUTM.Api.DTOS.Modules;
using AppUTM.Api.DTOS.ModulesRoles;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleRoleController : ControllerBase
    {
        private readonly IModuleRoleService _Moduleservice;

        private readonly IMapper _mapper;

        public ModuleRoleController(IMapper mapper, IModuleRoleService ModuleService)
        {
            _mapper = mapper;
            _Moduleservice = ModuleService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleRole>>> Get()
        {
            try
            {
                var Roles = await _Moduleservice.GetAllRoles();

                var Module = await _Moduleservice.GetAllModulesRoles();
                var ModuleList = _mapper.Map<IEnumerable<ModuleRole>, IEnumerable<ModuleRoleReturn>>(Module);
                var response = new ApiResponse<IEnumerable<ModuleRoleReturn>>(ModuleList);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{ModuleId:int}/{RoleId:int}")]
        public async Task<ActionResult> Get(int ModuleId, int RoleId)
        {
            try
            {
                var module = await _Moduleservice.GetModuleRoleById(ModuleId, RoleId);
                var Moduledto = _mapper.Map<ModuleRole, ModuleRoleReturn>(module);
                var response = new ApiResponse<ModuleRoleReturn>(Moduledto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post(ModuleRoleCreate moduleCreate)
        {
            try
            {
                var module = _mapper.Map<ModuleRoleCreate, ModuleRole>(moduleCreate);
                await _Moduleservice.CreateModuleRole(module);
                var moduleReturn = _mapper.Map<ModuleRole, ModuleRoleReturn>(module);
                var response = new ApiResponse<ModuleRoleReturn>(moduleReturn);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// PUT api/<UserController>/5
        [HttpPut("{ModuleId:int}/{RoleId:int}")]
        public async Task<ActionResult> Put(int ModuleId, int RoleId, ModuleRoleUpdate moduleRoleUpdate)
        {
            try
            {
                var moduleToBeUpdate = await _Moduleservice.GetModuleRoleById(ModuleId, RoleId);
                var moduleRoleUpdated = _mapper.Map<ModuleRoleUpdate, ModuleRole>(moduleRoleUpdate);
                if (moduleToBeUpdate == null)
                    return NotFound();
                await _Moduleservice.UpdateModuleRole(moduleToBeUpdate, moduleRoleUpdated);
                var result = new ApiResponse<bool>(true);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        //[HttpDelete("{ModuleId:int}/{RoleId:int}")]
        //public async Task<ActionResult> Delete(int ModuleId, int RoleId, ModuleRoleReturn moduleRole)
        //{
        //    var user = await _Moduleservice.GetModuleRoleById(ModuleId, RoleId);
        //    var moduleRoleUpdated = _mapper.Map<ModuleRoleReturn, ModuleRole>(moduleRole);

        //    if (moduleRoleUpdated == null) return NotFound();
        //    await _Moduleservice.DeleteModuleRole(moduleRoleUpdated);
        //    return Ok(true);
        //}


        [HttpDelete("{ModuleId:int}/{RoleId:int}")]
        public async Task<ActionResult> Delete(int ModuleId, int RoleId)
        {
            try
            {
                var moduleRoleToBeDelete = await _Moduleservice.GetModuleRoleById(ModuleId, RoleId);
               // var moduleRoleDeleted = _mapper.Map<ModuleRoleReturn, ModuleRole>(moduleToBeDelete);
                if (moduleRoleToBeDelete == null)
                    return NotFound();
                await _Moduleservice.DeleteModuleRole(moduleRoleToBeDelete);
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
