using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppUTM.Api.DTOS.Events;
using AppUTM.Api.Helpers;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;

namespace AppUTM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public EventController(IMapper mapper, IEventService eventService)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        // GET: api/<EventController>
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

        // Get api/<EventController>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>> GetAllEventsToday()
        //{
        //}
        // Get api/<EventController>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>> GetAllEventsWeek()
        //{
        //}
        // Get api/<EventController>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>> GetAllEventsQuarter()
        //{
        //}
        // Get api/<EventController>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>> GetAllEventsYear()
        //{
        //}

        // POST api/<EventController>
        [HttpPost]
        public async Task<ActionResult> Post(EventCreate eventCreate)
        {
            try
            {
                var eventEntity = _mapper.Map<EventCreate, Event>(eventCreate);
                eventEntity.Image = ImageHelper.ImageToBase64(eventCreate.ImageFile);
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
                if (eventForUpdateDto.ImageFile != null)
                {
                    eventForUpdate.Image = ImageHelper.ImageToBase64(eventForUpdateDto.ImageFile);
                }
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