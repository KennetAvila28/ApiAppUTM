using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    [Table("Favorites")]
    public class Favorites
    {
        public string Matricula { get; set; }

        public IEnumerable<EventFavorite> Events { get; set; }
    }
}