using AppUTM.Api.DTOS;
using AppUTM.Api.DTOS.Users;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppUTM.Api.Helpers;
using IAuthorizationService = AppUTM.Core.Interfaces.IAuthorizationService;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorization;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserService userService, IAuthorizationService authorization)
        {
            _mapper = mapper;
            _userService = userService;
            _authorization = authorization;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReturn>>> Get()
        {
            try
            {
                var User = await _userService.GetAllUsers();
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
                var user = await _userService.GetUserById(id);
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
        [HttpPost("student")]
        public async Task<ActionResult> GetGraphUser(Token token)
        {
            const string url = "https://graph.microsoft.com/v1.0/me";
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Code);
                var json = await client.GetStringAsync(url);
                return Ok(json);
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
                var Url = "http://api.utmetropolitana.edu.mx/api/Empleados/Get?correoinstitucional=";
                var Client = new HttpClient();
                var jsonString = await Client.GetFromJsonAsync<string>(Url + correo);
                var json = JObject.Parse(jsonString.Replace("[", "").Replace("]", ""));
                return Ok(json);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("worker/{correo}")]
        public async Task<ActionResult> GetWorker(string correo)
        {
            try
            {
                var Url = "http://api.utmetropolitana.edu.mx/api/Empleados/Get?correoinstitucional=";
                var Client = new HttpClient();
                var json = await Client.GetStringAsync(Url + correo);
                return Ok(json);
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
                await _userService.CreateUser(user);
                var userReturn = _mapper.Map<User, UserReturn>(user);
                var response = new ApiResponse<UserReturn>(userReturn);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<UserController>
        [AllowAnonymous]
        [HttpGet("Authorize/{correo}")]
        public ActionResult Authorize(string correo)
        {
            if (!_authorization.Login(correo))
                return Unauthorized();
            return Ok();
        }

        // POST api/<UserController>
        [AllowAnonymous]
        [HttpGet("ByEmail/{correo}")]
        public ActionResult GetByEmail(string correo)
        {
            var user = _authorization.GetUserByEmail(correo);
            var mapper = _mapper.Map<User, UserReturn>(user);
            var response = new ApiResponse<UserReturn>(mapper);
            return Ok(response);
        }

        //// PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserForUpdateDto userForUpdateDto)
        {
            try
            {
                var userForUpdate = _mapper.Map<User>(userForUpdateDto);
                userForUpdate.Id = id;
                await _userService.UpdateUser(userForUpdate);
                var result = new ApiResponse<bool>(true);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUser(await _userService.GetUserById(id));
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