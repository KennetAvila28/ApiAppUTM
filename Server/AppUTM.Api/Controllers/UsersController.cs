using System;
using AppUTM.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppUTM.Api.DTOS.Users;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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
                var userForUpdate = _mapper.Map<User>(userForUpdateDto);
                userForUpdate.Id = id;
                if (userForUpdate == null)
                    return NotFound();
                await _Userervice.UpdateUser(userForUpdate);
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

                await _Userervice.DeleteUser(await _Userervice.GetUserById(id));
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