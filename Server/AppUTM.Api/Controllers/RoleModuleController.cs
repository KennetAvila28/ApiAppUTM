using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Api.DTOS.RoleModule;
using AppUTM.Api.DTOS.Roles;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleModuleController : ControllerBase
    {
        private readonly IRoleModuleService _service;
        private readonly IMapper _mapper;

        public RoleModuleController(IRoleModuleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/<RoleModuleController>/5
        [HttpGet("{moduleId:int}/{roleId:int}")]
        public async Task<ActionResult<DTOS.RoleModule.RoleModuleReturn>> Get(int moduleId, int roleId)
        {
            try
            {
                var rolemodule = await _service.GetRoleModuleById(moduleId, roleId);
                var mapper = _mapper.Map<RoleModule, RoleModuleReturn>(rolemodule);
                var response = new ApiResponse<RoleModuleReturn>(mapper);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        [HttpGet("{roleId:int}")]
        public async Task<ActionResult<List<RoleModuleReturn>>> Get(int roleId)
        {
            try
            {
                var rolemodules = await _service.GetRoleModulesByRoleId(roleId);
                var mapper = _mapper.Map<IList<RoleModule>, IList<RoleModuleReturn>>(rolemodules);
                var response = new ApiResponse<IList<RoleModuleReturn>>(mapper);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // POST api/<RoleModuleController>
        [HttpPost]
        public async Task<ActionResult<RoleModuleReturn>> Post(List<RoleModuleCreate> roleModuleDto)
        {
            try
            {
                var rolemodules = _mapper.Map<List<RoleModuleCreate>, List<RoleModule>>(roleModuleDto);
                var entities = await _service.CreateRoleModule(rolemodules);
                var mapperResponse = _mapper.Map<IList<RoleModule>, IList<RoleModuleReturn>>(entities);
                var response = new ApiResponse<IList<RoleModuleReturn>>(mapperResponse);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // PUT api/<RoleModuleController>/5
        [HttpPut("{moduleId:int}/{roleId:int}")]
        public async Task<ActionResult> Put(int moduleId, int roleId, RoleModuleForUpdateDto roleModuleDto)
        {
            try
            {
                roleModuleDto.ModuleId = moduleId;
                roleModuleDto.RoleId = roleId;
                var entitie = _mapper.Map<RoleModuleForUpdateDto, RoleModule>(roleModuleDto);
                await _service.UpdateRoleModule(entitie);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // DELETE api/<RoleModuleController>/5
        [HttpDelete("{moduleId:int}/{roleId:int}")]
        public async Task<ActionResult> Delete(int moduleId, int roleId)
        {
            try
            {
                var rolemodule = await _service.GetRoleModuleById(moduleId, roleId);
                await _service.DeleteRoleModule(rolemodule);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }
    }
}