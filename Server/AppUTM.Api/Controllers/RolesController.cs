using AppUTM.Api.DTOS.Roles;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;

        public RolesController(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleReturn>>> Get()
        {
            var roles = await _roleService.GetAllRoles();
            var roleList = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleReturn>>(roles);
            var response = new ApiResponse<IEnumerable<RoleReturn>>(roleList);
            return Ok(response);
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var role = await _roleService.GetRoleById(id);
            var roleDto = _mapper.Map<Role, RoleReturn>(role);
            var response = new ApiResponse<RoleReturn>(roleDto);
            return Ok(response);
        }

        // POST api/<RolesController>
        [HttpPost]
        public async Task<ActionResult> Post(RoleCreate rolesCreate)
        {
            var role = _mapper.Map<RoleCreate, Role>(rolesCreate);
            await _roleService.CreateRole(role);
            var roleReturn = _mapper.Map<Role, RoleReturn>(role);
            var response = new ApiResponse<RoleReturn>(roleReturn);
            return Ok(response);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, RoleForUpdateDto roleForUpdateDto)
        {
            var roleForUpdate = _mapper.Map<Role>(roleForUpdateDto);
            roleForUpdate.Id = id;
            await _roleService.UpdateRole(roleForUpdate);
            var result = new ApiResponse<bool>(true);
            return Ok(result);
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var role = await _roleService.GetRoleById(id);
                await _roleService.DeleteRole(role);
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