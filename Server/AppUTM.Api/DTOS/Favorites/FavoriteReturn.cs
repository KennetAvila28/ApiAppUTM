using System.Collections.Generic;
using System.Text.Json.Serialization;
using AppUTM.Api.DTOS.Events;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Favorites
{
    public class FavoriteReturn
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public List<EventDto> Events { get; set; }
    }
}