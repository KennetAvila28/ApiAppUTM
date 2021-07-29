using System.ComponentModel.DataAnnotations.Schema;

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