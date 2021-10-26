using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Event>> GetAllEvents() => await _context.Events.Include(x => x.Author).Where(x => x.IsPublished == false & x.IsRechazed == false & x.IsRevised == false).OrderByDescending(e => e.StartDate).ToListAsync();

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id);
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

        public async Task PublishEvent(int id)
        {
            try
            {
                var eventSelect = await _unitOfWork.Events.GetById(id);
                if (eventSelect.IsPassed)
                {
                    eventSelect.UpdateAt = DateTime.Now;
                    eventSelect.IsPublished = true;
                    eventSelect.IsPassed = false;
                    _unitOfWork.Events.Update(eventSelect);
                    await _unitOfWork.CommitAsync();
                }
            }
            catch
            {
                throw new ArgumentException("Evento no ha sido aprobado anteriormente");
            }
        }

        public async Task PassedEvent(int id)
        {
            try
            {
                var eventSelect = await _unitOfWork.Events.GetById(id);
                eventSelect.IsPassed = true;
                eventSelect.IsRevised = false;
                eventSelect.UpdateAt = DateTime.Now;
                _unitOfWork.Events.Update(eventSelect);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public async Task RechazedEvent(int id)
        {
            var eventSelect = await _unitOfWork.Events.GetById(id);
            eventSelect.UpdateAt = DateTime.Now;
            eventSelect.IsRevised = false;
            eventSelect.IsRechazed = true;
            _unitOfWork.Events.Update(eventSelect);
            await _unitOfWork.CommitAsync();
        }

        public async Task RevisedEvent(int id)
        {
            var eventSelect = await _unitOfWork.Events.GetById(id);
            eventSelect.UpdateAt = DateTime.Now;
            eventSelect.IsRevised = true;
            _unitOfWork.Events.Update(eventSelect);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEvent(Event @event)
        {
            _unitOfWork.Events.Remove(@event);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<Event> GetRechazedEvents() => _context.Events.OrderByDescending(x => x.StartDate).Include(x => x.Author).Where(x => x.IsPassed == false & x.IsRechazed).ToList();

        public IEnumerable<Event> GetPassedEvents() => _context.Events.OrderByDescending(x => x.StartDate).Include(x => x.Author).Where(x => x.IsPassed & x.IsRechazed == false).ToList();

        public IEnumerable<Event> GetPublishedEvents() => _context.Events.OrderByDescending(x => x.StartDate).Include(p => p.Favorites).Include(x => x.Author).Where(x => x.IsPassed == false && x.IsPublished && x.IsRechazed == false).ToList();

        public IEnumerable<Event> GetRevisedEvents() => _context.Events.OrderByDescending(x => x.StartDate).Include(x => x.Author).Where(x => x.IsRevised & x.IsRechazed == false).ToList();

        public async Task<IEnumerable<Event>> GetAllEventsToday()
        {
            await Task.Delay(1000);
            return _context.Events.Include(p => p.Favorites).Include(x => x.Author).Where(x => x.IsPublished && x.StartDate.Day == DateTime.Now.Day && x.StartDate.Year ==DateTime.Now.Year && x.StartDate.Month ==DateTime.Now.Month ).OrderByDescending(x => x.StartDate).ToList();
        }

        public async Task<IEnumerable<Event>> GetAllEventsWeek()
        {
            var sunday = DayOfWeek.Sunday - DateTime.Now.DayOfWeek;
            var saturday = DayOfWeek.Saturday - DateTime.Now.DayOfWeek;
            await Task.Delay(1000);
            return _context.Events.Include(p => p.Favorites).Include(x => x.Author).Where(x => x.StartDate.Date >= DateTime.Now.AddDays(sunday) && x.StartDate <= DateTime.Now.AddDays(saturday) && x.IsPublished == true).OrderByDescending(x => x.StartDate).ToList();
        }

        public async Task<IEnumerable<Event>> GetAllEventsQuarter()
        {
            await Task.Delay(1000);
            return _context.Events.Include(p => p.Favorites).Include(x => x.Author).Where(x => x.StartDate.Month >= DateTime.Now.Month && x.StartDate.Month <= DateTime.Now.Month + 3 && x.IsPublished).OrderByDescending(x => x.StartDate).ToList();
        }

        public async Task<IEnumerable<Event>> GetAllEventsYear()
        {
            await Task.Delay(1000);
            return _context.Events.Include(p => p.Favorites).Include(x => x.Author).Where(x => x.StartDate.Year == DateTime.Now.Year && x.IsPublished).OrderByDescending(x => x.StartDate).ToList();
        }
    }
}