using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEvents();

        Task<Event> GetEventById(int id);

        Task<Event> CreateEvent(Event newEvent);

        Task UpdateEvent(Event eventToBeUpdated);

        Task DeleteEvent(Event @event);
    }
}