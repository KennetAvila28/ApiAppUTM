using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppUTM.Api.DTOS.Modules;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        private readonly IMapper _mapper;

        public ModuleController(IMapper mapper, IModuleService Moduleervice)
        {
            _mapper = mapper;
            _moduleService = Moduleervice;
        }

        // GET: api/<ModuleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleReturn>>> Get()
        {
            var Module = await _moduleService.GetAllModules();
            var ModuleList = _mapper.Map<IEnumerable<Module>, IEnumerable<ModuleReturn>>(Module);
            var response = new ApiResponse<IEnumerable<ModuleReturn>>(ModuleList);
            return Ok(response);
        }

        // GET api/<ModuleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var Module = await _moduleService.GetModuleById(id);
            var Moduledto = _mapper.Map<Module, ModuleReturn>(Module);
            var response = new ApiResponse<ModuleReturn>(Moduledto);
            return Ok(response);
        }

        // POST api/<ModuleController>
        [HttpPost]
        public async Task<ActionResult> Post(ModuleCreate ModuleCreate)
        {
            var Module = _mapper.Map<ModuleCreate, Module>(ModuleCreate);
            await _moduleService.CreateModule(Module);
            var ModuleReturn = _mapper.Map<Module, ModuleReturn>(Module);
            var response = new ApiResponse<ModuleReturn>(ModuleReturn);
            return Ok(response);
        }

        //// PUT api/<ModuleController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ModuleForUpdateDto ModuleForUpdateDto)
        {
            var ModuleForUpdate = _mapper.Map<Module>(ModuleForUpdateDto);
            ModuleForUpdate.Id = id;
            await _moduleService.UpdateModule(ModuleForUpdate);
            var result = new ApiResponse<bool>(true);
            return Ok(result);
        }
    }
}