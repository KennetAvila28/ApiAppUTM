﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    [Table("Favorites")]
    public class Favorites : BaseEntity
    {
        public string Clave { get; set; }

        public IList<EventFavorite> EventsfFavorites { get; set; }
    }
}