using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class EventFavoriteRepository : Repository<EventFavorite>, IEventFavoriteRepository
    {
        public EventFavoriteRepository(DbContext context) : base(context)
        {
        }
    }
}