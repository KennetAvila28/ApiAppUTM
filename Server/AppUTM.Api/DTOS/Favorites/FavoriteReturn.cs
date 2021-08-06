using System.Collections.Generic;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Favorites
{
    public class FavoriteReturn
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}