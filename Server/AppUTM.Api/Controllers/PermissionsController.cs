using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppUTM.Api.DTOS.Permissions;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _Permissionervice;

        private readonly IMapper _mapper;

        public PermissionsController(IMapper mapper, IPermissionService Permissionervice)
        {
            _mapper = mapper;
            _Permissionervice = Permissionervice;
        }

        // GET: api/<PermissionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionReturn>>> Get()
        {
            var Permission = await _Permissionervice.GetAllPermissions();
            var PermissionList = _mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionReturn>>(Permission);
            var response = new ApiResponse<IEnumerable<PermissionReturn>>(PermissionList);
            return Ok(response);
        }

        // GET api/<PermissionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var Permission = await _Permissionervice.GetPermissionById(id);
            var Permissiondto = _mapper.Map<Permission, PermissionReturn>(Permission);
            var response = new ApiResponse<PermissionReturn>(Permissiondto);
            return Ok(response);
        }

        // POST api/<PermissionController>
        [HttpPost]
        public async Task<ActionResult> Post(PermissionCreate PermissionCreate)
        {
            var Permission = _mapper.Map<PermissionCreate, Permission>(PermissionCreate);
            await _Permissionervice.CreatePermission(Permission);
            var PermissionReturn = _mapper.Map<Permission, PermissionReturn>(Permission);
            var response = new ApiResponse<PermissionReturn>(PermissionReturn);
            return Ok(response);
        }

        //// PUT api/<PermissionController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, PermissionForUpdateDto PermissionForUpdateDto)
        {
            var PermissionToBeUpdate = await _Permissionervice.GetPermissionById(id);
            var PermissionForUpdate = _mapper.Map<Permission>(PermissionForUpdateDto);
            if (PermissionToBeUpdate == null)
                return NotFound();
            await _Permissionervice.UpdatePermission(PermissionToBeUpdate, PermissionForUpdate);
            var result = new ApiResponse<bool>(true);
            return Ok(result);
        }
    }
}