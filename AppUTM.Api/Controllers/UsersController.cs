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
            var User = await _Userervice.GetAllUsers();
            var UserList = _mapper.Map<IEnumerable<User>, IEnumerable<UserReturn>>(User);
            var response = new ApiResponse<IEnumerable<UserReturn>>(UserList);
            return Ok(response);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var user = await _Userervice.GetUserById(id);
            var Userdto = _mapper.Map<User, UserReturn>(user);
            var response = new ApiResponse<UserReturn>(Userdto);
            return Ok(response);
        }

        [HttpGet("empleado/{correo}")]
        public async Task<ActionResult> Get(string correo)
        {
            var Url = "http://api.utmetropolitana.edu.mx/api/Empleados/Get?correoinstitucional=";
            var Client = new HttpClient();
            var json = await Client.GetStringAsync(Url + correo);
            return Ok(json);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post(UserCreate userCreate)
        {
            var user = _mapper.Map<UserCreate, User>(userCreate);
            await _Userervice.CreateUser(user);
            var userReturn = _mapper.Map<User, UserReturn>(user);
            var response = new ApiResponse<UserReturn>(userReturn);
            return Ok(response);
        }

        //// PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserForUpdateDto userForUpdateDto)
        {
            var userToBeUpdate = await _Userervice.GetUserById(id);
            var userForUpdate = _mapper.Map<User>(userForUpdateDto);
            if (userToBeUpdate == null)
                return NotFound();
            await _Userervice.UpdateUser(userToBeUpdate, userForUpdate);
            var result = new ApiResponse<bool>(true);
            return Ok(result);
        }
    }
}