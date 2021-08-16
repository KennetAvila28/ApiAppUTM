using AppUTM.Api.DTOS.Events;
using System.Collections.Generic;

namespace AppUTM.Api.DTOS.Favorites
{
    public class FavoriteReturn
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public List<EventDto> Events { get; set; }
    }
}