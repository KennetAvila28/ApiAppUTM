using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    [Table("Favorites")]
    public class Favorites
    {
        public string Matricula { get; set; }

        public IEnumerable<EventFavorite> Events { get; set; }
    }
}
