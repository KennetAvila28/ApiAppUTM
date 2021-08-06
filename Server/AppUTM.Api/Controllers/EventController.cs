using AppUTM.Api.DTOS.Events;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        //TODO:add get rechazed, passed and published events
        private readonly IMapper _mapper;

        private readonly IEventService _eventService;

        public EventController(IMapper mapper, IEventService eventService)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        // GET: api/<EventController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            try
            {
                var events = await _eventService.GetAllEvents();
                var eventList = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(events);
                var response = new ApiResponse<IEnumerable<EventReturn>>(eventList);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<EventController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            try
            {
                var selectedEvent = await _eventService.GetEventById(id);
                var eventDto = _mapper.Map<Event, EventReturn>(selectedEvent);
                var response = new ApiResponse<EventReturn>(eventDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Get api/<EventController>/GetToday
        [AllowAnonymous]
        [HttpGet("GetToday")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventsToday()
        {
            var eventsToday = await _eventService.GetAllEventsToday();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsToday);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        // Get api/<EventController>/GetWeek
        [AllowAnonymous]
        [HttpGet("GetWeek")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventsWeek()
        {
            var eventsWeek = await _eventService.GetAllEventsWeek();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsWeek);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        // Get api/<EventController>/GetQuarter
        [AllowAnonymous]
        [HttpGet("GetQuarter")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventsQuarter()
        {
            var eventsQuarter = await _eventService.GetAllEventsQuarter();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsQuarter);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        // Get api/<EventController>/GetYear
        [AllowAnonymous]
        [HttpGet("GetYear")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventsYear()
        {
            var eventsYear = await _eventService.GetAllEventsYear();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsYear);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        [HttpGet("GetPassed")]
        public ActionResult<IEnumerable<Event>> GetAllEventsPassed()
        {
            var eventsPassed = _eventService.GetPassedEvents();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsPassed);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        [HttpGet("GetRevised")]
        public ActionResult<IEnumerable<Event>> GetAllEventsRevised()
        {
            var eventsPassed = _eventService.GetRevisedEvents();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsPassed);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        [HttpGet("GetPublished")]
        public ActionResult<IEnumerable<Event>> GetAllEventsPublished()
        {
            var eventsPublished = _eventService.GetPublishedEvents();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsPublished);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        [HttpGet("GetRechazed")]
        public ActionResult<IEnumerable<Event>> GetAllEventsRechazed()
        {
            var eventsRechazed = _eventService.GetRechazedEvents();
            var eventDto = _mapper.Map<IEnumerable<Event>, IEnumerable<EventReturn>>(eventsRechazed);
            var response = new ApiResponse<IEnumerable<EventReturn>>(eventDto);
            return Ok(response);
        }

        [HttpPost("Rechazed")]
        public async Task<ActionResult<Event>> Rechazed([FromBody] int id)
        {
            try
            {
                await _eventService.RechazedEvent(id);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Published")]
        public async Task<ActionResult<Event>> Published([FromBody] int id)
        {
            try
            {
                await _eventService.PublishEvent(id);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Passed")]
        public async Task<ActionResult<Event>> Passed([FromBody] int id)
        {
            try
            {
                await _eventService.PassedEvent(id);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Revised")]
        public async Task<ActionResult<Event>> Revised([FromBody] int id)
        {
            try
            {
                await _eventService.RevisedEvent(id);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<EventController>
        [HttpPost]
        public async Task<ActionResult> Post(EventCreate eventCreate)
        {
            try
            {
                var eventEntity = _mapper.Map<EventCreate, Event>(eventCreate);
                await _eventService.CreateEvent(eventEntity);
                var eventResponse = _mapper.Map<Event, EventReturn>(eventEntity);
                var response = new ApiResponse<EventReturn>(eventResponse);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EventForUpdateDto eventForUpdateDto)
        {
            try
            {
                var eventForUpdate = _mapper.Map<EventForUpdateDto, Event>(eventForUpdateDto);

                eventForUpdate.Id = id;
                await _eventService.UpdateEvent(eventForUpdate);
                var eventResponse = _mapper.Map<Event, EventReturn>(eventForUpdate);
                var response = new ApiResponse<EventReturn>(eventResponse);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var eventForDelete = await _eventService.GetEventById(id);
                await _eventService.DeleteEvent(eventForDelete);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}