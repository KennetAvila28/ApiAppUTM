using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    [Table("EventsFavorites")]

    public class EventFavorite
    {
        [ForeignKey("Events")]
        public int EventId { get; set; }
        public Event Event { get; set; }
        [ForeignKey("Favorites")]
        public Favorites FavoriteId { get; set; }
    }
}
