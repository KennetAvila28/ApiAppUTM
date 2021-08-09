using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    public class EventFavorites
    {
        public int FavoriteId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public Favorites Favorites { get; set; }
    }
}