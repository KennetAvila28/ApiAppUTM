using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    [Table("Favorites")]
    public class Favorites
    {
        public int Id { get; set; }
        public string Clave { get; set; }

        public List<Event> Events { get; set; }
    }
}