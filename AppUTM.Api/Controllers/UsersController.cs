using System;
using AppUTM.Core.Models;
using AppUTM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppUTM.Api.DTOS.Roles;
using AppUTM.Api.DTOS.Users;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Text.Json;
using Hangfire.Server;
using System.Text.RegularExpressions;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _Userervice;

        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserService Userervice)
        {
            _mapper = mapper;
            _Userervice = Userervice;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReturn>>> Get()
        {
            try
            {
                var User = await _Userervice.GetAllUsers();
                var UserList = _mapper.Map<IEnumerable<User>, IEnumerable<UserReturn>>(User);
                var response = new ApiResponse<IEnumerable<UserReturn>>(UserList);
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
                var user = await _Userervice.GetUserById(id);
                var Userdto = _mapper.Map<User, UserReturn>(user);
                var response = new ApiResponse<UserReturn>(Userdto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("empleado/{correo}")]
        public async Task<ActionResult> Get(string correo)
        {
            try
            {
                var Client = new HttpClient();
                var Url = "http://api.utmetropolitana.edu.mx/api/Empleados/Get?correoinstitucional=";
                var jsonstring = await Client.GetStringAsync(Url + correo);
                var jsonclean = Regex.Replace(jsonstring, @"\s{2,}|//", " ").Substring(1);
                jsonclean = jsonclean.Remove(jsonclean.Length - 1).Replace(@"\", "").Replace("\u0022", "");
                var jsonempleados = jsonclean.Replace("ClaveEmpleado:", "\u0022ClaveEmpleado\u0022:\u0022").Replace(",PrimerNombre:", "\u0022,\u0022PrimerNombre\u0022:\u0022").Replace(",SegundoNombre:", "\u0022,\u0022SegundoNombre\u0022:\u0022").Replace(",PrimerApellido:", "\u0022,\u0022PrimerApellido\u0022:\u0022")
                .Replace(",SegundoApellido:", "\u0022,\u0022SegundoApellido\u0022:\u0022").Replace(",CorreoInstitucional:", "\u0022,\u0022CorreoInstitucional\u0022:\u0022").Replace(",NombreArea:", "\u0022,\u0022NombreArea\u0022:\u0022").Replace(",Departamento:", "\u0022,\u0022Departamento\u0022:\u0022").Replace(",TipoEmpleado:", "\u0022,\u0022TipoEmpleado\u0022:\u0022").Replace("}", "\u0022}");
                return Ok(jsonempleados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post(UserCreate userCreate)
        {
            try
            {
                var user = _mapper.Map<UserCreate, User>(userCreate);
                await _Userervice.CreateUser(user);
                var userReturn = _mapper.Map<User, UserReturn>(user);
                var response = new ApiResponse<UserReturn>(userReturn);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserForUpdateDto userForUpdateDto)
        {
            try
            {
                var userToBeUpdate = await _Userervice.GetUserById(id);
                var userForUpdate = _mapper.Map<UserForUpdateDto, User>(userForUpdateDto);
                if (userToBeUpdate == null)
                    return NotFound();
                await _Userervice.UpdateUser(userToBeUpdate, userForUpdate);
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