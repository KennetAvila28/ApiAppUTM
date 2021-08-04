using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;

namespace AppUTM.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //TODO: Get all events with coordinators

        public async Task<IEnumerable<Event>> GetAllEvents() => await _unitOfWork.Events.GetAll();

        public async Task<Event> GetEventById(int id)
        {
            return await _unitOfWork.Events.GetById(id);
        }

        public async Task<Event> CreateEvent(Event newEvent)
        {
            await _unitOfWork.Events.Add(newEvent);
            await _unitOfWork.CommitAsync();
            return newEvent;
        }

        public async Task UpdateEvent(Event eventToBeUpdated)
        {
            eventToBeUpdated.UpdateAt = DateTime.Now;
            _unitOfWork.Events.Update(eventToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEvent(Event @event)
        {
            _unitOfWork.Events.Remove(@event);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEventsToday()
        {
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.IsPublished & x.StartDate == DateTime.Today);
        }

        public async Task<IEnumerable<Event>> GetAllEventsWeek()
        {
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.StartDate.DayOfWeek >= DayOfWeek.Monday & x.StartDate.DayOfWeek <= DayOfWeek.Sunday & x.IsPublished);
        }

        public async Task<IEnumerable<Event>> GetAllEventsQuarter()
        {
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.StartDate.Month >= DateTime.Now.Month & x.StartDate.Month <= DateTime.Now.Month & x.IsPublished);
        }

        public async Task<IEnumerable<Event>> GetAllEventsYear()
        {
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.StartDate.Year == DateTime.Now.Year & x.IsPublished);
        }
    }
}