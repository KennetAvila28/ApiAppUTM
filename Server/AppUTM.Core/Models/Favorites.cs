using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    [Table("Favorites")]
    public class Favorites : BaseEntity
    {
        public string Clave { get; set; }

        [ForeignKey("Events")]
        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}