using System.Collections.Generic;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Favorites
{
    public class FavoriteCreate
    {
        public string Clave { get; set; }
        public int EventId { get; set; }
    }
}