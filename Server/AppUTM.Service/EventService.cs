using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public EventService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEvents() => await _context.Events.Include(x => x.Author).ToListAsync();

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Event> CreateEvent(Event newEvent)
        {
            newEvent.Image ??= ImageNotAviable.NotAviable;
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

        public IEnumerable<Event> GetRechazedEvents() => _unitOfWork.Events.Find(x => x.IsPassed == false).OrderBy(x => x.StartDate).ToList();

        public IEnumerable<Event> GetPassedEvents() => _unitOfWork.Events.Find(x => x.IsPassed == true).OrderBy(x => x.StartDate).ToList();

        public async Task<IEnumerable<Event>> GetAllEventsToday()
        {
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.IsPublished == true && x.StartDate == DateTime.Today).OrderBy(x => x.StartDate).ToList();
        }

        public async Task<IEnumerable<Event>> GetAllEventsWeek()
        {
            var sunday = DayOfWeek.Sunday - DateTime.Now.DayOfWeek;
            var saturday = DayOfWeek.Saturday - DateTime.Now.DayOfWeek;
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.StartDate.Date >= DateTime.Now.AddDays(sunday) && x.StartDate <= DateTime.Now.AddDays(saturday) && x.IsPublished == true).ToList();
        }

        public async Task<IEnumerable<Event>> GetAllEventsQuarter()
        {
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.StartDate.Month >= DateTime.Now.Month && x.StartDate.Month <= DateTime.Now.Month + 3 && x.IsPublished).OrderBy(x => x.StartDate).ToList();
        }

        public async Task<IEnumerable<Event>> GetAllEventsYear()
        {
            await Task.Delay(1000);
            return _unitOfWork.Events.Find(x => x.StartDate.Year == DateTime.Now.Year && x.IsPublished).OrderBy(x => x.StartDate).ToList();
        }
    }
}