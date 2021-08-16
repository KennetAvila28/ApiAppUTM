using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEvents();

        Task<Event> GetEventById(int id);

        Task<Event> CreateEvent(Event newEvent);

        Task UpdateEvent(Event eventToBeUpdated);

        Task PublishEvent(int id);

        Task PassedEvent(int id);

        Task RechazedEvent(int id);

        Task RevisedEvent(int id);

        Task DeleteEvent(Event @event);

        Task<IEnumerable<Event>> GetAllEventsToday();

        Task<IEnumerable<Event>> GetAllEventsWeek();

        Task<IEnumerable<Event>> GetAllEventsQuarter();

        Task<IEnumerable<Event>> GetAllEventsYear();

        IEnumerable<Event> GetRechazedEvents();

        IEnumerable<Event> GetPassedEvents();

        IEnumerable<Event> GetPublishedEvents();

        IEnumerable<Event> GetRevisedEvents();
    }
}