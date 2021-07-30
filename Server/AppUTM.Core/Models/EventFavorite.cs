using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    [Table("EventsFavorites")]
    public class EventFavorite
    {
        [ForeignKey("Events")]
        public int EventId { get; set; }

        [JsonIgnore]
        public Event Event { get; set; }

        [ForeignKey("Favorites")]
        public int FavoriteId { get; set; }

        [JsonIgnore]
        public Favorites Favorite { get; set; }
    }
}